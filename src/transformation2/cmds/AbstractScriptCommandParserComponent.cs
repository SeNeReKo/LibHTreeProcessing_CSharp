using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.cmds
{

	public abstract class AbstractScriptCommandParserComponent : IParserComponent<AbstractScriptCommand>
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

		public AbstractScriptCommandParserComponent()
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

		public abstract AbstractScriptCommand TryParse(IParsingContext ctx, TokenStream tokens);

	}

}
