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

	public class FilterConvertToLowerCase_ParserComponent : AbstractFilterParserComponent
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

		public FilterConvertToLowerCase_ParserComponent()
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
					"filter convert to lower case"
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"Letter by letter this filter will convert the text received to lower case. A new item of the same input type (with lower case"
					+ " data) is created and returned."
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
				TokenPattern.MatchWord("convert"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("lower"),
				TokenPattern.MatchWord("case")
				)) == null)
				return null;

			return new FilterConvertToLowerCase_Operation(lineNo);
		}

	}

}
