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

	public class RemoveAllChildren_ParserComponent : AbstractOperationParserComponent
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

		public RemoveAllChildren_ParserComponent()
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
					"remove all children"
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will remove all child nodes and child text chunks of the nodes received. Only attributes will remain untouched."
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
				TokenPattern.MatchWord("remove"),
				TokenPattern.MatchWord("all"),
				TokenPattern.MatchWord("children")
				)) == null)
				return null;

			return new RemoveAllChildren_Operation(lineNo);
		}

	}

}
