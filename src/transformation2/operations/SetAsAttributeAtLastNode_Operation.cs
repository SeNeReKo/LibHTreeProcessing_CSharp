﻿using System;
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

	public class SetAsAttributeAtLastNode_Operation : AbstractOperation
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string nodeName;
		private string attrName;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public SetAsAttributeAtLastNode_Operation(int lineNo, string nodeName, string attrName)
			: base(lineNo)
		{
			this.nodeName = nodeName;
			this.attrName = attrName;
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

					he.SetAttribute(attrName, text);
					return;
				}
			}

			ScriptException.CreateError_Unknown(LineNumber, "No node found in path!");
		}

	}

}
