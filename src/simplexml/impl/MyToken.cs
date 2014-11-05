using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml.impl
{

	public class MyToken
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public readonly string Text;
		public readonly int LineNumber;
		public readonly int Position;
		public readonly MyChar[] Chars;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MyToken(params MyChar[] chars)
		{
			this.Chars = chars;
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(IList<MyChar> chars)
		{
			this.Chars = chars.ToArray();
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(string text, params MyChar[] chars)
		{
			this.Text = text;
			this.Chars = chars;
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(string text, IList<MyChar> chars)
		{
			this.Text = text;
			this.Chars = chars.ToArray();
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(StringBuilder text, params MyChar[] chars)
		{
			this.Text = text.ToString();
			this.Chars = chars;
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(StringBuilder text, IList<MyChar> chars)
		{
			this.Text = text.ToString();
			this.Chars = chars.ToArray();
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(char[] text, params MyChar[] chars)
		{
			this.Text = new string(text);
			this.Chars = chars;
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		public MyToken(char[] text, IList<MyChar> chars)
		{
			this.Text = new string(text);
			this.Chars = chars.ToArray();
			this.LineNumber = this.Chars[0].LineNumber;
			this.Position = this.Chars[0].Position;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

	}

}
