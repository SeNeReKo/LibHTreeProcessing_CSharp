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

	public class MoveAsTextToLastNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string nodeName;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public MoveAsTextToLastNode_Operation(int lineNo, string nodeName)
			: base(lineNo)
		{
			this.nodeName = nodeName;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			for (int i = currentPath.Count - 1; i >= 0; i--) {
				if (currentPath[i].Element is HElement) {
					HElement he = (HElement)(currentPath[i].Element);
					if (nodeName != null) {
						if (!he.Name.Equals(nodeName)) continue;
					}

					string text;
					if (currentElement is HText) {
						text = ((HText)currentElement).Text;
					} else
					if (currentElement is HAttribute) {
						text = ((HAttribute)currentElement).Value;
					} else {
						throw new ImplementationErrorException();
					}

					// remove all text elements

					for (int j = he.Children.Count - 1; j >= 0; j--) {
						if (he.Children[j] is HText) he.Children.RemoveAt(j);
					}

					// add text element

					he.Children.Add(new HText(text));

					// remove element

					PathStruct ps = currentPath.ParentOf(currentElement);		// TODO: check "currentPath" in subtree: is it as short as expected?
					if (ps.Element == null) {
						// can't process this: path is too short
					} else {
						HElement heParent = (HElement)(ps.Element);

						if (currentElement is HText) {
							heParent.Children.Remove(currentElement);
						} else
						if (currentElement is HAttribute) {
							heParent.Attributes.RemoveAll(((HAttribute)currentElement).Name);
						} else {
							throw new ImplementationErrorException();
						}

					}

					return;
				}
			}

			ScriptException.CreateError_Unknown(LineNumber, "No node found in path!");
		}

	}

}
