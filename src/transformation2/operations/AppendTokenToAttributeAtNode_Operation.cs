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

	public class AppendTokenToAttributeAtNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		string attributeName;
		HExpression path;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public AppendTokenToAttributeAtNode_Operation(int lineNo, HExpression path)
			: base(lineNo)
		{
			this.path = path;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public AppendTokenToAttributeAtNode_Operation(int lineNo, HExpression path, string attributeName)
			: base(lineNo)
		{
			this.path = path;
			this.attributeName = attributeName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HAttribute a;

			if (attributeName == null) {
				MatchResult mr = path.MatchOne(dataCtx.Document);
				if (mr == null) ScriptException.CreateError_SelectorDidNotReturnAnyData(LineNumber);
				HPathWithIndices p;
				mr.GetSingleElementSelected(out p, out a);
			} else {
				MatchResult mr = path.MatchOne(dataCtx.Document);
				if (mr == null) ScriptException.CreateError_SelectorDidNotReturnAnyData(LineNumber);
				HPathWithIndices p;
				HElement n;
				mr.GetSingleElementSelected(out p, out n);
				a = n.Attributes[attributeName];
				if (a == null) {
					a = new HAttribute(attributeName, "");
					n.Attributes.Add(a);
				}
			}

			string textToAppend;

			if (currentElement is HText) {
				HText textElement = (HText)currentElement;
				textToAppend = textElement.Text;
			} else
			if (currentElement is HAttribute) {
				HAttribute attrib = (HAttribute)currentElement;
				textToAppend = attrib.Value;
			} else
				throw new ImplementationErrorException();

			textToAppend = textToAppend.Trim();

			if (textToAppend.Length > 0) {
				if ((a.Value.Length > 0) && !a.Value.EndsWith(' ')) a.Value += " ";
				a.Value += textToAppend;
			}
		}
		
	}

}
