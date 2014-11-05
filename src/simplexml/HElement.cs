using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public class HElement : HAbstractElement
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public readonly HAttributeList Attributes;

		public readonly HAbstractElementList Children;

		public string Name;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public HElement(string name)
		{
			this.Name = name;
			this.Attributes = new HAttributeList();
			this.Children = new HAbstractElementList();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool HasOnlyTexts
		{
			get {
				return Children.HasOnlyTexts;
			}
		}

		public bool HasOnlyElements
		{
			get {
				return Children.HasOnlyElements;
			}
		}

		public bool IsEmpty
		{
			get {
				return (Attributes.Count == 0) && (Children.Count == 0);
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Clone()
		{
			HElement n = new HElement(Name);
			foreach (HAttribute a in Attributes) {
				n.Attributes.Add((HAttribute)(a.Clone()));
			}
			foreach (HAbstractElement e in Children) {
				n.Children.Add(e.Clone());
			}
			return n;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(Name);

			if (Attributes.Count > 0) {
				foreach (HAttribute a in Attributes) {
					sb.Append(' ');
					sb.Append(a.ToString());
				}
			}

			return sb.ToString();
		}

		public bool NameIs(string name)
		{
			return this.Name.Equals(name);
		}

		public bool ContainsAttribute(string name)
		{
			HAttribute a = Attributes[name];
			if (a == null) return false;
			return true;
		}

		public bool ContainsAttribute(string name, params string[] possibleValues)
		{
			HAttribute a = Attributes[name];
			if (a == null) return false;
			foreach (string s in possibleValues) {
				if (s.Equals(a.Value)) return true;
			}
			return false;
		}

		public override bool ToPlainText(TextWriter tw)
		{
			bool bLastWasEmptyLine = false;
			foreach (HAbstractElement ae2 in Children) {
				bLastWasEmptyLine = ae2.ToPlainText(tw);
			}
			if (!bLastWasEmptyLine) tw.WriteLine();
			return true;
		}

		internal override bool DoVisit(Stack<HAbstractElement> parentElements, IHVisitor visitor, bool bDescend)
		{
			EnumVisitReturnCode retCode;
			retCode = visitor.Visit(parentElements, this);
			if (retCode == EnumVisitReturnCode.Terminate) return false;
			if ((retCode == EnumVisitReturnCode.ContinueAndDescend) && bDescend && (Children != null)) {
				parentElements.Push(this);
				if (!Children.DoVisit(parentElements, visitor, true)) return false;
				parentElements.Pop();
			}
			return true;
		}

		internal override bool DoVisit<T>(Stack<HAbstractElement> parentElements, IHVisitorCTX<T> visitor, bool bDescend, T context)
		{
			EnumVisitReturnCode retCode;
			retCode = visitor.Visit(context, parentElements, this);
			if (retCode == EnumVisitReturnCode.Terminate) return false;
			if ((retCode == EnumVisitReturnCode.ContinueAndDescend) && bDescend && (Children != null)) {
				parentElements.Push(this);
				if (!Children.DoVisit(parentElements, visitor, true, context)) return false;
				parentElements.Pop();
			}
			return true;
		}

		public HAttribute SetAttribute(string name, string value)
		{
			return Attributes.SetAttribute(name, value);
		}

		/// <summary>
		/// Create and add a regular child element
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public HElement CreateChildElement(string name)
		{
			HElement e = new HElement(name);
			Children.Add(e);
			return e;
		}

		/// <summary>
		/// Create and add a regular child element
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public HElement GetCreateChildElement(string name)
		{
			foreach (HAbstractElement e in Children) {
				if (e is HElement) {
					HElement he = (HElement)e;
					if (he.Name.Equals(name)) return he;
				}
			}

			HElement e2 = new HElement(name);
			Children.Add(e2);
			return e2;
		}

		/// <summary>
		/// Create and add a text element as child
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public HText CreateChildText(string text)
		{
			HText t = new HText(text);
			Children.Add(t);
			return t;
		}

		public HElement RemoveChildElement(string name)
		{
			for (int i = 0; i < Children.Count; i++) {
				if (Children[i] is HElement) {
					HElement he = (HElement)(Children[i]);
					if (he.Name.Equals(name)) {
						Children.RemoveAt(i);
						return he;
					}
				}
			}

			return null;
		}

	}

}
