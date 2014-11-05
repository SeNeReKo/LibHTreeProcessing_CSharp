using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.impl;
using LibHTreeProcessing.src.transformation2.operations;


namespace LibHTreeProcessing.src.transformation2.cmds
{

	public class LoadXmlFileIntoClipboard_Operation : AbstractScriptCommand
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		HElement data;
		string[] pathElements;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public LoadXmlFileIntoClipboard_Operation(int lineNo, HElement data, string path)
			: base(lineNo)
		{
			this.data = data;

			if (path.Equals("/")) {
				this.pathElements = new string[0];
			} else {
				this.pathElements = path.Substring(1).Split('/');
			}
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext)
		{
			HElement e = ProcessingUtils.GetCreateByPath(dataContext.Clipboard, pathElements);
			e.Children.Add(data);
		}

	}

}
