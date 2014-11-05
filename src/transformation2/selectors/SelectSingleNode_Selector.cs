using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.operations;
using LibHTreeProcessing.src.transformation2.impl;
using LibHTreeProcessing.src.transformation2.filters;


namespace LibHTreeProcessing.src.transformation2.selectors
{

	public class SelectSingleNode_Selector : AbstractSelector
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		HExpression expression;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SelectSingleNode_Selector(int lineNo, HExpression expression)
			: base(lineNo)
		{
			this.expression = expression;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, AbstractFilter filter, AbstractOperation operation)
		{
			MatchResult mr = expression.MatchOne(dataCtx.Document);
			if (FailIfNothingSelected) {
				if (mr == null)
					throw ScriptException.CreateError_SelectorDidNotReturnAnyData(LineNumber);
			} else {
				if (mr == null) return;
			}

			HPathWithIndices currentPath;
			HElement data2;
			HAbstractElement data = mr.GetSingleElementSelected(out currentPath, out data2);

			if (filter != null) {
				data = (HElement)(filter.Process(ctx, dataCtx, currentPath, data));
				if (data == null) {
					if (filter.FailIfNoData)
						throw ScriptException.CreateError_FilterDidNotReturnAnyData(LineNumber);
					return;
				}
			}

			operation.Process(ctx, dataCtx, currentPath, data);
		}

	}

}
