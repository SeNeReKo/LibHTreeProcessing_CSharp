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

	public class MergeTextChunks_Operation : AbstractOperation
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

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public MergeTextChunks_Operation(int lineNo)
			: base(lineNo)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private static void __Merge(HAbstractElementList list)
		{
			// step 1: mark all

			bool[] groupElementMarkers = new bool[list.Count];
			for (int i = 0; i < groupElementMarkers.Length; i++) {
				if (list[i] is HText) {
					groupElementMarkers[i] = true;
				}
			}

			// step 2: find consecuting elements and group them

			int nCount = 0;
			for (int i = groupElementMarkers.Length - 1; i >= 0; i--) {
				if (groupElementMarkers[i]) {
					nCount++;
				} else {
					if (nCount > 0) {
						// group detected
						ProcessingUtils.MergeTexts(list, i + 1, nCount);
						nCount = 0;
					}
				}
			}
			if (nCount > 0) {
				ProcessingUtils.MergeTexts(list, 0, nCount);
			}
		}

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HElement e = (HElement)currentElement;
			__Merge(e.Children);
		}
		
	}

}
