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

	public class FilterAddPrefix_ParserComponent : AbstractFilterParserComponent
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

		public FilterAddPrefix_ParserComponent()
			: base(EnumDataType.SingleText, EnumDataType.SingleAttribute)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"filter add prefix \"sometext\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This filter adds a static prefix text to the item received. A new item of the same type containing the modified text"
					+ " is created and returned."
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
				TokenPattern.MatchWord("filter"),
				TokenPattern.MatchWord("add"),
				TokenPattern.MatchWord("prefix"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			return new FilterAddPrefix_Operation(lineNo, tokensMatched[3].Text);
		}

	}

}
