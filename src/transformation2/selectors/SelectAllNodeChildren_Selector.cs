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

	public class SelectAllNodeChildren_Selector : AbstractSelector
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
		public SelectAllNodeChildren_Selector(int lineNo, HExpression expression)
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
			HElement selectedNode;
			mr.GetSingleElementSelected(out currentPath, out selectedNode);

			int index = -1;
			foreach (HAbstractElement selectedChild in selectedNode.Children.ToArray()) {
				index++;
				HAbstractElement data = selectedChild;

				if (filter != null) {
					data = (HElement)(filter.Process(ctx, dataCtx, currentPath, data));
					if (data == null) {
						if (filter.FailIfNoData)
							throw ScriptException.CreateError_FilterDidNotReturnAnyData(LineNumber);
						continue;
					}
				}

				dataCtx.SetVariable("index", index);
				currentPath.Push(data, index);
				operation.Process(ctx, dataCtx, currentPath, data);
				currentPath.Pop();
			}
		}

	}

}
