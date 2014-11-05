using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.stringmatching;


namespace LibHTreeProcessing.src.treesearch
{

	public partial class HExpressionCompiler
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		private static Tokenizer TOKENIZER = new Tokenizer(false, Tokenizer.EnumSpaceProcessing.SkipAllSpaces, true, ":");

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

		/// <summary>
		/// Expression examples:
		/// /somenode
		/// /somenode/someothernode
		/// /somenode/someothernode[text()] ---- matches to paths starting with "somenode" followed by "someothernode" which have a text
		/// /somenode/node() ---- matches to paths that start with "somenode" with at least one child node
		/// An exception is thrown on error.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="bAllowRelativeExpressions">Not supported yet. Set this to false!</param>
		/// <returns></returns>
		public static HExpression Compile(string text, bool bAllowRelativeExpressions)
		{
			TokenStream ts = new TokenStream(TOKENIZER.Tokenize(text));

			int n = 0;

			HExpression matcher = new HExpression(text);

			while (!ts.IsEOS) {
				AbstractTreeNodeVisitor v = __TryEatPATHDELIM(text, ts);
				if (v == null) {
					if (bAllowRelativeExpressions && (n == 0)) {
						v = new RecursiveAnyNodeVisitor();
					} else {
						throw new ExpressionCompilerException(text, ts.CharacterPosition + 1);
					}
				} else {
					if (n == 0) {
						if (v is DirectChildVisitor) {
							v = new DirectNodeVisitor();
						} else {
							v = new RecursiveAnyNodeVisitor();
						}
					}
				}

				n++;

				TreeTextMatcher tm = __TryEatPATHELEMENTTEXT(text, ts);
				if (tm != null) {
					matcher.Add(v, tm);
					break;
				}

				TreeElementMatcher em = __TryEatPATHELEMENTNODEATTRSEQ(text, ts);
				if (em != null) {
					matcher.Add(v, em);
					continue;
				}

				throw new ExpressionCompilerException(text, ts.CharacterPosition + 1);
			}

			if (!ts.IsEOS || (n == 0)) throw new ExpressionCompilerException(text, ts.CharacterPosition + 1);

			return matcher;
		}

		////////////////////////////////////////////////////////////////

		/*
		private static ChainElement __TryEatPATH(string originalLine, TokenStream ts)
		{
			TreeTextMatcher tm = __TryEatPATHELEMENTTEXT(originalLine, ts);
			if (tm != null) return tm;

			TreeElementMatcher em = __TryEatPATHELEMENTNODEATTRSEQ(originalLine, ts);
			if (em != null) {
				AbstractTreeNodeVisitor v = __TryEatPATHDELIM(originalLine, ts);
				if (v != null) {
					AbstractTreeNodeMatcher m2 = __TryEatPATH(originalLine, ts);
					if (m2 == null) throw new ExpressionCompilerException(originalLine, ts.CharacterPosition + 1);

					em.ContentMatcher = AndElement.Create(em.ContentMatcher, v);
				}
				return em;
			}

			return null;
		}
		*/

		private static AbstractTreeNodeVisitor __TryEatPATHDELIM(string originalLine, TokenStream ts)
		{
			Token[] tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('/'),
				TokenPattern.MatchDelimiter('/')
				);
			if (tokens != null) {
				return new RecursiveChildVisitor();
			}

			tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('/')
				);
			if (tokens != null) {
				return new DirectChildVisitor();
			}

			return null;
		}

		////////////////////////////////////////////////////////////////

		private static TreeElementMatcher __TryEatPATHELEMENTNODEATTRSEQ(string originalLine, TokenStream ts)
		{
			TreeElementMatcher m = __TryEatPATHELEMENTNODE(originalLine, ts);
			if (m == null) return null;

			List<AttributeMatcher> list = __TryEatPATHELEMENTATTRSEQ(originalLine, ts);
			if (list != null) {
				m.SetAttributeMatchers(list.ToArray());
			}

			return m;
		}

		private static List<AttributeMatcher> __TryEatPATHELEMENTATTRSEQ(string originalLine, TokenStream ts)
		{
			List<AttributeMatcher> attributeVisitors = new List<AttributeMatcher>();

			AttributeMatcher av;
			while ((av = __TryEatPATHELEMENTATTR(originalLine, ts)) != null) {
				attributeVisitors.Add(av);
			}

			if (attributeVisitors.Count == 0) return null;
			return attributeVisitors;
		}

		private static TreeElementMatcher __TryEatPATHELEMENTNODE(string originalLine, TokenStream ts)
		{
			Token[] tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('§'),
				TokenPattern.MatchWord("node")
				);
			if (tokens != null) {
				AbstractStringMatcher stringMatcher = __TryEatSTRINGCHECKRIGHTPART(originalLine, ts);
				string emittanceID = __TryEatEMITTANCE(originalLine, ts);

				return new TreeElementMatcher(emittanceID, stringMatcher);
			}

			Token token = ts.TryEat(TokenPattern.MatchAnyWord());
			if (token != null) {
				string emittanceID = __TryEatEMITTANCE(originalLine, ts);

				return new TreeElementMatcher(emittanceID, new StringMatcherEquals(token.Text));
			}

			return null;
		}

		private static TreeTextMatcher __TryEatPATHELEMENTTEXT(string originalLine, TokenStream ts)
		{
			Token[] tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('§'),
				TokenPattern.MatchWord("text")
				);
			if (tokens == null) return null;

			AbstractStringMatcher stringMatcher = __TryEatSTRINGCHECKRIGHTPART(originalLine, ts);
			string emittanceID = __TryEatEMITTANCE(originalLine, ts);

			return new TreeTextMatcher(emittanceID, stringMatcher);
		}

		private static AttributeMatcher __TryEatPATHELEMENTATTR(string originalLine, TokenStream ts)
		{
			Token[] tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('[')
				);
			if (tokens == null) return null;

			tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('@'),
				TokenPattern.MatchAnyWord(true)
				);
			if (tokens == null) throw new ExpressionCompilerException(originalLine, ts.CharacterPosition + 1);

			string attrName = tokens[1].Text;
			AbstractStringMatcher stringMatcher = __TryEatSTRINGCHECKRIGHTPART(originalLine, ts);
			string emittanceID = __TryEatEMITTANCE(originalLine, ts);

			Token token = ts.TryEat(TokenPattern.MatchDelimiter(']'));
			if (token == null) throw new ExpressionCompilerException(originalLine, ts.CharacterPosition + 1);

			AttributeMatcher ret = new AttributeMatcher(null,
				new StringMatcherEquals(attrName),
				stringMatcher);
			ret.EmitID = emittanceID;
			return ret;
		}

		private static string __TryEatEMITTANCE(string originalLine, TokenStream ts)
		{
			Token[] tokens = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('~'),
				TokenPattern.MatchDelimiter('>'),
				TokenPattern.MatchAnyWord()
				);
			if (tokens == null) return null;
			else return tokens[2].Text;
		}

		private static AbstractStringMatcher __TryEatSTRINGCHECKRIGHTPART(string originalLine, TokenStream ts)
		{
			SEQResult r = TryEatAlternatives(ts,
				new SEQ("equals",
					TokenPattern.MatchDelimiter("="),
					TokenPattern.MatchAnyStringSQ(true)
					),
				new SEQ("equals",
					TokenPattern.MatchWord("equals"),
					TokenPattern.MatchAnyStringSQ(true)
					),
				new SEQ("contains",
					TokenPattern.MatchWord("contains"),
					TokenPattern.MatchAnyStringSQ(true)
					),
				new SEQ("startsWith",
					TokenPattern.MatchWord("starts"),
					TokenPattern.MatchWord("with"),
					TokenPattern.MatchAnyStringSQ(true)
					),
				new SEQ("endsWith",
					TokenPattern.MatchWord("ends"),
					TokenPattern.MatchWord("with"),
					TokenPattern.MatchAnyStringSQ(true)
					)
				);

			if (r.IsMatch) {
				switch (r.Name) {
					case "equals":
						return new StringMatcherEquals(r.TokensContent[0].Text);
					case "contains":
						return new StringMatcherContains(r.TokensContent[0].Text);
					case "startsWith":
						return new StringMatcherStartsWith(r.TokensContent[0].Text);
					case "endsWith":
						return new StringMatcherEndsWith(r.TokensContent[0].Text);
					default:
						throw new ImplementationErrorException();
				}
			} else {
				return null;
			}
		}

	}

}
