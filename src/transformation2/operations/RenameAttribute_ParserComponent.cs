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

	public class RenameAttribute_ParserComponent : AbstractOperationParserComponent
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

		public RenameAttribute_ParserComponent()
			: base(EnumDataType.SingleAttribute)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"rename attribute to \"newName\""
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will modify the attributes received and rename them to the name specified."
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
				TokenPattern.MatchWord("rename"),
				TokenPattern.MatchWord("attribute"),
				TokenPattern.MatchWord("to"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[3].Text, tokens.LineNumber);

			return new RenameAttribute_Operation(lineNo,tokensMatched[3].Text);
		}

	}

}
