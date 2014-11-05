using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.stringmatching;
using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class TreeTextMatcher : AbstractTreeNodeMatcher
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private AbstractStringMatcher textMatcher;
		private HAbstractElement elementMatched;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/*
		public TreeElementMatcher()
		{
		}
		*/

		public TreeTextMatcher(string emitID, AbstractStringMatcher textMatcher)
		{
			this.EmitID = emitID;
			this.textMatcher = textMatcher;
		}

		public TreeTextMatcher(string emitID, string expectedText)
		{
			this.EmitID = emitID;
			this.textMatcher = new StringMatcherEquals(expectedText);
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
				collection[EmitID] = EnumElementType.Text;
			}
			lastElement = EnumElementType.Text;
		}

		public override bool Match(HAbstractElement element)
		{
			if (element is HText) {
				HText t = (HText)element;

				if (textMatcher != null) {
					if (!textMatcher.Match(t.Text)) return false;
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
			if (EmitID != null) {
				outRecords.Add(new MatchingRecord(
					EmitID,
					HPathWithIndices.Clone(path),
					null));
			}
		}

	}

}
