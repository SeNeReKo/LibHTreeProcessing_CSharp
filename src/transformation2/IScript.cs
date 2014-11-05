using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2
{

	public interface IScript
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

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		bool IsLinked
		{
			get;
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
		ScriptRuntimeContext Process(TransformationRuleContext ctx, HElement document);

		/// <summary>
		/// Process the script.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="dataContext">The data context containing the document to process.</param>
		/// <returns>Returns a container that stores output, clipboard data and - most importantly - the document
		/// transformed by the script. This container is NOT the same as the container passed to this method in the
		/// beginning: It will (always) contain a totally different document tree.</returns>
		ScriptRuntimeContext Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext,
			bool bCreateNewClipboard);

		/// <summary>
		/// Process the script.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="dataContext">An existing data context. The document stored in this object will be ignored.</param>
		/// <param name="document">The document to process.</param>
		/// <returns>Returns a container that stores output, clipboard data and - most importantly - the document
		/// transformed by the script. This container is NOT the same as the container passed to this method in the
		/// beginning: It will (always) contain a totally different document tree.</returns>
		ScriptRuntimeContext Process(TransformationRuleContext ctx, ScriptRuntimeContext dataContext, HElement document,
			bool bCreateNewClipboard);

		ScriptRuntimeContext CreateRuntimeContext();

		ScriptRuntimeContext CreateRuntimeContext(HElement document);

		void Link();

	}

}
