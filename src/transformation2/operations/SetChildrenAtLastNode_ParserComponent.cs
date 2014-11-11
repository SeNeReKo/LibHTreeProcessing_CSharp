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

	public class SetChildrenAtLastNode_ParserComponent : AbstractOperationParserComponent
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

		public SetChildrenAtLastNode_ParserComponent()
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
					"set children at last node",
					"set children at last node \"nodeName\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will identify the last node as specied, remove all children (= text chunks and nodes) of that node,"
					+ " and then populate this node with clones of all children of the node received."
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
				TokenPattern.MatchWord("children"),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchWord("last"),
				TokenPattern.MatchWord("node")
				)) == null)
				return null;

			Token tokenNodeName = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
			if (tokenNodeName == null) {
				return new SetChildrenAtLastNode_Operation(lineNo, null);
			} else {
				ParsingUtils.VerifyString(tokenNodeName.Text, tokenNodeName.LineNumber);
				return new SetChildrenAtLastNode_Operation(lineNo, tokenNodeName.Text);
			}
		}

	}

}
