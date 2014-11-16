using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public class BuildPathFromAttributes_Operation : AbstractFilter
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string attributeName;
		private string delimiter;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public BuildPathFromAttributes_Operation(int lineNo, string attributeName, string delimiter)
			: base(lineNo)
		{
			this.attributeName = attributeName;
			this.delimiter = delimiter;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HElement selected;

			if (currentElement is HAttribute) {
				selected = (HElement)(currentPath.ParentOf(currentElement).Element);
			} else
			if (currentElement is HElement) {
				selected = (HElement)currentElement;
			} else {
				throw ScriptException.CreateError_Unknown(LineNumber, "Data is not a regular node or attribute!");
			}

			string path = __BuildPath(currentPath, selected);

			if (path == null) return null;
			else return new HText(path);
		}

		private string __BuildPath(HPathWithIndices path, HElement finalElement)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < path.Count; i++) {
				string attrValue = ((HElement)(path[i].Element)).GetAttributeValue(attributeName);
				if (attrValue != null) {
					if (sb.Length > 0) sb.Append(delimiter);
					sb.Append(attrValue);
				}
				if (path[i].Element == finalElement) break;
			}
			if (sb.Length == 0) return null;
			return sb.ToString();
		}
		
	}

}
