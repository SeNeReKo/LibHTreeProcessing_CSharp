using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public abstract class AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public AbstractOperation(int lineNo)
		{
			this.LineNumber = lineNo;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Line number of first token from parsing this selector.
		/// </summary>
		public int LineNumber
		{
			get;
			private set;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// This method is called to perform an operation on the specified element.
		/// </summary>
		/// <param name="ctx">The processing context</param>
		/// <param name="dataCtx">The data context containing the (current) document root node, the clipboard and variables</param>
		/// <param name="currentPath">The path of the current element</param>
		/// <param name="currentElement">The element to process</param>
		public abstract void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx,
			HPathWithIndices currentPath, HAbstractElement currentElement);

		public virtual void Link(Script script)
		{
		}

	}

}
