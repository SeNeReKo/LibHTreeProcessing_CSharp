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

	public class SelectAllTexts_ParserComponent : AbstractSelectorParserComponent
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

		public SelectAllTexts_ParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"select all text chunks",
					"select all text chunks \"/pathexpr\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This selector will select all text chunks according to the path specified."
				};
			}
		}

		public override EnumDataType[] OutputDataTypes
		{
			get {
				return new EnumDataType[] {
					EnumDataType.SingleText
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractSelector TryParse(IParsingContext ctx, TokenStream tokens)
		{
			int lineNo = tokens.LineNumber;

			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("select"),
				TokenPattern.MatchWord("all"),
				TokenPattern.MatchWord("text"),
				TokenPattern.MatchWord("chunks")
				)) == null)
				return null;

			Token token = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
			if (token == null) {
				return new SelectAllTexts_Selector(lineNo, null);
			} else {
				return new SelectAllTexts_Selector(lineNo, ParsingUtils.ParseExpression(token.Text, token.LineNumber, false));
			}
		}

	}

}
