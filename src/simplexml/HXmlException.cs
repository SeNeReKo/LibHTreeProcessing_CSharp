using System;
using System.Collections.Generic;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public class HXmlException : Exception
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

		private HXmlException(int lineNo, int charPos, string message)
			: base(__BuildMessage(lineNo, charPos, message))
		{
			this.LineNumber = lineNo;
			this.CharacterPosition = charPos;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public int CharacterPosition
		{
			get;
			private set;
		}

		public int LineNumber
		{
			get;
			private set;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private static string __BuildMessage(int lineNo, int charPos, string message)
		{
			if ((message == null) || (message.Length == 0)) {
				return "Syntax error! (" + lineNo + ":" + charPos + ")";
			} else {
				return "Syntax error! " + message + " (" + lineNo + ":" + charPos + ")";
			}
		}

		////////////////////////////////////////////////////////////////

		public static HXmlException CreateError_General(int lineNo, int charPos)
		{
			return new HXmlException(lineNo, charPos, null);
		}

		public static HXmlException CreateError_Unknown(int lineNo, int charPos, string text)
		{
			return new HXmlException(lineNo, charPos, text);
		}

		public static HXmlException CreateError_WhiteSpaceExpected(int lineNo, int charPos)
		{
			return new HXmlException(lineNo, charPos, "White space expected!");
		}

		public static HXmlException CreateError_UnexpectedEOS(int lineNo, int charPos)
		{
			return new HXmlException(lineNo, charPos, "Unexpected EOS!");
		}

		public static HXmlException CreateError_InvalidCharacterDetected(int lineNo, int charPos, char c)
		{
			return new HXmlException(lineNo, charPos, "Invalid character detected: " + c);
		}

	}

}
