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

	public class CloneNode_ParserComponent : AbstractOperationParserComponent
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

		public CloneNode_ParserComponent()
			: base(EnumDataType.SingleNode)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] { "clone node using { \"attrName\" = \"attrValue\", ... }" };
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractOperation TryParse(IParsingContext ctx, TokenStream tokens)
		{
			int lineNo = tokens.LineNumber;

			Token[] tokensMatched;

			tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("clone"),
				TokenPattern.MatchWord("node"),
				TokenPattern.MatchWord("using")
				);
			if (tokensMatched == null) return null;

			List<HAttribute> attributes = ParsingUtils.TryEatAttributeDefinitionGroup(tokens, tokens.LineNumber);
			if (attributes.Count < 2) {
				throw ScriptException.CreateError_Unknown(lineNo, "At least two attributes are required for cloning!");
			}

			return new CloneNode_Operation(lineNo, attributes);
		}

	}

}
