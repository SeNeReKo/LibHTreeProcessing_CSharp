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

	public class Invoke_ParserComponent : AbstractOperationParserComponent
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

		public Invoke_ParserComponent()
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
					"invoke myProcedureName(node)"
				};
			}
		}


		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will invoke the procedure specified. While doing so the selected node will be considered to be a"
					+ " (virtual) root node: The procedure will not see any nodes above the selected node."
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
				TokenPattern.MatchWord("invoke"),
				TokenPattern.MatchAnyWord(),
				TokenPattern.MatchDelimiter('('),
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchDelimiter(')')
				)) == null)
				return null;

			return new Invoke_Operation(lineNo, tokensMatched[1].Text);
		}

	}

}
