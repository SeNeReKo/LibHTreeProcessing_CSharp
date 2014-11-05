using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class SubtreeCommandMultiple : AbstractScriptCommand
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private HExpression expression;
		private CommandSequenceCommand seqCmd;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SubtreeCommandMultiple(int lineNo, HExpression expression, CommandSequenceCommand seqCmd)
			: base(lineNo)
		{
			this.expression = expression;
			this.seqCmd = seqCmd;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx)
		{
			MatchResultGroup mrg = expression.MatchAll(dataCtx.Document);

			for (int i = mrg.Count - 1; i >= 0; i--) {
				MatchResult mr = mrg[i];

				HPathWithIndices path;
				HElement e;

				mr.GetSingleElementSelected(out path, out e);		// TODO: The absolute path is presented here. Does that make sense?
																	//	contra:	the nested group should not see the parental part of the tree
																	//	pro:	this way renaming or even removing this element from the parent remains possible

				seqCmd.Process(ctx, dataCtx.CreateNestedScope(e, false));
			}
		}

		public override void Link(Script script)
		{
			seqCmd.Link(script);
		}

	}

}
