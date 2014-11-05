using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class ScriptTokenProvider : IEnumerable<Token>
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		private static TokenPattern[] COMMENT_BEGIN_PATTERN = new TokenPattern[] {
			TokenPattern.MatchDelimiter('/'),
			TokenPattern.MatchDelimiter('*')
			};

		private static TokenPattern[] COMMENT_END_PATTERN = new TokenPattern[] {
			TokenPattern.MatchDelimiter('*'),
			TokenPattern.MatchDelimiter('/')
			};

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		Token[] tokens;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public ScriptTokenProvider(string content)
		{
			string[] textLines;
			if (content.IndexOf("\r\n") >= 0) {
				textLines = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			} else {
				textLines = content.Split(new string[] { "\n" }, StringSplitOptions.None);
			}

			Tokenizer tokenizer = new Tokenizer(false, Tokenizer.EnumSpaceProcessing.SkipAllSpaces, true, null);

			TokenPattern tpS = TokenPattern.MatchDelimiter('/');
			TokenPattern tpH = TokenPattern.MatchDelimiter('#');

			List<Token[]> lines = new List<Token[]>();

			int lineNo = 0;
			foreach (string line in textLines) {
				lineNo++;

				Token[] tokens = tokenizer.Tokenize(lineNo, line);

				// strip EOS

				tokens = SubArray(tokens, 0, tokens.Length - 1);

				// convert lines to token arrays and remove line comment markers

				int count = tokens.Length;

				for (int i = 0; i < tokens.Length; i++) {
					if (i < tokens.Length - 1) {
						if (tpS.Match(tokens[i]) && tpS.Match(tokens[i + 1])) {
							count = i;
							break;
						}
					}
					if (tpH.Match(tokens[i])) {
						count = i;
						break;
					}
				}

				if (tokens.Length == count) {
					lines.Add(tokens);
				} else {
					Token[] temp = new Token[count];
					for (int i = 0; i < count; i++) {
						temp[i] = tokens[i];
					}
					lines.Add(temp);
				}
			}

			// merge lines having a continue-identificator

			{
				int i = lines.Count - 1;
				while (i >= 0) {
					Token[] tokenLine = lines[i];
					if (tokenLine.Length == 0) {
						i--;
						continue;
					} else
					if (tokenLine[tokenLine.Length - 1].TokenIsDelimiter('\\')) {
						tokenLine = SubArray(tokenLine, 0, tokenLine.Length - 1);
						if (i < lines.Count - 1) {
							// regular line -> can join
							tokenLine = JoinArrays(tokenLine, lines[i + 1]);
							lines.RemoveAt(i + 1);
						}
						lines[i] = tokenLine;
						i--;
						continue;
					} else {
						i--;
						continue;
					}
				}
			}

			List<Token> myTokens = new List<Token>();
			int myLineNo = -1;
			int myPos = -1;
			foreach (Token[] line in lines) {
				foreach (Token t in line) {
					myTokens.Add(t);
					myLineNo = t.LineNumber;
					myPos = t.CharacterPosition + ((t.Text == null) ? 0 : t.Text.Length);
				}
				myTokens.Add(new Token(myLineNo, myPos, "" + (char)13, EnumGeneralTokenType.Delimiter));
			}

			// now remove block comments - we unfortunately haven't done that before

			List<Token> myTokensFinal = new List<Token>();

			TokenStream myTokenStream = new TokenStream(myTokens);
			bool bInBlockComment = false;
			while (!myTokenStream.IsEOS) {
				if (bInBlockComment) {
					if (myTokenStream.TryEatSequence(COMMENT_END_PATTERN) != null) {
						bInBlockComment = false;
					} else {
						myTokenStream.Skip(1);
					}
				} else {
					if (myTokenStream.TryEatSequence(COMMENT_BEGIN_PATTERN) != null) {
						bInBlockComment = true;
					} else {
						myTokensFinal.Add(myTokenStream.Read());
					}
				}
			}

			// return result

			this.tokens = myTokensFinal.ToArray();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private static Token[] SubArray(Token[] array, int pos, int len)
		{
			Token[] ret = new Token[len];
			for (int i = 0; i < len; i++) {
				ret[i] = array[pos + i];
			}
			return ret;
		}

		private static Token[] JoinArrays(params Token[][] arrays)
		{
			int len = 0;
			for (int i = 0; i < arrays.Length; i++) {
				len += arrays[i].Length;
			}
			Token[] ret = new Token[len];
			int pos = 0;
			for (int i = 0; i < arrays.Length; i++) {
				Token[] data = arrays[i];
				for (int j = 0; j < data.Length; j++) {
					ret[pos++] = data[j];
				}
			}
			return ret;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Token t in tokens) {
				yield return t;
			}
		}

		public IEnumerator<Token> GetEnumerator()
		{
			foreach (Token t in tokens) {
				yield return t;
			}
		}

	}

}
