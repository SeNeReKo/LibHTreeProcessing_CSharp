using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;


namespace LibHTreeProcessing.src.gui
{

	public partial class SearchForm : Form
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		TreeView treeView;
		List<TreeNode> results;
		volatile bool bPreventEvents;
		HToolkit.StyleNodeDelegate styleNodeDelegate;
		SearchFormController controller;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public SearchForm(SearchFormController controller, TreeView treeView, HToolkit.StyleNodeDelegate styleNodeDelegate)
		{
			bPreventEvents = true;

			this.controller = controller;
			this.styleNodeDelegate = styleNodeDelegate;
			this.treeView = treeView;

			InitializeComponent();

			UpdateComponentStates();
			bPreventEvents = false;

			results = new List<TreeNode>();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public string Expression
		{
			get {
				string s = edtExpression.Text;
				if (s.Length == 0) return null;
				return s;
			}
			set {
				edtExpression.Text = value;
			}
		}

		protected HExpression ExpressionCompiled
		{
			get {
				try {
					string s = Expression;
					HExpression m = HExpressionCompiler.Compile(s, true);
					return m;
				} catch (Exception ee) {
					return null;
				}
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private void __ClearMarks(TreeNodeCollection tc)
		{
			foreach (TreeNode n in results) {
				n.NodeFont = null;
				n.ForeColor = treeView.ForeColor;
				n.BackColor = treeView.BackColor;
			}
			results.Clear();

			____RestyleNodes(tc);
		}

		private void ____RestyleNodes(TreeNodeCollection tc)
		{
			foreach (TreeNode n in tc) {
				styleNodeDelegate(n, (HAbstractElement)(n.Tag));
				____RestyleNodes(n.Nodes);
			}
		}

		/*
		private void __MarkNode(TreeNodeCollection tc, Matcher m)
		{
			foreach (TreeNode n in tc) {
				if (n.Tag is HAbstractElement) {
					HAbstractElement e = (HAbstractElement)(n.Tag);
					if (m.MatchOne(e) != null) {
						n.BackColor = Color.DarkRed;
						n.ForeColor = Color.White;
						results.Add(n);
					}
				}
				__MarkNode(n.Nodes, m);
			}
		}
		*/

		private void __MarkNode(TreeNodeCollection tc, HPathWithIndices path, int pathIndex)
		{
			PathStruct ps = path[pathIndex];
			TreeNode n = tc[ps.Index];
			if (pathIndex == path.Count - 1) {
				n.BackColor = Color.DarkRed;
				n.ForeColor = Color.White;
				results.Add(n);
			} else {
				__MarkNode(n.Nodes, path, pathIndex + 1);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{

			HExpression m = ExpressionCompiled;
			MatchResultGroup mrg = m.MatchAll((HAbstractElement)(treeView.Nodes[0].Tag));

			bPreventEvents = true;

			treeView.BeginUpdate();
			treeView.SuspendLayout();
			__ClearMarks(treeView.Nodes);
			foreach (MatchResult mr in mrg) {
				__MarkNode(treeView.Nodes, mr.Path, 0);
			}
			treeView.ResumeLayout();
			treeView.EndUpdate();

			hScrollBar1.Value = 0;
			hScrollBar1.Maximum = results.Count;

			bPreventEvents = false;

			UpdateComponentStates();
			__SelectResult(0);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				Visible = false;
				e.Cancel = true;
				controller.Hide();
			} else {
				base.OnFormClosing(e);
			}
		}

		private void edtExpression_TextChanged(object sender, EventArgs e)
		{
			UpdateComponentStates();
		}

		protected void UpdateComponentStates()
		{
			bool b = false;
			try {
				string s = Expression;
				HExpression m = HExpressionCompiler.Compile(s, true);
				b = true;
			} catch (Exception ee) {
			}

			btnSearch.Enabled = b;

			if ((results == null) || (results.Count == 0)) {
				label5.Text = "----";
			} else {
				label5.Text = (hScrollBar1.Value + 1) + " / " + results.Count;
			}
		}

		private void __SelectResult(int index)
		{
			if ((index >= 0) && (index < results.Count)) {
				treeView.SelectedNode = results[index];
				treeView.TopNode = results[index];
			} else {
				treeView.SelectedNode = null;
			}
			UpdateComponentStates();

			Control c = treeView.Parent;
			while (c != null) {
				if (c is Form) {
					treeView.Select();
					Form f = (Form)c;
					f.BringToFront();
					break;
				} else {
					c = c.Parent;
				}
			}
		}

		private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			if (bPreventEvents) return;

			__SelectResult(e.NewValue);
		}

	}

}
