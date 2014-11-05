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

	public class FilterAddPrefix_Operation : AbstractFilter
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string text;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public FilterAddPrefix_Operation(int lineNo, string text)
			: base(lineNo)
		{
			this.text = text;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			if (currentElement is HText) {
				HText t = (HText)currentElement;
				HText t2 = new HText(this.text + t.Text);
				return t2;
			} else
			if (currentElement is HAttribute) {
				HAttribute a = (HAttribute)currentElement;
				HAttribute a2 = new HAttribute(a.Name, this.text + a.Value);
				return a2;
			} else
				throw ScriptException.CreateError_Unknown(LineNumber, "Data is neither text nor attribute!");

			// return null;
		}
		
	}

}
