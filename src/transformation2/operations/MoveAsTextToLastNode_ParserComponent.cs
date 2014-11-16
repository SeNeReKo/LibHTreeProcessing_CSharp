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

	public class MoveAsTextToLastNode_ParserComponent : AbstractOperationParserComponent
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

		public MoveAsTextToLastNode_ParserComponent()
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
					"move as text to last node",
					"move as text to last node \"nodeName\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will receive either text chunks or attributes. It will use that data as a value: It will select the last node as specified,"
					+ " clear all text from it and then move the text value as a new text chunk to that node."
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
				TokenPattern.MatchWord("move"),
				TokenPattern.MatchWord("as"),
				TokenPattern.MatchWord("text"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("last"),
				TokenPattern.MatchWord("node")
				)) == null)
				return null;

			Token tokenNodeName = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
			if (tokenNodeName == null) {
				return new MoveAsTextToLastNode_Operation(lineNo, null);
			} else {
				ParsingUtils.VerifyString(tokenNodeName.Text, tokenNodeName.LineNumber);
				return new MoveAsTextToLastNode_Operation(lineNo, tokenNodeName.Text);
			}
		}

	}

}
