using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.transformation2.cmds;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class ExtraCommandsParser : AbstractComponentBasedParser<AbstractScriptCommandParserComponent, AbstractScriptCommand>
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

		public ExtraCommandsParser()
			: base()
		{
		}

		public ExtraCommandsParser(params AbstractScriptCommandParserComponent[] parsingComponents)
			: base(parsingComponents)
		{
		}

		public ExtraCommandsParser(IEnumerable<AbstractScriptCommandParserComponent> parsingComponents)
			: base(parsingComponents)
		{
		}

		public ExtraCommandsParser(params Type[] parsingComponentTypes)
			: base(parsingComponentTypes)
		{
		}

		public ExtraCommandsParser(IEnumerable<Type> parsingComponentTypes)
			: base(parsingComponentTypes)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

	}

}
