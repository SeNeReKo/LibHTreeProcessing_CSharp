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

	public class SubtreeCommandSingle : AbstractScriptCommand
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
		public SubtreeCommandSingle(int lineNo, HExpression expression, CommandSequenceCommand seqCmd)
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
			MatchResult mr = expression.MatchOne(dataCtx.Document);
			if (mr == null) return;

			HPathWithIndices path;
			HElement e;

			mr.GetSingleElementSelected(out path, out e);

			seqCmd.Process(ctx, dataCtx.CreateNestedScope(e, false));
		}

		public override void Link(Script script)
		{
			seqCmd.Link(script);
		}

	}

}
