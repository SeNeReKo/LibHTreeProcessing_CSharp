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

	public class SelectSingleText_ParserComponent : AbstractSelectorParserComponent
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

		public SelectSingleText_ParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"select single text chunk \"/pathexpr\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This selector will select a single text chunk according to the path specified."
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
				TokenPattern.MatchWord("single"),
				TokenPattern.MatchWord("text"),
				TokenPattern.MatchWord("chunk"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			return new SelectSingleText_Selector(
				lineNo,
				ParsingUtils.ParseExpression(tokensMatched[4].Text, tokensMatched[4].LineNumber, false)
				);
		}

	}

}
