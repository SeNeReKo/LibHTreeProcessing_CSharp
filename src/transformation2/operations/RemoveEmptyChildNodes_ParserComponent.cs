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

	public class RemoveEmptyChildNodes_ParserComponent : AbstractOperationParserComponent
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

		public RemoveEmptyChildNodes_ParserComponent()
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
					"remove empty child nodes"
				};
			}
		}

		public override string[] LongHelpText
		{
			get {
				return new string[] {
					"This operator will scan the cildren of the nodes received for empty nodes and empty text chunks. If found these elements"
					+ " will be removed. Text chunks are considered to be empty if they contain text data of length zero. Nodes are considered"
					+ " to be empty if they contain no attributes and no children."
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
				TokenPattern.MatchWord("empty"),
				TokenPattern.MatchWord("child"),
				TokenPattern.MatchWord("nodes")
				)) == null)
				return null;

			return new RemoveEmptyChildNodes_Operation(lineNo);
		}

	}

}
