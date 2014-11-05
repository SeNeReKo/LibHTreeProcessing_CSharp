using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public class TransformEntitiesByClipboardMap_ParserComponent : AbstractFilterParserComponent
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

		public TransformEntitiesByClipboardMap_ParserComponent()
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
					"transform entities by clipboard map \"somepath\""
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractFilter TryParse(IParsingContext ctx, TokenStream tokens)
		{
			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("transform"),
				TokenPattern.MatchWord("entities"),
				TokenPattern.MatchWord("by"),
				TokenPattern.MatchWord("clipboard"),
				TokenPattern.MatchWord("map"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			// ----

			HExpression expression = ParsingUtils.ParseExpression(tokensMatched[5].Text, tokens.LineNumber, false);

			Dictionary<string, EnumElementType> emitIDs;
			EnumElementType lastElementType;
			expression.CollectEmitIDs(out emitIDs, out lastElementType);
			if (emitIDs.Count > 0) {
				ScriptException.CreateError_Unknown(tokensMatched[5].LineNumber,
					"Expression has emit IDs!");
			}
			if (lastElementType != EnumElementType.Node) {
				ScriptException.CreateError_Unknown(tokensMatched[5].LineNumber,
					"Last match in expression does not specify matching an element!");
			}

			expression = ParsingUtils.ParseExpression("/clipboard" + tokensMatched[5].Text, tokens.LineNumber, false);

			// ----

			// ParsingUtils.VerifyString(tokensMatched[7].Text, tokens.LineNumber);

			// ----

			return new TransformEntitiesByClipboardMap_Operation(tokensMatched[0].LineNumber, expression);
		}

	}

}
