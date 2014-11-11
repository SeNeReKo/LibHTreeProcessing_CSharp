using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public abstract class AbstractFilterParserComponent : IParserComponent<AbstractFilter>
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private readonly EnumDataType[] validInputDataTypes;
		private readonly EnumDataType[] outputDataType;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AbstractFilterParserComponent(EnumDataType validInputDataType, params EnumDataType[] additionalValidInputDataTypes)
		{
			this.validInputDataTypes = new EnumDataType[1 + additionalValidInputDataTypes.Length];
			this.validInputDataTypes[0] = validInputDataType;
			additionalValidInputDataTypes.CopyTo(this.validInputDataTypes, 1);
			this.outputDataType = validInputDataTypes;
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
				return validInputDataTypes;
			}
		}

		public virtual EnumDataType[] OutputDataTypes
		{
			get {
				return outputDataType;
			}
		}

		public abstract string[] LongHelpText
		{
			get;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public abstract AbstractFilter TryParse(IParsingContext ctx, TokenStream tokens);

	}

}
