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

	public abstract class AbstractOperationParserComponent : IParserComponent<AbstractOperation>
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

		public AbstractOperationParserComponent(EnumDataType validInputDataType, params EnumDataType[] additionalValidInputDataTypes)
		{
			this.validInputDataTypes = new EnumDataType[1 + additionalValidInputDataTypes.Length];
			this.validInputDataTypes[0] = validInputDataType;
			additionalValidInputDataTypes.CopyTo(this.validInputDataTypes, 1);
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

		public EnumDataType[] OutputDataTypes
		{
			get {
				return new EnumDataType[0];
			}
		}

		public abstract string[] LongHelpText
		{
			get;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public abstract AbstractOperation TryParse(IParsingContext ctx, TokenStream tokens);

	}

}
