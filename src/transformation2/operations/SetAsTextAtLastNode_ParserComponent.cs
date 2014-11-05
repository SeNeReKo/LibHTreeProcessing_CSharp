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

	public class SetAsTextAtLastNode_ParserComponent : AbstractOperationParserComponent
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

		public SetAsTextAtLastNode_ParserComponent()
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
					"set as text at last node",
					"set as text at last node \"nodeName\""
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
				TokenPattern.MatchWord("set"),
				TokenPattern.MatchWord("as"),
				TokenPattern.MatchWord("text"),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchWord("last"),
				TokenPattern.MatchWord("node")
				)) == null)
				return null;

			Token tokenNodeName = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
			if (tokenNodeName == null) {
				return new SetAsTextAtLastNode_Operation(lineNo, null);
			} else {
				ParsingUtils.VerifyString(tokenNodeName.Text, tokenNodeName.LineNumber);
				return new SetAsTextAtLastNode_Operation(lineNo, tokenNodeName.Text);
			}
		}

	}

}
