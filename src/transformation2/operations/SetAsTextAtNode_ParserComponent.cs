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

	public class SetAsTextAtNode_ParserComponent : AbstractOperationParserComponent
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

		public SetAsTextAtNode_ParserComponent()
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
					"set as text at node \"/some/path\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will reveive either text chunks or attributes. It will use that data as a value: It will select the node as specified,"
					+ " clear all text from it and then add a new text chunk containing that value."
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
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			HExpression he = ParsingUtils.ParseExpression(tokensMatched[5].Text, tokensMatched[5].LineNumber, false);
			if (he.DetermineSelectedElementType() != EnumElementType.Node) {
				throw ScriptException.CreateError_InvalidNodePathSpecified(tokensMatched[5].LineNumber, tokensMatched[5].Text);
			}

			return new SetAsTextAtNode_Operation(lineNo, he);
		}

	}

}
