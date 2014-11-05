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

	public class SetAsAttributeAtNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private HExpression he;
		private string attrName;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SetAsAttributeAtNode_Operation(int lineNo, HExpression he, string attrName)
			: base(lineNo)
		{
			this.he = he;
			this.attrName = attrName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			MatchResult mr = he.MatchOne(dataCtx.Document);
			if (mr == null) {
				throw ScriptException.CreateError_Unknown(LineNumber, "Not found: " + he.ToString());
			}

			HPathWithIndices hpwi;
			HElement targetNode;
			mr.GetSingleElementSelected(out hpwi, out targetNode);

			// set text element

			if (currentElement is HText) {
				HText t = (HText)currentElement;
				targetNode.SetAttribute(attrName, t.Text);
			} else
			if (currentElement is HAttribute) {
				HAttribute a = (HAttribute)currentElement;
				targetNode.SetAttribute(attrName, a.Value);
			} else
				throw ScriptException.CreateError_Unknown(LineNumber, "Can't handle node elements!");
		}

	}

}
