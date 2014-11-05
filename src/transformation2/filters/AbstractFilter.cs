using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.operations;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public abstract class AbstractFilter
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
		/// <param name="lineNo">Line number of first token from parsing this filter.</param>
		public AbstractFilter(int lineNo)
		{
			this.LineNumber = lineNo;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// If <code>true</code> processing should fail if there has no data been returned by the filter.
		/// This check is performed by <code>Process()</code> of the selector.
		/// </summary>
		public bool FailIfNoData
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
		/// This method is called to perform an operation on the specified element.
		/// </summary>
		/// <param name="ctx">The processing context</param>
		/// <param name="dataCtx">The data context containing the (current) document root node, the clipboard and variables</param>
		/// <param name="currentPath">The path of the current element</param>
		/// <param name="currentElement">The element to process</param>
		public abstract HAbstractElement Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement);

		public virtual void Link(Script script)
		{
		}

	}

}
