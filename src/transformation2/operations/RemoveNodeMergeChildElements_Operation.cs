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

	public class RemoveNodeMergeChildElements_Operation : AbstractOperation
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

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public RemoveNodeMergeChildElements_Operation(int lineNo)
			: base(lineNo)
		{
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

			int n = parent.Children.IndexOf(currentElement);
			if (n < 0) throw ScriptException.CreateError_Unknown(LineNumber, "Element not found in child list of parent!");

			// save children

			HAbstractElementList list = ((HElement)(currentElement)).Children;

			// replace with child nodes

			parent.Children.RemoveAt(n);
			parent.Children.InsertRange(n, list);
		}
		
	}

}
