using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


using LibNLPCSharp.simpletokenizing;
using LibNLPCSharp.util;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public static class ParsingUtils
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

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/*
		public static HExpression ParsePatternExpression(string expression, int lineNo)
		{
			HExpression m = TryParsePatternExpression(expression);
			if (m == null) throw TransformationParseException.CreateErrorInvalidPatternExpressionSpecified(lineNo);
			return m;
		}

		public static HExpression TryParsePatternExpression(string expression)
		{
			try {
				HExpression m = HExpressionCompiler.Compile(expression, false);
				return m;
			} catch (Exception ee) {
				return null;
			}
		}
		*/

		public static Regex ParseRegEx(string s, int lineNo)
		{
			if ((s.Trim().Length != s.Length) || (s.Length == 0))
				throw ScriptException.CreateError_NotAValidRegExSpecified(lineNo, s);

			try {
				return new Regex(s, RegexOptions.Compiled);
			} catch (Exception ee) {
				throw ScriptException.CreateError_NotAValidRegExSpecified(lineNo, s);
			}
		}

		/// <summary>
		/// Verify a single string: If it contains spaces at the beginning or end of the expression or if it is empty
		/// an exception is thrown.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="lineNo"></param>
		public static HExpression ParseExpression(string s, int lineNo, bool bAllowRelativeExpressions)
		{
			if ((s.Trim().Length != s.Length) || (s.Length == 0))
				throw ScriptException.CreateError_NotAValidExpressionSpecified(lineNo, s);

			try {
				return HExpressionCompiler.Compile(s, bAllowRelativeExpressions);
			} catch (Exception ee) {
				throw ScriptException.CreateError_NotAValidExpressionSpecified(lineNo, s);
			}
		}

		/// <summary>
		/// Verify a single string: If it contains spaces at the beginning or end of the expression or if it is empty
		/// an exception is thrown.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="lineNo"></param>
		public static void VerifyString(string s, int lineNo)
		{
			if ((s.Trim().Length != s.Length) || (s.Length == 0))
				throw ScriptException.CreateError_InvalidStringSpecified(lineNo, s);
		}

		/// <summary>
		/// Eats an attribute definition group such as: { "bla" = "blubb", ... }
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="lineNo"></param>
		/// <returns></returns>
		public static List<HAttribute> EatAttributeDefinitionGroup(TokenStream tokens, int lineNo)
		{
			List<HAttribute> ret = TryEatAttributeDefinitionGroup(tokens, lineNo);
			if (ret == null)
				throw ScriptException.CreateError_AttributeDefinitionGroupExpected(lineNo);
			return ret;
		}

		/// <summary>
		/// Tries to eat an attribute definition group such as: { "bla" = "blubb", ... }
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="lineNo"></param>
		/// <returns></returns>
		public static List<HAttribute> TryEatAttributeDefinitionGroup(TokenStream tokens, int lineNo)
		{
			if (tokens.TryEat(TokenPattern.MatchDelimiter('{')) == null) return null;

			bool bFirst = true;
			List<HAttribute> array = new List<HAttribute>();
			while (tokens.TryEat(TokenPattern.MatchDelimiter('}')) == null) {
				if (tokens.IsEOS) throw ScriptException.CreateError_UnexpectedEOS(lineNo);

				if (bFirst) {
					bFirst = false;
				} else {
					if (tokens.TryEat(TokenPattern.MatchDelimiter(',')) == null)
						throw ScriptException.CreateError_CommaExpected(lineNo);
				}

				Token[] t = tokens.TryEatSequence(
					TokenPattern.MatchAnyStringDQ(),
					TokenPattern.MatchDelimiter('='),
					TokenPattern.MatchAnyStringDQ());
				if (t == null)
					throw ScriptException.CreateError_AttributeDefinitionGroupExpected(lineNo);
				VerifyString(t[0].Text, lineNo);
				VerifyString(t[2].Text, lineNo);

				array.Add(new HAttribute(t[0].Text, t[2].Text));
			}

			return array;
		}

		/// <summary>
		/// Eats a string group such as: { "bla", "blubb", ... }
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="lineNo"></param>
		/// <returns></returns>
		public static List<string> EatStringGroup(TokenStream tokens, int lineNo)
		{
			List<string> ret = TryEatStringGroup(tokens, lineNo);
			if (ret == null) throw ScriptException.CreateError_StringGroupExpected(lineNo);
			return ret;
		}

		/// <summary>
		/// Tries to eat a string group such as: { "bla", "blubb", ... }
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="lineNo"></param>
		/// <returns></returns>
		public static List<string> TryEatStringGroup(TokenStream tokens, int lineNo)
		{
			if (tokens.TryEat(TokenPattern.MatchDelimiter('{')) == null) return null;

			bool bFirst = true;
			List<string> array = new List<string>();
			while (tokens.TryEat(TokenPattern.MatchDelimiter('}')) == null) {
				if (tokens.IsEOS) throw ScriptException.CreateError_UnexpectedEOS(lineNo);
				
				if (bFirst) {
					bFirst = false;
				} else {
					if (tokens.TryEat(TokenPattern.MatchDelimiter(',')) == null)
						throw ScriptException.CreateError_CommaExpected(lineNo);
				}

				Token t = tokens.TryEat(TokenPattern.MatchAnyStringDQ());
				if (t == null)
					throw ScriptException.CreateError_StringExpected(lineNo);
				VerifyString(t.Text, lineNo);

				array.Add(t.Text);
			}

			return array;
		}

		public static HExpression EatPatternExpression(TokenStream tokens, int lineNo)
		{
			HExpression m = TryEatPatternExpression(tokens, lineNo);
			if (m == null) throw ScriptException.CreateError_PatternExpressionExpected(lineNo);
			return m;
		}

		/// <summary>
		/// Tries to eat a pattern expression such as: { pattern "/path" }
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="lineNo"></param>
		/// <returns></returns>
		public static HExpression TryEatPatternExpression(TokenStream tokens, int lineNo)
		{
			if (tokens.TryEat(TokenPattern.MatchDelimiter('{')) == null) return null;

			Token[] tokensMatched;

			tokensMatched = tokens.TryEatSequence(
			   TokenPattern.MatchWord("pattern"),
			   TokenPattern.MatchAnyStringDQ(true),
			   TokenPattern.MatchDelimiter("}")
			   );
			if (tokensMatched != null) {
				return ParseExpression(tokensMatched[1].Text, lineNo, false);
			}

			return null;
		}

		public static void BuildShortHelpText<T>(
			IEnumerable<IParserComponent<T>> parserComponents,
			string prefix,
			StringBuilder sb)
			where T : class
		{
			IParserComponent<T>[] list = (new List<IParserComponent<T>>(parserComponents)).ToArray();
			Array.Sort(list, new TransformationRuleParserComponentSorter<T>());

			foreach (IParserComponent<T> a in list) {
				bool bFirst = true;
				foreach (string s in a.ShortHelp) {
					sb.Append(Util.CRLF);
					if (prefix!= null) sb.Append(prefix);
					if (bFirst) {
						sb.Append("»  ");
						bFirst = false;
					} else {
						sb.Append("    ");
					}
					sb.Append(s);
				}
			}
			sb.Append(Util.CRLF);
		}

	}

}
