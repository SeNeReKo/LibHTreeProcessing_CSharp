using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public class TransformByClipboardMap_Operation : AbstractFilter
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private HExpression clipboardExpression;
		private string keyAttrName;
		private string valueAttrName;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public TransformByClipboardMap_Operation(int lineNo, HExpression clipboardExpression)
			: base(lineNo)
		{
			this.clipboardExpression = clipboardExpression;
			this.keyAttrName = "key";
			this.valueAttrName = "value";
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			string data;
			HAttribute aRetrieved = null;
			if (currentElement is HText) {
				HText tRetrieved = (HText)currentElement;
				data = tRetrieved.Text;
			} else
			if (currentElement is HAttribute) {
				aRetrieved = (HAttribute)currentElement;
				data = aRetrieved.Value;
			} else
				throw ScriptException.CreateError_Unknown(LineNumber, "Data is neither text nor attribute!");

			// ----

			object replacement = ProcessingUtils.ReplaceDataByClipboardMap(
				dataCtx, clipboardExpression, keyAttrName, valueAttrName,
				data, true, LineNumber);

			if (replacement is string) {
				string sReplacement = (string)replacement;

				if (currentElement is HText) {
					return new HText(sReplacement);
				} else
				if (currentElement is HAttribute) {
					return new HAttribute(aRetrieved.Name, sReplacement);
				} else
					throw new ImplementationErrorException();
			}

			throw ScriptException.CreateError_Unknown(LineNumber, "Unsuitable map: Text values required!");
		}
		
	}

}
