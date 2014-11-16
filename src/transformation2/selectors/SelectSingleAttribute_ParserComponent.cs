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

	public class SelectSingleAttribute_ParserComponent : AbstractSelectorParserComponent
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

		public SelectSingleAttribute_ParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"select single attribute \"/pathexpr\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This selector will select a single attribute according to the path specified. Please have in mind that path expressions"
					+ " will result in node or text chunks by default. Use naming to select an attribute."
				};
			}
		}

		public override EnumDataType[] OutputDataTypes
		{
			get {
				return new EnumDataType[] {
					EnumDataType.SingleAttribute
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
				TokenPattern.MatchWord("attribute"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			return new SelectSingleAttribute_Selector(
				lineNo,
				ParsingUtils.ParseExpression(tokensMatched[3].Text,
				tokensMatched[3].LineNumber,
				false));
		}

	}

}
