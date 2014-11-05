using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.util;


namespace LibHTreeProcessing.src.simplexml.impl
{

	public class MyCharacterStream
	{

		private class MyCharStack : AbstractStack<MyChar>
		{
			public override string ToString()
			{
				return "";
			}
		}

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private bool bIsEOS;
		private int lineNo;
		private int charPos;
		private TextReader r;

		private MyCharStack buffer;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MyCharacterStream(TextReader r)
		{
			this.r = r;

			buffer = new MyCharStack();

			lineNo = 1;
			charPos = 1;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public int LineNumber
		{
			get {
				MyChar c = Peek();
				if (c == null) return -1;
				return c.LineNumber;
			}
		}

		public int Position
		{
			get {
				MyChar c = Peek();
				if (c == null) return -1;
				return c.Position;
			}
		}

		public bool IsEOS
		{
			get {
				if (buffer.Count > 0) return false;
				MyChar c = Peek();
				return c == null;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void Unread(MyChar c)
		{
			if (c == null) throw new Exception("Can't push back EOS!");

			buffer.Push(c);
		}

		public void Unread(params MyChar[] chars)
		{
			for (int i = chars.Length - 1; i >= 0; i--) {
				MyChar c = chars[i];
				if (c == null) throw new Exception("Can't push back EOS!");

				buffer.Push(c);
			}
		}

		public MyChar Read()
		{
			MyChar c = buffer.Pop();
			if (c != null) return c;

			if (bIsEOS) return null;

			int n = r.Read();
			if (n < 0) {
				// EOS
				bIsEOS = true;
				return null;
			} else {
				c = new MyChar((char)n, lineNo, charPos);
				if (n == 13) {
					lineNo++;
					charPos = 1;
				}
				return c;
			}
		}

		public MyChar Peek()
		{
			MyChar c = buffer.Peek();
			if (c != null) return c;

			c = Read();
			if (c == null) return null;
			
			buffer.Push(c);
			return c;
		}

		public override string ToString()
		{
			return "MyCharacterStream[@" + lineNo + ":" + charPos + "]";
		}

	}

}
