using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.selectors;
using LibHTreeProcessing.src.transformation2.operations;
using LibHTreeProcessing.src.transformation2.filters;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class SelectorBasedScriptCommand : AbstractScriptCommand
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		AbstractSelector selector;
		AbstractFilter filter;
		AbstractOperation operation;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SelectorBasedScriptCommand(AbstractSelector selector, AbstractOperation operation)
			: base(selector.LineNumber)
		{
			this.selector = selector;
			this.operation = operation;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SelectorBasedScriptCommand(AbstractSelector selector, AbstractFilter filter, AbstractOperation operation)
			: base(selector.LineNumber)
		{
			this.selector = selector;
			this.filter = filter;
			this.operation = operation;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx)
		{
			selector.Process(ctx, dataCtx, filter, operation);
		}

		public override void Link(Script script)
		{
			if (filter != null) filter.Link(script);
			if (operation != null) operation.Link(script);
		}

	}

}
