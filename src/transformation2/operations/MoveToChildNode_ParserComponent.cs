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

	public class MoveToChildNode_ParserComponent : AbstractOperationParserComponent
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

		public MoveToChildNode_ParserComponent()
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
					"move to child node \"/some/path\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator moves the element received to the node specified. The elements moved can be attributes or a text chunks."
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
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("child"),
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[4].Text, tokensMatched[4].LineNumber);
			HExpression expression = ParsingUtils.ParseExpression(tokensMatched[4].Text, tokensMatched[4].LineNumber, false);

			Dictionary<string, EnumElementType> emitIDCollection;
			EnumElementType lastElement;
			expression.CollectEmitIDs(out emitIDCollection, out lastElement);

			if (emitIDCollection.Count > 1) {
				ScriptException.CreateError_MoreThanOneEmitIDsSpecified(lineNo);
			} else
			if (emitIDCollection.Count == 1) {
				lastElement = emitIDCollection.First().Value;
			}
			if (lastElement != EnumElementType.Node) {
				throw ScriptException.CreateError_NodeExpressionRequired(lineNo, tokensMatched[2].Text);
			}

			return new MoveToChildNode_Operation(lineNo, expression);
		}

	}

}
