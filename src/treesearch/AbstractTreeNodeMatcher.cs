using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public abstract class AbstractTreeNodeMatcher
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AbstractTreeNodeMatcher()
		{
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

		/// <summary>
		/// Try to match this element.
		/// </summary>
		/// <param name="element">The element to match against.</param>
		/// <returns>Returns <code>true</code> if there is a match.</returns>
		public abstract bool Match(HAbstractElement element);

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
		public abstract void Emit(HPathWithIndices path, List<MatchingRecord> outRecords);

		public abstract void CollectEmitIDs(Dictionary<string, EnumElementType> collection, out EnumElementType lastElement);

	}

}
