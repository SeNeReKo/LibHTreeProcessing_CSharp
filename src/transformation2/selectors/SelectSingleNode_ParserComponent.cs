using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.selectors
{

	public class SelectSingleNode_ParserComponent : AbstractSelectorParserComponent
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

		public SelectSingleNode_ParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"select single node \"/pathexpr\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This selector will select a single node according to the path specified."
				};
			}
		}

		public override EnumDataType[] OutputDataTypes
		{
			get {
				return new EnumDataType[] {
					EnumDataType.SingleNode
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractSelector TryParse(IParsingContext ctx, TokenStream tokens)
		{
			int lineNo = tokens.LineNumber;

			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("select"),
				TokenPattern.MatchWord("single"),
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			return new SelectSingleNode_Selector(
				lineNo,
				ParsingUtils.ParseExpression(tokensMatched[3].Text, tokensMatched[3].LineNumber, false)
				);
		}

	}

}
