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

	public class CommandSequenceCommand : AbstractScriptCommand
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		List<AbstractScriptCommand> commands;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public CommandSequenceCommand(int lineNo, params AbstractScriptCommand[] commands)
			: base(lineNo)
		{
			this.commands = new List<AbstractScriptCommand>(commands);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public CommandSequenceCommand(int lineNo, IEnumerable<AbstractScriptCommand> commands)
			: base(lineNo)
		{
			this.commands = new List<AbstractScriptCommand>(commands);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx)
		{
			foreach (AbstractScriptCommand cmd in commands) {
				cmd.Process(ctx, dataCtx);
			}
		}

		public override void Link(Script script)
		{
			foreach (AbstractScriptCommand cmd in commands) {
				cmd.Link(script);
			}
		}

	}

}
