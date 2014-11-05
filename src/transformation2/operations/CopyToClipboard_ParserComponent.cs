using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.impl;
using LibHTreeProcessing.src.transformation2.operations;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public class CopyToClipboard_ParserComponent : AbstractOperationParserComponent
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

		public CopyToClipboard_ParserComponent()
			: base(EnumDataType.SingleText, EnumDataType.SingleAttribute, EnumDataType.SingleNode)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"copy to clipboard at \"/somepath\""
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractOperation TryParse(IParsingContext ctx, TokenStream tokens)
		{
			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("copy"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchWord("clipboard"),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[4].Text, tokens.LineNumber);

			string path = tokensMatched[4].Text;
			if (!path.StartsWith("/")
				|| (path.IndexOf("//") >= 0)
				|| (path.IndexOf("\\") >= 0)
				) throw ScriptException.CreateError_InvalidNodePathSpecified(tokensMatched[4].LineNumber, path);

			return new CopyToClipboard_Operation(tokensMatched[0].LineNumber, path);
		}

	}

}
