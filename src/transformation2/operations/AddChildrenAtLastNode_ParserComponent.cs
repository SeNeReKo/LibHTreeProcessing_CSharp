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

	public class AddChildrenAtLastNode_ParserComponent : AbstractOperationParserComponent
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

		public AddChildrenAtLastNode_ParserComponent()
			: base(EnumDataType.SingleNode)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"add children at last node",
					"add children at last node \"nodeName\""
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
				TokenPattern.MatchWord("add"),
				TokenPattern.MatchWord("children"),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchWord("last"),
				TokenPattern.MatchWord("node")
				)) == null)
				return null;

			Token tokenNodeName = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
			if (tokenNodeName == null) {
				return new AddChildrenAtLastNode_Operation(lineNo, null);
			} else {
				ParsingUtils.VerifyString(tokenNodeName.Text, tokenNodeName.LineNumber);
				return new AddChildrenAtLastNode_Operation(lineNo, tokenNodeName.Text);
			}
		}

	}

}
