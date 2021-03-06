﻿using System;
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

	public class MergeTextChunks_ParserComponent : AbstractOperationParserComponent
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

		public MergeTextChunks_ParserComponent()
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
					"merge text chunks"
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will merge all consecuting text chunks at the node received. The result of this merge operation"
					+ " will replace the original text chunks."
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
				TokenPattern.MatchWord("merge"),
				TokenPattern.MatchWord("text"),
				TokenPattern.MatchWord("chunks")
				)) == null)
				return null;

			return new MergeTextChunks_Operation(lineNo);
		}

	}

}
