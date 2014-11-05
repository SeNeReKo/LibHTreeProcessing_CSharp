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

	public class CopyToClipboard_Operation : AbstractOperation
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
		public CopyToClipboard_Operation(int lineNo, string path)
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
			HElement e = ProcessingUtils.GetCreateByPath(dataContext.Clipboard, pathElements);

			if (currentElement is HAttribute) {
				HAttribute a = (HAttribute)currentElement;
				e.Children.Add(new HText(a.Value));
			} else
			if (currentElement is HText) {
				HText t = (HText)currentElement;
				e.Children.Add(new HText(t.Text));
			} else
			if (currentElement is HElement) {
				HElement e2 = (HElement)currentElement;
				e.Children.Add((HElement)(e2.Clone()));
			} else
				throw new ImplementationErrorException();
		}

	}

}
