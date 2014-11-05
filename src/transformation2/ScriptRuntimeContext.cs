using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2
{

	public class ScriptRuntimeContext
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private readonly HElement document;
		private readonly HElement clipboard;
		private readonly HElement output;

		private readonly Dictionary<string, object> variables;

		public readonly Script Script;

		private ScriptRuntimeContext parent;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		internal ScriptRuntimeContext(Script script)
		{
			this.Script = script;

			this.clipboard = new HElement("clipboard");
			this.output = new HElement("output");

			this.variables = new Dictionary<string, object>();
		}

		internal ScriptRuntimeContext(Script script, HElement document)
		{
			this.Script = script;

			this.document = document;
			this.clipboard = new HElement("clipboard");
			this.output = new HElement("output");

			this.variables = new Dictionary<string, object>();
		}

		private ScriptRuntimeContext(Script script, ScriptRuntimeContext parent, HElement clipboard, HElement document)
		{
			this.Script = script;

			this.document = document;
			if (clipboard == null) {
				this.clipboard = new HElement("clipboard");
			} else {
				this.clipboard = clipboard;
			}
			this.output = parent.output;

			this.variables = new Dictionary<string, object>();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public HElement Document
		{
			get {
				return document;
			}
		}

		public HElement Clipboard
		{
			get {
				return clipboard;
			}
		}

		public HElement Output
		{
			get {
				return output;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public object GetVariable(string name)
		{
			object var;
			if (variables.TryGetValue(name, out var)) {
				return var;
			}
			if (parent != null) {
				return parent.GetVariable(name);
			} else {
				return null;
			}
		}

		public void RemoveVariable(string name)
		{
			variables.Remove(name);
		}

		public void SetVariable(string name, object value)
		{
			variables.Remove(name);
			variables.Add(name, value);
		}

		public ScriptRuntimeContext CreateNestedScope(HElement document, bool bCreateNewClipboard)
		{
			return new ScriptRuntimeContext(Script, this, bCreateNewClipboard ? null : clipboard, document);
		}

	}

}
