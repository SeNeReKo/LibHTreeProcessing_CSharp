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

	public class GroupNodes_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string groupNodeName;
		private HashSet<string> recognizeNodeNames;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public GroupNodes_Operation(int lineNo, IEnumerable<string> recognizeNodeNames, string groupNodeName)
			: base(lineNo)
		{
			this.recognizeNodeNames = new HashSet<string>(recognizeNodeNames);
			this.groupNodeName = groupNodeName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			HElement e = (HElement)currentElement;

			// step 1: mark all

			bool[] groupElementMarkers = new bool[e.Children.Count];
			for (int i = 0; i < groupElementMarkers.Length; i++) {
				if (e.Children[i] is HElement) {
					groupElementMarkers[i] = recognizeNodeNames.Contains(((HElement)(e.Children[i])).Name);
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
						ProcessingUtils.Group(e.Children, i + 1, nCount, groupNodeName);
						nCount = 0;
					}
				}
			}
			if (nCount > 0) {
				ProcessingUtils.Group(e.Children, 0, nCount, groupNodeName);
			}
		}
		
	}

}
