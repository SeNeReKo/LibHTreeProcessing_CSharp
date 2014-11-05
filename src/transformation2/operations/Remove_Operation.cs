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

	public class Remove_Operation : AbstractOperation
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
		public Remove_Operation(int lineNo)
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
			PathStruct ps = currentPath.ParentOf(currentElement);		// TODO: check "currentPath" in subtree: is it as short as expected?
			if (ps.Element == null) {
				// can't process this: path is too short
				return;
			}
			HElement heParent = (HElement)(ps.Element);

			if (currentElement is HElement) {
				heParent.Children.Remove(currentElement);
			} else
			if (currentElement is HText) {
				heParent.Children.Remove(currentElement);
			} else
			if (currentElement is HAttribute) {
				heParent.Attributes.RemoveAll(((HAttribute)currentElement).Name);
			} else {
				throw new ImplementationErrorException();
			}
		}
		
	}

}
