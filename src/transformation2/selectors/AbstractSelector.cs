using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.filters;
using LibHTreeProcessing.src.transformation2.operations;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.selectors
{

	public abstract class AbstractSelector
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
		public AbstractSelector(int lineNo)
		{
			this.LineNumber = lineNo;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// If <code>true</code> processing should fail if there has no data been selected.
		/// This check is performed by <code>Process()</code> of this class.
		/// </summary>
		public bool FailIfNothingSelected
		{
			get;
			set;
		}

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
		/// This method is called to select one or more nodes from the specified tree.
		/// </summary>
		/// <param name="ctx">The processing context</param>
		/// <param name="dataCtx">The data context containing the (current) document root node, the clipboard and variables</param>
		/// <param name="filter">A filter to apply</param>
		/// <param name="operation">An operation to perform</param>
		public abstract void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx,
			AbstractFilter filter, AbstractOperation operation);

	}

}
