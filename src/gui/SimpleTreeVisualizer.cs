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

using LibHTreeProcessing.src.transformation2;


namespace LibHTreeProcessing.src.gui
{

	public partial class SimpleTreeVisualizer : UserControl
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		private static readonly Font FONT = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Regular);

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		HElement rootNode;
		int rightMargin;
		List<Button> buttons;
		SearchFormController searchFormController;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public SimpleTreeVisualizer()
		{
			buttons = new List<Button>();

			InitializeComponent();

			searchFormController = new SearchFormController(treeView1, __StyleNodeCallback);

			rightMargin = 5 + btnSearch.Width + 5;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public SearchFormController SearchFormController
		{
			get {
				return searchFormController;
			}
		}

		public HElement RootNode
		{
			get {
				return rootNode;
			}
			set {
				if (this.rootNode != value) {
					this.rootNode = value;
					if (value == null) {
						treeView1.Nodes.Clear();
						textBox1.Text = "";
					} else {
						treeView1.Visible = false;
						treeView1.BeginUpdate();
						treeView1.Nodes.Clear();
						HToolkit.FillTree(value, treeView1, __StyleNodeCallback, HToolkit.EnumFillTagMode.Always);
						if (treeView1.Nodes.Count == 0) {
							treeView1.SelectedNode = null;
						} else {
							treeView1.SelectedNode = treeView1.Nodes[0];
						}
						treeView1.EndUpdate();
						treeView1.Visible = true;
					}
					UpdateComponentStates();
				}
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void UpdateComponentStates()
		{
			bool bValid = rootNode != null;
			foreach (Button btn in buttons) {
				btn.Enabled = bValid;
			}
		}

		public Button AddButton(string title)
		{
			Button btn = new Button();
			btn.Text = title;
			btn.Bounds = btnCollapse.Bounds;
			btn.Width = 125;
			btn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			btn.Location = new Point(Width - rightMargin - btn.Width, btn.Top);
			rightMargin += btn.Width + 6;
			panel1.Controls.Add(btn);
			buttons.Add(btn);
			return btn;
		}

		public void CollapseNodes(params string[] nodeNames)
		{
			treeView1.SuspendLayout();
			treeView1.BeginUpdate();
			__CollapseNodes(treeView1.Nodes, nodeNames);
			treeView1.EndUpdate();
			treeView1.ResumeLayout();
		}

		private void __CollapseNodes(TreeNodeCollection nodes, string[] nodeNames)
		{
			for (int i = nodes.Count - 1; i >= 0; i--) {
				if (nodes[i].Tag is HElement) {
					HElement e = (HElement)(nodes[i].Tag);
					bool b = false;
					foreach (string nodeName in nodeNames) {
						if (e.Name.Equals(nodeName)) {
							b = true;
							break;
						}
					}
					if (b) {
						nodes[i].Collapse();
					} else {
						__CollapseNodes(nodes[i].Nodes, nodeNames);
					}
				}
			}
		}

		private void __StyleNodeCallback(TreeNode node, HAbstractElement element)
		{
			if (element is HText) {
				node.NodeFont = FONT;
				node.ForeColor = Color.DarkGreen;
			} else {
				// node.NodeFont = treeView1.Font;
				// node.ForeColor = Color.Black;
			}
		}

		private void btnCollapse_Click(object sender, EventArgs e)
		{
			string s = edtNodeName.Text.Trim();
			if (s.Length == 0) return;
			CollapseNodes(s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
		}

		private void miCopy_Click(object sender, EventArgs e)
		{
			TreeNode n = treeView1.SelectedNode;
			if (n == null) return;
			Clipboard.SetText(n.Text, TextDataFormat.UnicodeText);
		}

		private HAbstractElement[] __BuildSimplePathForNode(TreeNode node)
		{
			List<HAbstractElement> ret = new List<HAbstractElement>();

			TreeNode n = node;
			while (n != null) {
				ret.Insert(0, (HAbstractElement)(n.Tag));
				n = n.Parent;
			}

			return ret.ToArray();
		}

		private string __BuildFullPathForNode(TreeNode node)
		{
			List<string> ret = new List<string>();

			{
				TreeNode n = node;
				while (n != null) {
					string s = __BuildExpressionForSingleNode(n);
					ret.Insert(0, s);
					n = n.Parent;
				}
			}

			{
				StringBuilder sb = new StringBuilder();
				foreach (string s in ret) {
					sb.Append("/");
					sb.Append(s);
				}
				return sb.ToString();
			}
		}

		private string __BuildExpressionForSingleNode(TreeNode node)
		{
			StringBuilder sb = new StringBuilder();
			HAbstractElement nodeAE = (HAbstractElement)(node.Tag);
			if (nodeAE is HText) {
				HText nodeAE_Text = (HText)nodeAE;
				sb.Append("§text='" + nodeAE_Text.Text + "'");
			} else {
				HElement nodeAE_Element = (HElement)nodeAE;
				sb.Append(nodeAE_Element.Name);
				foreach (HAttribute a in nodeAE_Element.Attributes) {
					sb.Append("[@" + a.Name + "='" + a.Value + "']");
				}
			}

			return sb.ToString();
		}

		private string __BuildExpression(HAbstractElement[] path)
		{
			StringBuilder sb = new StringBuilder();
			foreach (HAbstractElement e in path) {
				sb.Append("/");
				if (e is HElement) {
					HElement he = (HElement)e;
					sb.Append(he.Name);
				} else
				if (e is HText) {
					HText ht = (HText)e;
					sb.Append("§text=\'");
					sb.Append(ht.Text);			// TODO: mask single quotation marks!
					sb.Append("\'");
				} else {
					throw new ImplementationErrorException();
				}
			}
			return sb.ToString();
		}

		private void miCreateExpressionFullPath_Click(object sender, EventArgs e)
		{
			TreeNode n = treeView1.SelectedNode;
			if (n == null) return;

			string expr = __BuildFullPathForNode(n);

			Clipboard.SetText(expr, TextDataFormat.UnicodeText);
		}

		private void treeView1_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & System.Windows.Forms.MouseButtons.Right) != 0) {
				TreeViewHitTestInfo hti = treeView1.HitTest(e.Location);
				if (hti.Location == TreeViewHitTestLocations.Label) {
					treeView1.SelectedNode = hti.Node;
					contextMenuStrip1.Show(treeView1, e.Location);
				}
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode n = treeView1.SelectedNode;
			if (n == null) {
				textBox1.Text = "";
				return;
			}

			if (!(n.Tag is HText)) {
				textBox1.Text = "";
				return;
			}

			HText t = (HText)(n.Tag);
			textBox1.Text = t.Text;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			searchFormController.Show();
		}

		private void miCreateExpressionNode_Click(object sender, EventArgs e)
		{
			TreeNode n = treeView1.SelectedNode;
			if (n == null) return;

			string expr = __BuildExpressionForSingleNode(n);

			Clipboard.SetText(expr, TextDataFormat.UnicodeText);
		}

		/*
		private class V : IHVisitor
		{
			private Matcher m;
			public readonly List<MatchingRecord> results;

			public V(Matcher m)
			{
				this.m = m;
				results = new List<MatchingRecord>();
			}

			public EnumVisitReturnCode Visit(Stack<HAbstractElement> parentElements, HAbstractElement element)
			{
				IMatchingResult r = m.Match(element);
				if (r != null) {
					results.AddRange(r.ToArray());
				}
				return EnumVisitReturnCode.ContinueAndDescend;
			}

			public void VisitingBegin()
			{
			}

			public void VisitingEnd(bool bResult)
			{
			}
		}
		*/

	}

}
