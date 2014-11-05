using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public class RemoveTextSpacesBeforeDelimiters_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string delimiters;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public RemoveTextSpacesBeforeDelimiters_Operation(int lineNo, string delimiters)
			: base(lineNo)
		{
			this.delimiters = delimiters;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HText t = (HText)currentElement;

			List<char> allChars = new List<char>(t.Text);

			int n = 0;
			while (n < allChars.Count) {
				char c = allChars[n];
				if (delimiters.IndexOf(c) >= 0) {

					while (true) {
						int lastN = n - 1;
						if (lastN < 0)
							break;
						if (char.IsWhiteSpace(allChars[lastN])) {
							allChars.RemoveAt(lastN);
							n--;
						} else {
							break;
						}
					}

				}
				n++;
			}

			StringBuilder sb = new StringBuilder();
			foreach (char c in allChars) sb.Append(c);

			t.Text = sb.ToString();
		}
		
	}

}
