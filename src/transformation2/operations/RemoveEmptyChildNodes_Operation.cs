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

	public class RemoveEmptyChildNodes_Operation : AbstractOperation
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
		public RemoveEmptyChildNodes_Operation(int lineNo)
			: base(lineNo)
		{
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

			for (int i = e.Children.Count - 1; i >= 0; i--) {
				if (e.Children[i] is HText) continue;

				HElement he = (HElement)(e.Children[i]);

				if (he.Attributes.Count > 0) continue;

				bool bRemove = true;
				foreach (HAbstractElement nested in he.Children) {
					if (nested is HText) {
						HText t = (HText)(nested);
						if (t.Text.Length != 0) {
							bRemove = false;
							break;
						}
					} else
					if (nested is HElement) {
						bRemove = false;
						break;
					} else
						throw new ImplementationErrorException();
				}

				if (bRemove) {
					e.Children.RemoveAt(i);
				}
			}
		}
		
	}

}
