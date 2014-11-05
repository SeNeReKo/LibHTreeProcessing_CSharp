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

	public class TransformEntitiesByClipboardMap_Operation : AbstractFilter
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
		public TransformEntitiesByClipboardMap_Operation(int lineNo, HExpression clipboardExpression)
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

			// ---- build output list ----

			List<HAbstractElement> output = new List<HAbstractElement>();

			StringBuilder sb = new StringBuilder();
			StringBuilder sbEntity = new StringBuilder();
			bool bInEntity = false;
			foreach (char c in data) {
				if (bInEntity) {
					if (c == ';') {
						bInEntity = false;
						object replacement = ProcessingUtils.ReplaceDataByClipboardMap(
							dataCtx, clipboardExpression, keyAttrName, valueAttrName,
							sbEntity.ToString(), true, LineNumber);
							
						if (replacement is string) {
							sb.Append((string)replacement);
						} else
						if (replacement is HAbstractElementList) {
							if (sb.Length > 0) {
								output.Add(new HText(sb.ToString()));
								sb.Remove(0, sb.Length);
								output.AddRange((HAbstractElementList)replacement);
							}
						} else
							throw new ImplementationErrorException();

						sbEntity.Remove(0, sbEntity.Length);
					} else {
						sbEntity.Append(c);
					}
				} else {
					if (c == '&') {
						bInEntity = true;
					} else {
						sb.Append(c);
					}
				}
			}
			if (bInEntity)
				ScriptException.CreateError_Unknown(LineNumber, "Invalid entity specification in text!");
			if (sb.Length > 0) {
				output.Add(new HText(sb.ToString()));
			}

			// ----

			HElement newNode = new HElement("temp");
			newNode.Children.AddRange(output);
			return newNode;
		}

	}

}
