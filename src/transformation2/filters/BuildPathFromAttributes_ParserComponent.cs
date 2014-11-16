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

	public class BuildPathFromAttributes_ParserComponent : AbstractFilterParserComponent
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

		public BuildPathFromAttributes_ParserComponent()
			: base(EnumDataType.SingleNode, EnumDataType.SingleAttribute)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"build path based on attribute \"sometext\" with delimiter \"delim\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This filter takes a node, determines all ancestors, and builds a path based on the specified attribute."
					+ " A text chunk is then created and returned."
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractFilter TryParse(IParsingContext ctx, TokenStream tokens)
		{
			int lineNo = tokens.LineNumber;

			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("build"),
				TokenPattern.MatchWord("path"),
				TokenPattern.MatchWord("based"),
				TokenPattern.MatchWord("on"),
				TokenPattern.MatchWord("attribute"),
				TokenPattern.MatchAnyStringDQ(),
				TokenPattern.MatchWord("with"),
				TokenPattern.MatchWord("delimiter"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[5].Text, tokensMatched[5].LineNumber);

			return new BuildPathFromAttributes_Operation(lineNo, tokensMatched[5].Text, tokensMatched[8].Text);
		}

	}

}
