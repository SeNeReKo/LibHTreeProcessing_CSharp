using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.impl;
using LibHTreeProcessing.src.transformation2.operations;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public class MoveToClipboard_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		string[] pathElements;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public MoveToClipboard_Operation(int lineNo, string path)
			: base(lineNo)
		{
			if (path.Equals("/")) {
				this.pathElements = new string[0];
			} else {
				this.pathElements = path.Substring(1).Split('/');
			}
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			PathStruct ps = currentPath.ParentOf(currentElement);		// TODO: check "currentPath" in subtree: is it as short as expected?
			if (ps.Element == null) {
				// can't process this: path is too short
				return;
			}
			HElement heParent = (HElement)(ps.Element);

			HElement e = ProcessingUtils.GetCreateByPath(dataContext.Clipboard, pathElements);

			if (currentElement is HAttribute) {
				HAttribute a = (HAttribute)currentElement;
				heParent.Attributes.RemoveAll(a.Name);
				e.Children.Add(new HText(a.Value));
			} else
			if (currentElement is HText) {
				HText t = (HText)currentElement;
				heParent.Children.Remove(currentElement);
				e.Children.Add(t);
			} else
			if (currentElement is HElement) {
				HElement e2 = (HElement)currentElement;
				heParent.Children.Remove(currentElement);
				e.Children.Add(e2);
			} else
				throw new ImplementationErrorException();
		}

	}

}
