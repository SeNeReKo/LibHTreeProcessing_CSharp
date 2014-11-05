using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class AttributeVisitor
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private AttributeMatcher[] attributeMatchers;
		private HAttribute[] attributesMatched;
		private bool[] temp;
		private HAbstractElement elementMatched;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AttributeVisitor(string emitID, params AttributeMatcher[] attributeMatchers)
		{
			this.EmitID = emitID;

			if (attributeMatchers.Length == 0) {
				this.attributeMatchers = null;
				this.temp = new bool[0];
				this.attributesMatched = new HAttribute[0];
			} else {
				this.attributeMatchers = attributeMatchers;
				this.temp = new bool[attributeMatchers.Length];
				this.attributesMatched = new HAttribute[attributeMatchers.Length];
			}
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public string EmitID
		{
			get;
			set;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void SetAttributeMatchers(params AttributeMatcher[] attributeMatchers)
		{
			if (attributeMatchers.Length == 0) {
				this.attributeMatchers = null;
				this.temp = new bool[0];
				this.attributesMatched = new HAttribute[0];
			} else {
				this.attributeMatchers = attributeMatchers;
				this.temp = new bool[attributeMatchers.Length];
				this.attributesMatched = new HAttribute[attributeMatchers.Length];
			}
		}

		public void SetAttributeMatchers(IEnumerable<AttributeMatcher> attributeMatchers)
		{
			AttributeMatcher[] attributeMatchers2 = attributeMatchers.ToArray();
			if (attributeMatchers2.Length == 0) {
				this.attributeMatchers = null;
				this.temp = new bool[0];
				this.attributesMatched = new HAttribute[0];
			} else {
				this.attributeMatchers = attributeMatchers2;
				this.temp = new bool[attributeMatchers2.Length];
				this.attributesMatched = new HAttribute[attributeMatchers2.Length];
			}
		}

		public bool Match(HAbstractElement element)
		{
			if (element is HElement) {
				HElement e = (HElement)element;

				if (attributeMatchers != null) {
					int expectedMatches = attributeMatchers.Length;
					for (int i = 0; i < attributeMatchers.Length; i++) {
						temp[i] = false;
						attributesMatched[i] = null;
					}
					foreach (HAttribute a in e.Attributes) {
						for (int i = 0; i < attributeMatchers.Length; i++) {
							if (temp[i]) continue;
							AttributeMatcher am = attributeMatchers[i];
							if (am.Match(a)) {
								temp[i] = true;
								string emitID = am.EmitID;
								if (emitID != null) {
									attributesMatched[i] = a;
								}
								expectedMatches--;
								break;
							}
						}
						if (expectedMatches == 0) break;
					}
					if (expectedMatches > 0) return false;
				}

				elementMatched = element;

				return true;
			} else {
				elementMatched = null;

				return false;
			}
		}

	}

}
