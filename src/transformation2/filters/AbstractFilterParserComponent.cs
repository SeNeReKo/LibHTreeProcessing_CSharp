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

		private EnumDataType[] validInputDataTypes;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AbstractFilterParserComponent(EnumDataType validInputDataType, params EnumDataType[] additionalValidInputDataTypes)
		{
			this.validInputDataTypes = new EnumDataType[1 + additionalValidInputDataTypes.Length];
			this.validInputDataTypes[0] = validInputDataType;
			additionalValidInputDataTypes.CopyTo(this.validInputDataTypes, 1);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public EnumDataType[] ValidInputDataTypes
		{
			get {
				return validInputDataTypes;
			}
		}

		public abstract string[] ShortHelp
		{
			get;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public abstract AbstractFilter TryParse(IParsingContext ctx, TokenStream tokens);

	}

}
