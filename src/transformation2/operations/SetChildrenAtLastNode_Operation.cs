using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public class SetChildrenAtLastNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string nodeName;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SetChildrenAtLastNode_Operation(int lineNo, string nodeName)
			: base(lineNo)
		{
			this.nodeName = nodeName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			for (int i = currentPath.Count - 1; i >= 0; i--) {
				if (currentPath[i].Element is HElement) {
					HElement he = (HElement)(currentPath[i].Element);
					if (nodeName != null) {
						if (!he.Name.Equals(nodeName)) continue;
					}

					HAbstractElementList elementsToAdd = ((HElement)currentElement).Children;
					he.Children.Clear();
					foreach (HAbstractElement elementToAdd in elementsToAdd) {
						he.Children.Add(elementToAdd.Clone());
					}

					return;
				}
			}

			ScriptException.CreateError_Unknown(LineNumber, "No node found in path!");
		}

	}

}
