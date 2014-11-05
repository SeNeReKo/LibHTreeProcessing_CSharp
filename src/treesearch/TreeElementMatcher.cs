using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.stringmatching;
using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class TreeElementMatcher : AbstractTreeNodeMatcher
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private AbstractStringMatcher nameMatcher;
		private AttributeMatcher[] attributeMatchers;
		private HAttribute[] attributesMatched;
		private bool[] temp;
		private HAbstractElement elementMatched;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/*
		public TreeElementMatcher()
		{
		}
		*/

		public TreeElementMatcher(string emitID, params AttributeMatcher[] attributeMatchers)
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

		public TreeElementMatcher(string emitID, string expectedName)
		{
			this.EmitID = emitID;
			this.attributeMatchers = new AttributeMatcher[0];

			if (expectedName != null) {
				this.nameMatcher = new StringMatcherEquals(expectedName);
			}

			this.temp = new bool[0];
			this.attributesMatched = new HAttribute[0];
		}

		public TreeElementMatcher(string emitID, AbstractStringMatcher nameMatcher,
			params AttributeMatcher[] attributeMatchers)
		{
			this.EmitID = emitID;

			this.nameMatcher = nameMatcher;

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

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void CollectEmitIDs(Dictionary<string, EnumElementType> collection, out EnumElementType lastElement)
		{
			if (EmitID != null) {
				collection[EmitID] = EnumElementType.Node;
			}
			if (attributeMatchers != null) {
				foreach (AttributeMatcher am in attributeMatchers) {
					if (am.EmitID != null) {
						collection[am.EmitID] = EnumElementType.Attribute;
					}
				}
			}
			lastElement = EnumElementType.Node;
		}

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

		public override bool Match(HAbstractElement element)
		{
			if (element is HElement) {
				HElement e = (HElement)element;

				if (nameMatcher != null) {
					if (!nameMatcher.Match(e.Name)) return false;
				}

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

		/// <summary>
		/// This method is called to give the matcher a chance for emitting something.
		/// Multiple emits can be performed by this implementation: All should be written to
		/// <code>outRecords</code>.
		/// Please note that only if the whole recognition chain matches this method will be invoked.
		/// In that case <code>Emit()</code> is invoked for all matchers.
		/// </summary>
		/// <param name="path">A path to the element matched in a temporary object.
		/// Do not modify it! Clone it, if necessary.</param>
		/// <param name="outRecords">A collection that recieves results.</param>
		public override void Emit(HPathWithIndices path, List<MatchingRecord> outRecords)
		{
			for (int i = 0; i < attributesMatched.Length; i++) {
				if (attributesMatched[i] != null) {
					outRecords.Add(new MatchingRecord(
						attributeMatchers[i].EmitID,
						(HPathWithIndices)(HPathWithIndices.Clone(path).Push(new PathStruct(attributesMatched[i], -1))),
						attributesMatched[i].Value));
				}
			}

			if (EmitID != null) {
				outRecords.Add(new MatchingRecord(
					EmitID,
					HPathWithIndices.Clone(path),
					null));
			}
		}

	}

}
