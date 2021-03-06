﻿using System;
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

	public class MoveToClipboard_ParserComponent : AbstractOperationParserComponent
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

		public MoveToClipboard_ParserComponent()
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
					"move to clipboard at \"/somepath\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator moved the data received to the clipboard at the specified position. The data to move can be any kind of"
					+ " data. Please have in mind that after that operation this data will no longer be contained in the node received."
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
				TokenPattern.MatchWord("move"),
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
				) throw ScriptException.CreateError_InvalidNodePathSpecified(tokensMatched[2].LineNumber, path);

			return new MoveToClipboard_Operation(tokensMatched[0].LineNumber, path);
		}

	}

}
