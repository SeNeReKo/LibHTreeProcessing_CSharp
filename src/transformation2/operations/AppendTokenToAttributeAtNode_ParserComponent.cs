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

	public class AppendTokenToAttributeAtNode_ParserComponent : AbstractOperationParserComponent
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

		public AppendTokenToAttributeAtNode_ParserComponent()
			: base(EnumDataType.SingleAttribute, EnumDataType.SingleText)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"append token to attribute at \"somepath\"",
					"append token to attribute \"attrName\" at \"nodepath\""
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
				TokenPattern.MatchWord("append"),
				TokenPattern.MatchWord("token"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("attribute"),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchAnyStringDQ()
				)) != null) {

				HExpression e = ParsingUtils.ParseExpression(tokensMatched[5].Text, tokensMatched[5].LineNumber, false);
				return new AppendTokenToAttributeAtNode_Operation(lineNo, e);
			} else
			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("append"),
				TokenPattern.MatchWord("token"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("attribute"),
				TokenPattern.MatchAnyStringDQ(),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchAnyStringDQ()
				)) != null) {

				ParsingUtils.VerifyString(tokensMatched[4].Text, tokensMatched[4].LineNumber);
				HExpression e = ParsingUtils.ParseExpression(tokensMatched[6].Text, tokensMatched[6].LineNumber, false);
				return new AppendTokenToAttributeAtNode_Operation(lineNo, e, tokensMatched[4].Text);
			}

			return null;
		}

	}

}
