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

	public class GroupNodeSequences_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string groupNodeName;
		private string[] recognizeNodeNames;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public GroupNodeSequences_Operation(int lineNo, List<string> recognizeNodeNames, string groupNodeName)
			: base(lineNo)
		{
			this.recognizeNodeNames = recognizeNodeNames.ToArray();
			this.groupNodeName = groupNodeName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private bool __HasSequence(HAbstractElementList list, int index, string[] recognizeNodeNames)
		{
			if (index + recognizeNodeNames.Length > list.Count) return false;
			for (int i = 0; i < recognizeNodeNames.Length; i++) {
				if (!(list[i] is HElement)) return false;
				HElement e = (HElement)(list[index + i]);
				if (!(e.Name.Equals(recognizeNodeNames[i]))) return false;
			}
			return true;
		}

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HElement e = (HElement)currentElement;

			int i = 0;
			while (i < e.Children.Count) {
				if (__HasSequence(e.Children, i, recognizeNodeNames)) {
					ProcessingUtils.Group(e.Children, i, recognizeNodeNames.Length, groupNodeName);
				}
				i++;
			}
		}
		
	}

}
