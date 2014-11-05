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

	public class RemoveTextSpacesBeforeDelimiters_ParserComponent : AbstractOperationParserComponent
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

		public RemoveTextSpacesBeforeDelimiters_ParserComponent()
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
					"remove text spaces before delimiters \"...some.punctuations...\""
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractOperation TryParse(IParsingContext ctx, TokenStream tokens)
		{
			int lineNo = tokens.LineNumber;

			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				   TokenPattern.MatchWord("remove"),
				   TokenPattern.MatchWord("text"),
				   TokenPattern.MatchWord("spaces"),
				   TokenPattern.MatchWord("before"),
				   TokenPattern.MatchWord("delimiters"),
				   TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			if (tokensMatched == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[5].Text, tokens.LineNumber);

			return new RemoveTextSpacesBeforeDelimiters_Operation(lineNo, tokensMatched[5].Text);
		}

	}

}
