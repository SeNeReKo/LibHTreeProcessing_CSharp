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

	public class MoveToChildNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private HExpression expression;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public MoveToChildNode_Operation(int lineNo, HExpression expression)
			: base(lineNo)
		{
			this.expression = expression;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			PathStruct ps = currentPath.ParentOf(currentElement);
			if (ps.Element == null) {
				throw ScriptException.CreateError_NoParent(LineNumber);
			}

			HElement parent = (HElement)(ps.Element);

			// select node where the element should be inserted

			MatchResult mr = expression.MatchOne(parent);
			if (mr == null) throw ScriptException.CreateError_SelectorDidNotReturnAnyData(LineNumber);

			// remove element from parent

			int n = parent.Children.IndexOf(currentElement);
			if (n < 0) ScriptException.CreateError_Unknown(LineNumber, "Element not found in child list of parent!");
			parent.Children.RemoveAt(n);

			// prepare insert

			HPathWithIndices selectedPath;
			HElement selectedNode;
			mr.GetSingleElementSelected(out selectedPath, out selectedNode);

			// insert into tree

			selectedNode.Children.Add(currentElement);
		}
		
	}

}
