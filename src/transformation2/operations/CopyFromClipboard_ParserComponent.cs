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

	public class CopyFromClipboard_ParserComponent : AbstractOperationParserComponent
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

		public CopyFromClipboard_ParserComponent()
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
					"copy from clipboard \"/somepath\""
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
				TokenPattern.MatchWord("copy"),
				TokenPattern.MatchWord("from"),
				TokenPattern.MatchWord("clipboard"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[3].Text, tokens.LineNumber);

			string path = tokensMatched[3].Text;
			if (!path.StartsWith("/")
				|| (path.IndexOf("//") >= 0)
				|| (path.IndexOf("\\") >= 0)
				) throw ScriptException.CreateError_NotAValidExpressionSpecified(tokensMatched[3].LineNumber, path);
			if (!path.StartsWith("/clipboard/")) path = "/clipboard" + path;

			HExpression he = ParsingUtils.ParseExpression(path, lineNo, false);

			return new CopyFromClipboard_Operation(tokensMatched[0].LineNumber, he);
		}

	}

}
