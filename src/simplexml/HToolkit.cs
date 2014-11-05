using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;


using LibNLPCSharp.util;


namespace LibHTreeProcessing.src.simplexml
{

	public partial class HToolkit
	{

		public delegate bool CheckOutputTextAsInlineDelegate(HElement element);

		public enum EnumFillTagMode
		{
			None,
			IfNull,
			Always,
		}

		public delegate void StyleNodeDelegate(TreeNode node, HAbstractElement element);

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		private HToolkit()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public static HElement Parse(string text, bool bResolveEntities)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(text);
			return Convert(doc, bResolveEntities);
		}

		public static void CountNodes(HAbstractElement root,
			out int countElements, out int countTexts, out int countAttributes)
		{
			countElements = 0;
			countTexts = 0;
			countAttributes = 0;

			__CountNodes(root, ref countElements, ref countTexts, ref countAttributes);
		}

		private static void __CountNodes(HAbstractElement node,
			ref int countElements, ref int countTexts, ref int countAttributes)
		{
			if (node is HElement) {
				countElements++;
				HElement e = (HElement)node;
				countAttributes += e.Attributes.Count;

				foreach (HAbstractElement eChild in e.Children) {
					__CountNodes(eChild, ref countElements, ref countTexts, ref countAttributes);
				}
			} else {
				countTexts++;
			}
		}

		public static HElement Convert(XmlDocument doc, bool bResolveEntities,
			out int countElements, out int countTexts, out int countAttributes)
		{
			countElements = 1;
			countTexts = 0;
			countAttributes = 0;

			HElement rootNode = new HElement(doc.DocumentElement.Name);
			__CopyAttributes(doc.DocumentElement, rootNode, ref countAttributes);

			__CopyNodes(doc.DocumentElement.ChildNodes, rootNode.Children, bResolveEntities,
				ref countElements, ref countTexts, ref countAttributes);

			return rootNode;
		}

		public static HElement Convert(XmlElement doc, bool bResolveEntities)
		{
			int countElements = 1;
			int countTexts = 0;
			int countAttributes = 0;

			HElement rootNode = new HElement(doc.Name);
			__CopyAttributes(doc, rootNode, ref countAttributes);

			__CopyNodes(doc.ChildNodes, rootNode.Children, bResolveEntities, ref countElements, ref countTexts, ref countAttributes);

			return rootNode;
		}

		public static HElement Convert(XmlElement doc, bool bResolveEntities,
			out int countElements, out int countTexts, out int countAttributes)
		{
			countElements = 1;
			countTexts = 0;
			countAttributes = 0;

			HElement rootNode = new HElement(doc.Name);
			__CopyAttributes(doc, rootNode, ref countAttributes);

			__CopyNodes(doc.ChildNodes, rootNode.Children, bResolveEntities,
				ref countElements, ref countTexts, ref countAttributes);

			return rootNode;
		}

		public static void CopyAttributesWithExceptions(HElement from, HElement to, params string[] exceptions)
		{
			foreach (HAttribute a in from.Attributes) {
				bool bFound = false;
				foreach (string s in exceptions) {
					if (a.Name.Equals(s)) {
						bFound = true;
						break;
					}
				}
				if (bFound) continue;
				to.Attributes.Add(new HAttribute(a.Name, a.Value));
			}
		}

		public static void FillTree(HAbstractElement root, TreeView treeView,
			StyleNodeDelegate styleNodeDelegate, EnumFillTagMode fillNodeTagsWithElements)
		{
			treeView.BeginUpdate();
			treeView.Nodes.Clear();
			__DocumentToTree(root, treeView.Nodes, styleNodeDelegate, fillNodeTagsWithElements);
			treeView.ExpandAll();
			treeView.EndUpdate();
		}

		public static void FillTree(HAbstractElement root, TreeNode node,
			StyleNodeDelegate styleNodeDelegate, EnumFillTagMode fillNodeTagsWithElements)
		{
			TreeView treeView = node.TreeView;

			treeView.BeginUpdate();
			node.Nodes.Clear();
			__DocumentToTree(root, node.Nodes, styleNodeDelegate, fillNodeTagsWithElements);
			node.ExpandAll();
			treeView.EndUpdate();
		}

		private static void __DocumentToTree(HAbstractElement root, TreeNodeCollection nodes,
			StyleNodeDelegate styleNodeDelegate, EnumFillTagMode fillNodeTagsWithElements)
		{
			TreeNode newNode;

			if (root is HText) {
				HText t = (HText)root;
				newNode = nodes.Add(t.Text);
				if (styleNodeDelegate != null) {
					styleNodeDelegate(newNode, root);
				}
				switch (fillNodeTagsWithElements) {
					case EnumFillTagMode.Always:
						newNode.Tag = root;
						break;
					case EnumFillTagMode.IfNull:
						newNode.Tag = root;
						break;
				}
			} else
			if (root is HElement) {
				HElement e = (HElement)root;
				string s = e.Name;
				foreach (HAttribute a in e.Attributes) {
					s += "  " + a.Name + "=\"" + a.Value + "\"";
				}
				newNode = nodes.Add(s);
				if (styleNodeDelegate != null) {
					styleNodeDelegate(newNode, root);
				}
				switch (fillNodeTagsWithElements) {
					case EnumFillTagMode.Always:
						newNode.Tag = root;
						break;
					case EnumFillTagMode.IfNull:
						if (newNode.Tag == null) newNode.Tag = root;
						break;
				}

				foreach (HAbstractElement n in e.Children) {
					__DocumentToTree(n, newNode.Nodes, styleNodeDelegate, fillNodeTagsWithElements);
				}
			}
		}

		public static List<string> ToText(List<HAbstractElement> someElements)
		{
			List<string> output = new List<string>();
			__ToText(someElements, output);
			return output;
		}

		private static void __ToText(List<HAbstractElement> someElements, List<string> output)
		{
			foreach (HAbstractElement hae in someElements) {
				if (hae is HText) {
					HText t = (HText)hae;
					output.Add(t.Text);
				} else
				if (hae is HElement) {
					HElement e = (HElement)hae;
					__ToText(e.Children, output);
				} else
					throw new ImplementationErrorException();
			}
		}

	}

}
