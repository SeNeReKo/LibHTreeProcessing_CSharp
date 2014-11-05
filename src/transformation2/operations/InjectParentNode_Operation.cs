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

	public class InjectParentNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string newNodeName;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public InjectParentNode_Operation(int lineNo, string newNodeName)
			: base(lineNo)
		{
			this.newNodeName = newNodeName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			if (currentElement is HElement) {

				HElement hce = (HElement)currentElement;

				HElement replacement = new HElement(hce.Name);

				replacement.Attributes.AddRange(hce.Attributes);
				hce.Attributes.Clear();

				replacement.Children.AddRange(hce.Children);
				hce.Children.Clear();

				hce.Name = newNodeName;
				hce.Children.Add(replacement);

			} else {

				PathStruct ps = currentPath.ParentOf(currentElement);
				if (ps.Element == null) {
					throw ScriptException.CreateError_NoParent(LineNumber);
				}

				HElement parent = (HElement)(ps.Element);

				int n = parent.Children.IndexOf(currentElement);
				if (n < 0) ScriptException.CreateError_Unknown(LineNumber, "Element not found in child list of parent!");

				// create new node

				HElement e = new HElement(newNodeName);
				e.Children.Add(currentElement);

				// replace with new node

				parent.Children[n] = e;

			}
		}
		
	}

}
