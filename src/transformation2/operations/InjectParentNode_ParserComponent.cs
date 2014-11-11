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

	public class InjectParentNode_ParserComponent : AbstractOperationParserComponent
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

		public InjectParentNode_ParserComponent()
			: base(EnumDataType.SingleNode, EnumDataType.SingleText)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"inject parent node \"newNodeName\""
				};
			}
		}


		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will create a new parent node for the node selected. Please have in mind that a root node is a special case:"
					+ " Root nodes do not have parents. Injection neverless will work for root nodes."
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
				TokenPattern.MatchWord("inject"),
				TokenPattern.MatchWord("parent"),
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[3].Text, tokensMatched[3].LineNumber);

			return new InjectParentNode_Operation(lineNo, tokensMatched[3].Text);
		}

	}

}
