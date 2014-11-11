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

	public class SetAsAttributeAtNode_ParserComponent : AbstractOperationParserComponent
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

		public SetAsAttributeAtNode_ParserComponent()
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
					"set as attribute \"attrName\" at node \"/some/path\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will reveive either text chunks or attributes. It will use that data as a value: It will select the node as specified"
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
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			string attrName = tokensMatched[3].Text;
			ParsingUtils.VerifyString(attrName, tokensMatched[3].LineNumber);

			HExpression he = ParsingUtils.ParseExpression(tokensMatched[6].Text, tokensMatched[6].LineNumber, false);
			if (he.DetermineSelectedElementType() != EnumElementType.Node) {
				throw ScriptException.CreateError_InvalidNodePathSpecified(tokensMatched[6].LineNumber, tokensMatched[6].Text);
			}

			return new SetAsAttributeAtNode_Operation(lineNo, he, attrName);
		}

	}

}
