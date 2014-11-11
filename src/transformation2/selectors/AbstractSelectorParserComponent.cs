using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.selectors
{

	public abstract class AbstractSelectorParserComponent : IParserComponent<AbstractSelector>
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

		public AbstractSelectorParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public abstract string[] ShortHelp
		{
			get;
		}

		public EnumDataType[] ValidInputDataTypes
		{
			get {
				return new EnumDataType[0];
			}
		}

		public abstract EnumDataType[] OutputDataTypes
		{
			get;
		}

		public abstract string[] LongHelpText
		{
			get;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public abstract AbstractSelector TryParse(IParsingContext ctx, TokenStream tokens);

	}

}
