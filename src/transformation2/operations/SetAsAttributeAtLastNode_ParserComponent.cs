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

	public class SetAsAttributeAtLastNode_ParserComponent : AbstractOperationParserComponent
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

		public SetAsAttributeAtLastNode_ParserComponent()
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
					"set as attribute \"attributeName\" at last node",
					"set as attribute \"attributeName\" at last node \"nodeName\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will reveive either text chunks or attributes. ItIt will use that data as a value: It will select the last node as specified"
					+ " and then set the specified attribute to that value. If no such attribute yet exists a new one is created."
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
				TokenPattern.MatchWord("attribute"),
				TokenPattern.MatchAnyStringDQ(),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchWord("last"),
				TokenPattern.MatchWord("node")
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[3].Text, tokensMatched[3].LineNumber);

			Token tokenNodeName = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
			if (tokenNodeName == null) {
				return new SetAsAttributeAtLastNode_Operation(lineNo, null, tokensMatched[3].Text);
			} else {
				ParsingUtils.VerifyString(tokenNodeName.Text, tokenNodeName.LineNumber);
				return new SetAsAttributeAtLastNode_Operation(lineNo, tokenNodeName.Text, tokensMatched[3].Text);
			}
		}

	}

}
