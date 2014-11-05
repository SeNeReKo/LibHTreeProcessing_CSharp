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

	public class SelectAllTexts_Selector : AbstractSelector
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
		public SelectAllTexts_Selector(int lineNo, HExpression expression)
			: base(lineNo)
		{
			if (expression == null) {
				this.expression = HExpressionCompiler.Compile("//§text", true);
			} else {
				this.expression = expression;
			}
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, AbstractFilter filter, AbstractOperation operation)
		{
			MatchResultGroup mrg = expression.MatchAll(dataCtx.Document);
			if (FailIfNothingSelected) {
				if ((mrg == null) || (mrg.Count == 0))
					throw ScriptException.CreateError_SelectorDidNotReturnAnyData(LineNumber);
			}

			for (int i = mrg.Count - 1; i >= 0; i--) {
				MatchResult mr = mrg[i];

				HPathWithIndices currentPath;
				HText data2;
				HAbstractElement data = mr.GetSingleElementSelected(out currentPath, out data2);

				if (filter != null) {
					data = (HText)(filter.Process(ctx, dataCtx, currentPath, data));
					if (data == null) {
						if (filter.FailIfNoData)
							throw ScriptException.CreateError_FilterDidNotReturnAnyData(LineNumber);
						continue;
					}
				}

				dataCtx.SetVariable("index", i);
				operation.Process(ctx, dataCtx, currentPath, data);
			}
			dataCtx.RemoveVariable("index");
		}

	}

}
