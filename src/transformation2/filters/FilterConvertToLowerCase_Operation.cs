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

	public class FilterConvertToLowerCase_Operation : AbstractFilter
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
		public FilterConvertToLowerCase_Operation(int lineNo)
			: base(lineNo)
		{
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
				HText t2 = new HText(t.Text.ToLower());
				return t2;
			} else
			if (currentElement is HAttribute) {
				HAttribute a = (HAttribute)currentElement;
				HAttribute a2 = new HAttribute(a.Name, a.Value.ToLower());
				return a2;
			} else
				throw ScriptException.CreateError_Unknown(LineNumber, "Data is neither text nor attribute!");

			// return null;
		}
		
	}

}
