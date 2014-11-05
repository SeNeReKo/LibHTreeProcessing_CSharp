using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml.impl
{

	public class MyTokenizer
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		MyCharacterStream cs;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MyTokenizer(MyCharacterStream cs)
		{
			this.cs = cs;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool IsEOS
		{
			get {
				return cs.IsEOS;
			}
		}

		public int LineNumber
		{
			get {
				return cs.LineNumber;
			}
		}

		public int Position
		{
			get {
				return cs.Position;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void Unread(MyChar c)
		{
			cs.Unread(c);
		}

		public void Unread(MyToken t)
		{
			cs.Unread(t.Chars);
		}

		public MyChar Read()
		{
			return cs.Read();
		}

		public MyChar TryEatCharacter(char delim)
		{
			MyChar c = cs.Read();
			if (c == null) return null;

			if (c.C == delim) {
				return c;
			} else {
				cs.Unread(c);
				return null;
			}
		}

		public MyToken TryEatXmlString()
		{
			MyChar c = cs.Read();
			if (c == null) return null;

			if (c.C != '"') {
				cs.Unread(c);
				return null;
			}

			List<MyChar> chars = new List<MyChar>();
			chars.Add(c);

			MyChar c2;
			StringBuilder sb = new StringBuilder();
			while (true) {
				c2 = cs.Read();
				if (c2 == null) throw new Exception("Unclosed string literal!");	// EOS

				chars.Add(c2);
				if (c2.C == '"') {
					// end of string reached!
					return new MyToken(sb.ToString(), chars);
				} else {
					sb.Append(c2.C);
				}
			}
		}

		public MyToken TryEatWhiteSpaces()
		{
			MyChar c = cs.Read();
			if (c == null) return null;	// EOS

			if (!(char.IsWhiteSpace(c.C) || (c.C == 13) || (c.C == 10))) {
				cs.Unread(c);
				return null;
			}

			// whitespace encountered

			List<MyChar> chars = new List<MyChar>();
			chars.Add(c);

			int n = 1;
			while (true) {
				c = cs.Read();
				if (c == null) return new MyToken(chars);		// EOS

				if (char.IsWhiteSpace(c.C) || (c.C == 13) || (c.C == 10)) {
					// whitespace encountered
					n++;
					chars.Add(c);
				} else {
					// other character encountered
					cs.Unread(c);
					return  new MyToken(chars);
				}
			}
		}

		public MyToken TryEatXmlWord()
		{
			MyChar c = cs.Read();
			if (c == null) return null;

			if (!char.IsLetter(c.C)) {
				cs.Unread(c);
				return null;
			}

			List<MyChar> chars = new List<MyChar>();
			chars.Add(c);

			StringBuilder sb = new StringBuilder();
			sb.Append(c.C);

			while (true) {
				MyChar c2 = cs.Read();
				if (c2 == null) return new MyToken(sb, chars);		// EOS

				if (char.IsLetterOrDigit(c.C)) {
					sb.Append(c.C);
					chars.Add(c);
				} else {
					// no more tokens
					cs.Unread(c2);
					return new MyToken(sb, chars);
				}
			}
		}

		public MyToken TryEatSequence(params char[] sequence)
		{
			MyChar[] chars = new MyChar[sequence.Length];
			chars[0] = cs.Read();
			if (chars[0] == null) return null;	// EOS

			for (int i = 1; i < chars.Length; i++) {
				MyChar c = cs.Read();
				if ((c != null) && (c.C == sequence[i])) {
					// good -> continue with sequence
					chars[i] = c;
				} else {
					// bad -> unwind stored data
					if (c != null) cs.Unread(c);
					for (int j = i - 1; j >= 0; j--) {
						cs.Unread(chars[j]);
					}
					return null;
				}
			}
			
			return new MyToken(sequence, chars);
		}

		public MyToken TryEatSequence(string sequence)
		{
			MyChar[] chars = new MyChar[sequence.Length];
			chars[0] = cs.Read();
			if (chars[0] == null) return null;	// EOS

			for (int i = 1; i < chars.Length; i++) {
				MyChar c = cs.Read();
				if ((c != null) && (c.C == sequence[i])) {
					// good -> continue with sequence
					chars[i] = c;
				} else {
					// bad -> unwind stored data
					if (c != null) cs.Unread(c);
					for (int j = i - 1; j >= 0; j--) {
						cs.Unread(chars[j]);
					}
					return null;
				}
			}

			return new MyToken(sequence, chars);
		}

		public MyToken TryEatUntil(params char[] sequence)
		{
			MyChar c = cs.Peek();
			if (c == null) return null;

			List<MyChar> chars = new List<MyChar>();
			chars.Add(c);

			StringBuilder sb = new StringBuilder();

			while (true) {
				MyToken t = TryEatSequence(sequence);
				if (t != null) {
					// sequence found
					chars.AddRange(t.Chars);
					return new MyToken(sb, chars);
				} else {
					// next token is not the beginning of the sequence
					c = cs.Read();
					if (c == null) throw HXmlException.CreateError_UnexpectedEOS(chars[0].LineNumber, chars[0].Position);

					sb.Append(c.C);
					chars.Add(c);
				}
			}
		}

		public MyToken TryEatUntil(string sequence)
		{
			MyChar c = cs.Peek();
			if (c == null) return null;

			List<MyChar> chars = new List<MyChar>();
			chars.Add(c);

			StringBuilder sb = new StringBuilder();

			while (true) {
				MyToken t = TryEatSequence(sequence);
				if (t != null) {
					// sequence found
					chars.AddRange(t.Chars);
					return new MyToken(sb, chars);
				} else {
					// next token is not the beginning of the sequence
					c = cs.Read();
					if (c == null) throw HXmlException.CreateError_UnexpectedEOS(chars[0].LineNumber, chars[0].Position);

					sb.Append(c.C);
					chars.Add(c);
				}
			}
		}

	}

}
