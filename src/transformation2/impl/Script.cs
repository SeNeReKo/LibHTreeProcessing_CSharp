using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.impl
{

	/// <summary>
	/// This class is the script implementation.
	/// </summary>
	public class Script : IScript
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private CommandSequenceCommand cmds;

		private bool bLinked;

		private Dictionary<string, SimpleProcedure> procedures;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public Script(Dictionary<string, SimpleProcedure> procedures, params AbstractScriptCommand[] cmds)
		{
			this.procedures = procedures;
			this.cmds = new CommandSequenceCommand(1, cmds);
		}

		public Script(Dictionary<string, SimpleProcedure> procedures, IEnumerable<AbstractScriptCommand> cmds)
		{
			this.procedures = procedures;
			this.cmds = new CommandSequenceCommand(1, cmds);
		}

		public Script(Dictionary<string, SimpleProcedure> procedures, CommandSequenceCommand cmds)
		{
			this.procedures = procedures;
			this.cmds = cmds;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool IsLinked
		{
			get {
				return bLinked;
			}
		}

		public Dictionary<string, SimpleProcedure> Procedures
		{
			get {
				return procedures;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Process the script.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="document">The document to process.</param>
		/// <returns>Returns a container that stores output, clipboard data and - most importantly - the document
		/// transformed by the script.</returns>
		public ScriptRuntimeContext Process(TransformationRuleContext ctx, HElement document)
		{
			if (!bLinked) throw new Exception("Script not yet linked!");

			// clone tree and create data context

			ScriptRuntimeContext dataCtx = new ScriptRuntimeContext(this, (HElement)(document.Clone()));

			// apply rules

			cmds.Process(ctx, dataCtx);

			// return result

			return dataCtx;
		}

		/// <summary>
		/// Process the script.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="dataContext">The data context containing the document to process.</param>
		/// <returns>Returns a container that stores output, clipboard data and - most importantly - the document
		/// transformed by the script. This container is NOT the same as the container passed to this method in the
		/// beginning: It will (always) contain a totally different document tree.</returns>
		public ScriptRuntimeContext Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext,
			bool bCreateNewClipboard)
		{
			return Process(ctx, dataContext, dataContext.Document, bCreateNewClipboard);
		}

		/// <summary>
		/// Process the script.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="dataContext">An existing data context. The document stored in this object will be ignored.</param>
		/// <param name="document">The document to process.</param>
		/// <returns>Returns a container that stores output, clipboard data and - most importantly - the document
		/// transformed by the script. This container is NOT the same as the container passed to this method in the
		/// beginning: It will (always) contain a totally different document tree.</returns>
		public ScriptRuntimeContext Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext, HElement document,
			bool bCreateNewClipboard)
		{
			if (!bLinked) throw new Exception("Script not yet linked!");

			// clone tree and create data context

			ScriptRuntimeContext dataCtx = dataContext.CreateNestedScope((HElement)(document.Clone()), bCreateNewClipboard);

			// apply rules

			cmds.Process(ctx, dataCtx);

			// return result

			return dataCtx;
		}

		public ScriptRuntimeContext CreateRuntimeContext()
		{
			return new ScriptRuntimeContext(this);
		}

		public ScriptRuntimeContext CreateRuntimeContext(HElement document)
		{
			return new ScriptRuntimeContext(this, document);
		}

		public void Link()
		{
			if (!bLinked) {
				cmds.Link(this);
				bLinked = true;
			}
		}

	}

}
