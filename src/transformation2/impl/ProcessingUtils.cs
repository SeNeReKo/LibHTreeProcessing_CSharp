using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public static class ProcessingUtils
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

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Collects the specified elements and replaces them with a new node named "newGroupNodeName".
		/// </summary>
		/// <param name="list"></param>
		/// <param name="ofs"></param>
		/// <param name="len"></param>
		/// <param name="newGroupNodeName"></param>
		public static void Group(HAbstractElementList list, int ofs, int len, string newGroupNodeName)
		{
			HElement e = new HElement(newGroupNodeName);
			for (int i = 0; i < len; i++) {
				e.Children.Add(list[i + ofs]);
			}
			list.RemoveRange(ofs, len);
			list.Insert(ofs, e);
		}

		/// <summary>
		/// Collects the specified elements and replaces them with a new node named "newGroupNodeName".
		/// </summary>
		/// <param name="list"></param>
		/// <param name="ofs"></param>
		/// <param name="len"></param>
		/// <param name="newGroupNodeName"></param>
		public static void MergeTexts(HAbstractElementList list, int ofs, int len)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < len; i++) {
				HText t = (HText)(list[i + ofs]);
				sb.Append(t.Text);
			}
			list.RemoveRange(ofs, len);
			list.Insert(ofs, new HText(sb.ToString()));
		}

		public static HElement GetCreateByPath(HElement root, string[] pathElements)
		{
			HElement e = root;
			for (int i = 0; i < pathElements.Length; i++) {
				e = e.GetCreateChildElement(pathElements[i]);
			}
			return e;
		}

		/// <summary>
		/// Either returns a <code>string</code> or a <code>HAbstractElementList</code>. <code>null</code> is returned if no replacement was found.
		/// </summary>
		/// <param name="dataCtx"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static object ReplaceDataByClipboardMap(ScriptRuntimeContext dataCtx, HExpression clipboardExpression, string keyAttrName, string valueAttrName,
			string data, bool bThrowExceptionIfKeyNotFound, int lineNo)
		{
			MatchResultGroup mrg = clipboardExpression.MatchAll(dataCtx.Clipboard);

			Dictionary<string, string> __temp = new Dictionary<string, string>();

			foreach (MatchResult mr in mrg) {
				HElement mapElement = (HElement)(mr.Path.Top.Element);
				HAttribute mapK = mapElement.Attributes[keyAttrName];
				if (mapK == null)
					throw ScriptException.CreateError_Unknown(lineNo, "Map does not contain attribute \"" + keyAttrName + "\"!");

				__temp.Add(mapK.Value, mapK.Value);

				if (mapK.Value.Equals(data)) {
					// found!

					// 1st chance: attribute

					HAttribute mapV = mapElement.Attributes[valueAttrName];
					if (mapV != null) {
						// return attribute value
						return mapV.Value;
					}

					// 2st chance: text content

					if (mapElement.Children.Count == 1) {
						if (mapElement.Children[0] is HText) {
							// convert to string
							return ((HText)(mapElement.Children[0])).Text;
						}
					}

					// 3st chance: arbitrary content

					return mapElement.Children;
				}
			}

			if (bThrowExceptionIfKeyNotFound) {
				MatchResultGroup mrg2 = clipboardExpression.MatchAll(dataCtx.Clipboard);
				throw ScriptException.CreateError_Unknown(lineNo, "Map does not contain key \"" + data + "\"!");
			} else {
				return null;
			}
		}

	}

}
