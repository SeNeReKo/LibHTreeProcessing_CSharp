using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


using LibNLPCSharp.util;
using LibHTreeProcessing.src.simplexml;
using LibNLPCSharp.gui;

using LibHTreeProcessing.src;


namespace LibHTreeProcessing.src.gui
{

	public partial class MainForm
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

		private TagObject __FindTagObject(TreeNode node)
		{
			TreeNode n = node;

			while ((n.Tag == null) || !(n.Tag is TagObject)) {
				n = n.Parent;
				if (n == null) {
					return null;
				}
			}

			TagObject to = (TagObject)(n.Tag);
			return to;
		}

		private static bool __IsOrdinaryTextElement(HElement he)
		{
			if (he.Name.Equals("bodytext") || he.Name.StartsWith("gatha")) {
				return true;
			} else {
				return false;
			}
		}

		private void __StyleNode(TreeNode node, HAbstractElement element)
		{
			node.NodeFont = regularFont;
			if (element is HText) {
				node.ForeColor = Color.DarkGreen;
			}
		}

	}

}
