using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public class HText : HAbstractElement
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public string Text;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public HText(string text)
		{
			this.Text = text;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Clone()
		{
			return new HText(Text);
		}

		public override bool ToPlainText(TextWriter tw)
		{
			tw.WriteLine(Text);
			return false;
		}

		/// <summary>
		/// Visit all elements (recursively).
		/// </summary>
		/// <param name="parentElements">The parent elements or <code>null</code>.</param>
		/// <param name="visitor">The visitor</param>
		/// <param name="bDescend">If <code>true</code> recursively descend the tree</param>
		/// <returns>Returns <code>false</code> if the visitor has returned <code>false</code> once.</returns>
		internal override bool DoVisit(Stack<HAbstractElement> parentElements, IHVisitor visitor, bool bDescend)
		{
			EnumVisitReturnCode r = visitor.Visit(parentElements, this);
			return r != EnumVisitReturnCode.Terminate;
		}

		/// <summary>
		/// Visit all elements (recursively).
		/// </summary>
		/// <param name="parentElements">The parent elements or <code>null</code>.</param>
		/// <param name="visitor">The visitor</param>
		/// <param name="bDescend">If <code>true</code> recursively descend the tree</param>
		/// <returns>Returns <code>false</code> if the visitor has returned <code>false</code> once.</returns>
		internal override bool DoVisit<T>(Stack<HAbstractElement> parentElements, IHVisitorCTX<T> visitor, bool bDescend, T context)
		{
			EnumVisitReturnCode r = visitor.Visit(context, parentElements, this);
			return r != EnumVisitReturnCode.Terminate;
		}

		public override string ToString()
		{
			return Text;
		}

	}

}
