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

	public class GroupNodeSequences_ParserComponent : AbstractOperationParserComponent
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

		public GroupNodeSequences_ParserComponent()
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
					"group node sequences { \"nodeNameA\", \"nodeNameB\", ... } using \"newNodeName\""
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

			TokenStream.IMarker mark = tokens.Mark();

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("group"),
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchWord("sequences")
				)) == null)
				return null;

			List<string> elements = ParsingUtils.EatStringGroup(tokens, tokens.LineNumber);
			if (elements == null) {
				mark.Reset();
				return null;
			}

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("using"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				throw new Exception("Syntax error in line " + tokens.LineNumber + ": \"using <string>\" expected!");

			ParsingUtils.VerifyString(tokensMatched[1].Text, tokensMatched[1].LineNumber);

			return new GroupNodeSequences_Operation(lineNo, elements, tokensMatched[1].Text);
		}

	}

}
