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


namespace LibHTreeProcessing.src.transformation2.cmds
{

	public class LoadXmlFileIntoClipboard_ParserComponent : AbstractScriptCommandParserComponent
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

		public LoadXmlFileIntoClipboard_ParserComponent()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override string[] ShortHelp
		{
			get {
				return new string[] {
					"load xml file \"filePath\" into clipboard at \"/somepath\""
				};
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override AbstractScriptCommand TryParse(IParsingContext ctx, TokenStream tokens)
		{
			Token[] tokensMatched;

			if ((tokensMatched = tokens.TryEatSequence(
				TokenPattern.MatchWord("load"),
				TokenPattern.MatchWord("xml"),
				TokenPattern.MatchWord("file"),
				TokenPattern.MatchAnyStringDQ(),
				TokenPattern.MatchWord("into"),
				TokenPattern.MatchWord("clipboard"),
				TokenPattern.MatchWord("at"),
				TokenPattern.MatchAnyStringDQ()
				)) == null)
				return null;

			ParsingUtils.VerifyString(tokensMatched[3].Text, tokens.LineNumber);

			if (!System.IO.File.Exists(tokensMatched[3].Text)) {
				throw ScriptException.CreateError_FileNotFound(tokensMatched[3].LineNumber, tokensMatched[3].Text);
			}
			HElement fileData;
			try {
				fileData = HToolkit.LoadXmlFromFile(tokensMatched[3].Text, false);
			} catch (Exception ee) {
				throw ScriptException.CreateError_FailedToLoadFile(tokensMatched[3].LineNumber, tokensMatched[3].Text, ee.Message);
			}

			ParsingUtils.VerifyString(tokensMatched[7].Text, tokens.LineNumber);

			string path = tokensMatched[7].Text;
			if (!path.StartsWith("/")
				|| (path.IndexOf("//") >= 0)
				|| (path.IndexOf("\\") >= 0)
				) throw ScriptException.CreateError_InvalidNodePathSpecified(tokensMatched[7].LineNumber, path);

			return new LoadXmlFileIntoClipboard_Operation(tokensMatched[0].LineNumber, fileData, path);
		}

	}

}
