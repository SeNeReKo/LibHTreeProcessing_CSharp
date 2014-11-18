using System;
using System.Collections.Generic;
using System.Text;


using LibSimpleScriptEditor.src;


namespace LibHTreeProcessing.src.transformation2
{

	public class ScriptException : Exception, ICompilationError
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

		private ScriptException(int lineNo, string message)
			: base(__BuildMessage(lineNo, message))
		{
			this.LineNumber = lineNo;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public int LineNumber
		{
			get;
			private set;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private static string __BuildMessage(int lineNo, string message)
		{
			if ((message == null) || (message.Length == 0)) {
				return "Syntax error! (Line: " + lineNo + ")";
			} else {
				return "Syntax error! " + message + " (Line: " + lineNo + ")";
			}
		}

		////////////////////////////////////////////////////////////////

		public static ScriptException CreateError_General(int lineNo)
		{
			return new ScriptException(lineNo, null);
		}

		public static ScriptException CreateError_FilterDidNotReturnAnyData(int lineNo)
		{
			return new ScriptException(lineNo, "Filter did not return any data!");
		}

		public static ScriptException CreateError_SelectorDidNotReturnAnyData(int lineNo)
		{
			return new ScriptException(lineNo, "Selector did not return any data!");
		}

		public static ScriptException CreateError_PatternExpressionExpected(int lineNo)
		{
			return new ScriptException(lineNo, "Pattern expression expected!");
		}

		public static ScriptException CreateError_AttributeDefinitionGroupExpected(int lineNo)
		{
			return new ScriptException(lineNo, "Attribute definition group expected!");
		}

		public static ScriptException CreateError_UnexpectedEOS(int lineNo)
		{
			return new ScriptException(lineNo, "Unexpected end of stream!");
		}

		public static ScriptException CreateError_CommaExpected(int lineNo)
		{
			return new ScriptException(lineNo, "Comma expected!");
		}

		public static ScriptException CreateError_OneOrMoreCommandsExpected(int lineNo)
		{
			return new ScriptException(lineNo, "One or more commands expected!");
		}

		public static ScriptException CreateError_ClosingCurleyBracesExpected(int lineNo)
		{
			return new ScriptException(lineNo, "\"}\" expected!");
		}

		public static ScriptException CreateError_CommandBlockExpected(int lineNo)
		{
			return new ScriptException(lineNo, "Command block expected!");
		}

		public static ScriptException CreateError_PipeExpected(int lineNo)
		{
			return new ScriptException(lineNo, "\">>\" or \">>>>\" expected!");
		}

		public static ScriptException CreateError_ValidFilterOrOperationExpected(int lineNo)
		{
			return new ScriptException(lineNo, "Valid filter or operation expected!");
		}

		public static ScriptException CreateError_ExcessiveTextEncountered(int lineNo)
		{
			return new ScriptException(lineNo, "Misspelled command or other unexpected text encountered!");
		}

		public static ScriptException CreateError_EOLExpected(int lineNo)
		{
			return new ScriptException(lineNo, "End of line expected!");
		}

		public static ScriptException CreateError_AttributeDefinitionExpected(int lineNo)
		{
			return new ScriptException(lineNo, "Attribute definition expected!");
		}

		public static ScriptException CreateError_StringGroupExpected(int lineNo)
		{
			return new ScriptException(lineNo, "String group expected!");
		}

		public static ScriptException CreateError_StringExpected(int lineNo)
		{
			return new ScriptException(lineNo, "String expected!");
		}

		public static ScriptException CreateError_InvalidPatternExpressionSpecified(int lineNo, string data)
		{
			return new ScriptException(lineNo, "Invalid pattern expression specified: \"" + data + "\"");
		}

		public static ScriptException CreateError_NotAValidRegExSpecified(int lineNo, string data)
		{
			return new ScriptException(lineNo, "Not a valid regular expression specified: \"" + data + "\"");
		}

		public static ScriptException CreateError_NotAValidExpressionSpecified(int lineNo, string data)
		{
			return new ScriptException(lineNo, "Not a valid path expression specified: \"" + data + "\"");
		}

		public static ScriptException CreateError_InvalidStringSpecified(int lineNo, string data)
		{
			return new ScriptException(lineNo, "Not a valid string specified: \"" + data + "\"");
		}

		public static ScriptException CreateError_NodeExpressionRequired(int lineNo, string data)
		{
			return new ScriptException(lineNo, "Node expression required: \"" + data + "\"");
		}

		public static ScriptException CreateError_NoParent(int lineNo)
		{
			return new ScriptException(lineNo, "No parent!");
		}

		public static ScriptException CreateError_Unknown(int lineNo, string text)
		{
			return new ScriptException(lineNo, text);
		}

		public static ScriptException CreateError_ProcedureNotFound(int lineNo, string procName)
		{
			return new ScriptException(lineNo, "Procedure not found: " + procName);
		}

		public static ScriptException CreateError_ProcedureAlreadyDefined(int lineNo, string procName, int defLineNo)
		{
			return new ScriptException(lineNo, "At line " + defLineNo + " a procedure named \"" + procName + "\" is already defined!");
		}

		public static ScriptException CreateError_MoreThanOneEmitIDsSpecified(int lineNo)
		{
			return new ScriptException(lineNo, "More than one EmitIDs specified!");
		}

		public static ScriptException CreateError_InvalidNodePathSpecified(int lineNo, string data)
		{
			return new ScriptException(lineNo, "Invalid node path specified: \"" + data + "\"");
		}

		public static ScriptException CreateError_FileNotFound(int lineNo, string filePath)
		{
			return new ScriptException(lineNo, "File not found: \"" + filePath + "\"");
		}

		public static ScriptException CreateError_FailedToLoadFile(int lineNo, string filePath, string errorMessage)
		{
			return new ScriptException(lineNo, "Failed to load file \"" + filePath + "\": " + errorMessage);
		}

	}

}
