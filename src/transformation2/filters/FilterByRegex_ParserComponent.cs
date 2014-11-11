using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public class FilterByRegex_ParserComponent : AbstractFilterParserComponent
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

		public FilterByRegex_ParserComponent()
			: base(EnumDataType.SingleText)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"filter text by regex \"regex\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This filter tries to match the received value of a text chunk or attribute by the specified regular expression. The first group"
					+ " specified in the regular expression will form the new output of the filter. If no such group exists or too many groups are specified "
					+ " an error will occur at runtime. If no match occurred, there will be no output. If a match occurs, an output element of the same type"
					+ " as the input element will be created."
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
				TokenPattern.MatchWord("text"),
				TokenPattern.MatchWord("by"),
				TokenPattern.MatchWord("regex"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			Regex regex = ParsingUtils.ParseRegEx(tokensMatched[4].Text, lineNo);

			return new FilterByRegex_Operation(lineNo, regex);
		}

	}

}
