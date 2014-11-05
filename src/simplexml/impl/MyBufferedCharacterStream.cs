using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.util;


namespace LibHTreeProcessing.src.simplexml.impl
{

	public class MyBufferedCharacterStream
	{

		private static int SAVE_BUFFER_DATA_LENGTH = 4096;
		private static int MAX_BUFFER_DATA_LENGTH = 65536;

		public interface IMark
		{
			void ResetToMark();
			void DiscardMark();
		}

		private class MyMark : IMark, IDisposable
		{
			private bool bDiscarded;
			private MyBufferedCharacterStream stream;
			internal int bufferCursor;
			private int lineNo;
			private int charPos;

			public MyMark(MyBufferedCharacterStream stream, int bufferCursor, int lineNo, int charPos)
			{
				this.stream = stream;
				this.bufferCursor = bufferCursor;
				this.lineNo = lineNo;
				this.charPos = charPos;
			}

			public void Dispose()
			{
				if (!bDiscarded) throw new Exception("Mark detected that has not been explicitely discarded!");
			}

			public void ResetToMark()
			{
				if (bDiscarded) throw new Exception("Mark is already discarded!");
				stream.bufferCursor = bufferCursor;
			}

			public void DiscardMark()
			{
				bDiscarded = true;
				stream.marks.Remove(this);
			}
		}

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private bool bIsStreamEOS;
		private int bufferCursor;
		private int bufferPrepared;
		private int bufferFilled;
		private TextReader r;
		private char[] buffer;
		private MyChar[] buffer2;

		private int lineNo;
		private int charPos;

		List<MyMark> marks;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MyBufferedCharacterStream(TextReader r)
		{
			this.r = r;

			buffer = new char[MAX_BUFFER_DATA_LENGTH];
			buffer2 = new MyChar[MAX_BUFFER_DATA_LENGTH];
			bufferFilled = 0;

			lineNo = 1;
			charPos = 1;

			marks = new List<MyMark>();
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
				if (bufferCursor < bufferFilled) return false;
				return bIsStreamEOS;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private void __DiscardOldDataFromBuffer()
		{
			int minPos = __GetMinimumBufferPositionRequired();
			if (minPos > SAVE_BUFFER_DATA_LENGTH) {
				// calculate number of chars to copy
				int len = bufferFilled - minPos;

				// shift data in buffer
				Array.Copy(buffer, minPos, buffer, 0, len);
				Array.Copy(buffer2, minPos, buffer2, 0, len);

				// correct pointers into the buffer
				bufferFilled -= minPos;
				bufferCursor -= minPos;
				bufferPrepared -= minPos;
				foreach (MyMark m in marks) {
					m.bufferCursor -= minPos;
				}
			}
		}

		/// <summary>
		/// Obtain the minimum position we must have in the buffer. This takes
		/// all marks into consideration as well as a save are for unwinds of
		/// length SAVE_BUFFER_DATA_LENGTH.
		/// </summary>
		/// <returns>The minimum position we must keep in the buffer. We may
		/// discard all data before this position.</returns>
		private int __GetMinimumBufferPositionRequired()
		{
			int n = bufferCursor - SAVE_BUFFER_DATA_LENGTH;
			if (n < 0) return 0;

			foreach (MyMark m in marks) {
				if (m.bufferCursor < n) n = m.bufferCursor;
			}
			return n;
		}

		/// <summary>
		/// Try to fill the buffer with more data
		/// </summary>
		private void __FillBufferWithDataFromStream(int countCharsRequired)
		{
			if (bIsStreamEOS) return;

			int charsStillAvailableInBuffer = bufferFilled - bufferCursor;
			if (countCharsRequired <= charsStillAvailableInBuffer) return;

			int charsExpectedByNextRead = buffer.Length - bufferFilled;
			if (countCharsRequired > charsExpectedByNextRead) {
				throw new Exception("Internal data buffers too small!");
			}

			while (charsExpectedByNextRead > 0) {
				int charsRead = r.Read(buffer, bufferFilled, charsExpectedByNextRead);
				if (charsRead <= 0) {
					bIsStreamEOS = true;
					return;
				}
				bufferFilled += charsRead;
				charsExpectedByNextRead -= charsRead;
			}
		}

		////////////////////////////////////////////////////////////////

		public void Unread(MyChar c)
		{
			if (c == null) throw new Exception("Can't push back EOS!");

			if (bufferCursor == 0)
				throw new Exception("Can't unwind any further!");

			if (buffer2[bufferCursor - 1] != c)
				throw new Exception("Invalid character presented for pushback!");

			bufferCursor--;
		}

		public void Unread(params MyChar[] chars)
		{
			if (bufferCursor - chars.Length < 0)
				throw new Exception("Can't unwind that much data!");

			for (int i = chars.Length - 1; i >= 0; i--) {
				MyChar c = chars[i];

				if (c == null) throw new Exception("Can't push back EOS!");

				if (buffer2[bufferCursor - chars.Length + i] != c)
					throw new Exception("Invalid character presented for pushback!");
			}

			bufferCursor -= chars.Length;
		}

		public MyChar Read()
		{
			if (bufferCursor == bufferFilled) {
				if (bIsStreamEOS) return null;

				__DiscardOldDataFromBuffer();
				__FillBufferWithDataFromStream(1);
			}

			if (bufferCursor == bufferFilled) {
				// EOS: last read did not return any more data
				return null;
			}

			MyChar c;
			if (bufferCursor == bufferPrepared) {
				char cc = buffer[bufferCursor];
				if (cc == 13) {
					lineNo++;
					charPos = 1;
				}
				c = new MyChar(cc, lineNo, charPos);
				buffer2[bufferCursor] = c;
				bufferPrepared++;
			} else {
				c = buffer2[bufferCursor];
			}
			bufferCursor++;

			return c;
		}

		public MyChar Peek()
		{
			if (bufferCursor == bufferFilled) {
				if (bIsStreamEOS) return null;

				__DiscardOldDataFromBuffer();
				__FillBufferWithDataFromStream(1);
			}

			if (bufferCursor == bufferFilled) {
				// EOS: last read did not return any more data
				return null;
			}

			MyChar c;
			if (bufferCursor == bufferPrepared) {
				char cc = buffer[bufferCursor];
				if (cc == 13) {
					lineNo++;
					charPos = 1;
				}
				c = new MyChar(cc, lineNo, charPos);
				buffer2[bufferCursor] = c;
				bufferPrepared++;
			} else {
				c = buffer2[bufferCursor];
			}
			
			return c;
		}

		/// <summary>
		/// Create a mark
		/// </summary>
		/// <returns></returns>
		public IMark Mark()
		{
			MyChar c = Peek();
			MyMark m;
			if (c == null) {
				m = new MyMark(this, bufferCursor, lineNo, charPos);
			} else {
				m = new MyMark(this, bufferCursor, c.LineNumber, c.Position);
			}
			marks.Add(m);
			return m;
		}

		public override string ToString()
		{
			return "MyBufferedCharacterStream[@" + lineNo + ":" + charPos + "]";
		}

	}

}
