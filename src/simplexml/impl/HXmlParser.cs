using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;


namespace LibHTreeProcessing.src.simplexml.impl
{

	/*
	public class HXmlParser
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

		public HXmlParser()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private static bool __TryEat_XML_ENDTOKEN(MyTokenizer tokenizer, string tokenName)
		{
			MyChar c = tokenizer.Read();
			if (c == null) return false;

			MyToken word = tokenizer.TryEatXmlWord();
			if (word == null) {
				tokenizer.Unread(c);
				return false;
			}
			if (!word.Text.Equals(tokenName)) {
				tokenizer.Unread(word);
				tokenizer.Unread(c);
				return false;
			}

			__TryEat_OPTIONAL_IRRELEVANT_WHITESPACES(tokenizer);
		}

		private static string __TryEat_XML_TEXT(MyTokenizer tokenizer)
		{
			StringBuilder sb = new StringBuilder();

			while (true) {
				MyChar c = tokenizer.Read();
				if (c == null) {
					if (sb.Length > 0) return sb.ToString();
					else return null;
				}
				if (c.C == '<') {
					tokenizer.Unread(c);
					if (sb.Length > 0) return sb.ToString();
					else return null;
				}
				if (c.C == '>') {
					throw HXmlException.CreateError_InvalidCharacterDetected(tokenizer.LineNumber, tokenizer.Position, '>');
				}
				sb.Append(c.C);
			}
		}

		private static string __TryEat_COMMENT(MyTokenizer tokenizer)
		{
			if (!tokenizer.TryEatSequence("<!--")) return null;
			StringBuilder sb = new StringBuilder();

			while (true) {
				if (tokenizer.IsEOS) throw HXmlException.CreateError_UnexpectedEOS(tokenizer.LineNumber, tokenizer.Position);

				if (tokenizer.TryEatSequence("-->")) {
					// end of comment reached
					sb.Append();
					return sb.ToString();
				}

				MyChar c = tokenizer.Read();
				sb.Append(c.C);
			}
		}

		private static string __TryEat_DOCTYPE(MyTokenizer tokenizer)
		{
			if (!tokenizer.TryEatSequence("<!DOCTYPE")) return null;
			StringBuilder sb = new StringBuilder("<!DOCTYPE");

			while (true) {
				if (tokenizer.IsEOS) throw HXmlException.CreateError_UnexpectedEOS(tokenizer.LineNumber, tokenizer.Position);

				MyToken t = tokenizer.TryEatXmlString();
				if (t != null) {
					sb.Append('\"');
					sb.Append(t.Text);
					sb.Append('\"');
					continue;
				}

				MyChar c = tokenizer.Read();
				if (c.C == '>') {
					// end of doctype declaration reached
					sb.Append('>');
					return sb.ToString();
				}

				sb.Append(c.C);
			}
		}

		private static void __TryEat_OPTIONAL_IRRELEVANT_WHITESPACES(MyTokenizer tokenizer)
		{
			tokenizer.TryEatWhiteSpaces();
		}

		private static void __TryEat_MANDATORY_IRRELEVANT_WHITESPACES(MyTokenizer tokenizer)
		{
			int n = tokenizer.TryEatWhiteSpaces();
			if (n == 0) throw HXmlException.CreateError_WhiteSpaceExpected(tokenizer.LineNumber, tokenizer.Position);
		}

	}
	*/

}
