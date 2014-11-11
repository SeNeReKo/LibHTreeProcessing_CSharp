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

	public class Noop_ParserComponent : AbstractScriptCommandParserComponent
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

		public Noop_ParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] { "noop" };
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] { "No operation. This command does nothing at all." };
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractScriptCommand TryParse(IParsingContext ctx, TokenStream tokens)
		{
			Token[] tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("noop")
				);
			if (tokensMatched == null) return null;

			return new Noop_TransformationRule(tokensMatched[0].LineNumber);
		}

	}

}
