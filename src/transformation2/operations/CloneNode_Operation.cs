using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;

using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.impl;
using LibHTreeProcessing.src.treesearch;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public class CloneNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		List<HAttribute> attributesToSet;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public CloneNode_Operation(int lineNo, List<HAttribute> attributesToSet)
			: base(lineNo)
		{
			this.attributesToSet = attributesToSet;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HElement nodeToClone = (HElement)currentElement;

			PathStruct ps = currentPath.ParentOf(nodeToClone);
			if (ps.Element == null) {
				throw ScriptException.CreateError_NoParent(LineNumber);
			}
			HElement parentOfNodeToClone = (HElement)(ps.Element);

			// perform cloning

			List<HAbstractElement> nodesCloned = new List<HAbstractElement>();
			nodesCloned.Add(nodeToClone);
			for (int i = 1; i < attributesToSet.Count; i++) {
				nodesCloned.Add((HElement)(nodeToClone.Clone()));
			}
			for (int i = 0; i < attributesToSet.Count; i++) {
				((HElement)(nodesCloned[i])).SetAttribute(attributesToSet[i].Name, attributesToSet[i].Value);
			}

			// insert clones

			int index = parentOfNodeToClone.Children.IndexOf(nodeToClone);
			if (index < 0) throw ScriptException.CreateError_Unknown(LineNumber, "Can't find position in parent child list? That's strange!");
			parentOfNodeToClone.Children.RemoveAt(index);
			parentOfNodeToClone.Children.InsertRange(index, nodesCloned);
		}

	}

}
