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

	public class ConvertTextToLowerCase_ParserComponent : AbstractOperationParserComponent
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

		public ConvertTextToLowerCase_ParserComponent()
			: base(EnumDataType.SingleText)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"convert to lower case"
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"Letter by letter this operator will convert the text received to lower case. The existing data will be"
					+ " replaced by a lower case version."
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
				TokenPattern.MatchWord("convert"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("lower"),
				TokenPattern.MatchWord("case")
				)) == null)
				return null;

			return new ConvertTextToLowerCase_Operation(lineNo);
		}

	}

}
