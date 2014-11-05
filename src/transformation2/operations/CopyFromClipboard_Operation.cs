﻿using System;
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

	public class CopyFromClipboard_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		HExpression clipboardExpression;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public CopyFromClipboard_Operation(int lineNo, HExpression clipboardExpression)
			: base(lineNo)
		{
			this.clipboardExpression = clipboardExpression;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HElement he = (HElement)(currentElement);

			MatchResultGroup mrg = clipboardExpression.MatchAll(dataContext.Clipboard);
			foreach (MatchResult mr in mrg) {
				HPathWithIndices p;
				HAbstractElement clipboadElement;
				mr.GetSingleElementSelected(out p, out clipboadElement);

				if (clipboadElement is HAttribute) {
					he.Attributes.Add((HAttribute)(clipboadElement.Clone()));
				} else {
					he.Children.Add(clipboadElement.Clone());
				}
			}
		}

	}

}
