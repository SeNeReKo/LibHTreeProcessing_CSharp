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
using LibNLPCSharp.simpletokenizing;
using LibNLPCSharp.gui;

using LibHTreeProcessing.src;
using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.transformation2;


namespace LibHTreeProcessing.src.gui
{

	[DefaultEvent("OnTextChangedImmediate")]
	public partial class ScriptEditorControl : UserControl
	{

		public delegate void TextEventDelegate(string text);

		public event TextEventDelegate OnTextChangedImmediate;
		public event TextEventDelegate OnTextChangedDelayed;

		public delegate void OnActivatedDelegate(ITabPageHandle handle, bool bActivated);

		#region interface ITabPageHandle
		public interface ITabPageHandle
		{
			event OnActivatedDelegate OnActivated;

			Control Control
			{
				get;
			}

			void RemoveTabPage();
		}
		#endregion

		#region class TabPageHandle
		private class TabPageHandle : ITabPageHandle
		{
			public event OnActivatedDelegate OnActivated;

			private readonly List<TabPageHandle> tabPageHandles;
			private readonly TabControl tabControl;
			private readonly TabPage tabPage;
			private readonly Control control;

			private bool bLastActivated;

			public TabPageHandle(List<TabPageHandle> tabPageHandles, TabControl tabControl, TabPage tabPage, Control control)
			{
				this.tabPageHandles = tabPageHandles;
				this.tabControl = tabControl;
				this.tabPage = tabPage;
				this.control = control;

				tabControl.Selected += new TabControlEventHandler(tabControl_Selected);
			}

			private void tabControl_Selected(object sender, TabControlEventArgs e)
			{
				bool bActivated = tabControl.SelectedTab == tabPage;
				if (bLastActivated != bActivated) {
					bLastActivated = bActivated;
					if (OnActivated != null) {
						OnActivated(this, bActivated);
					}
				}
			}

			public Control Control
			{
				get {
					return control;
				}
			}

			public void RemoveTabPage()
			{
				if (tabPageHandles.Remove(this)) {
					tabControl.TabPages.Remove(tabPage);
				}
			}
		}
		#endregion

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private ScriptCompiler parser;

		private volatile bool bPreventEvents;

		private List<TabPageHandle> tabPageHandles;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public ScriptEditorControl()
		{
			bPreventEvents = true;

			InitializeComponent();

			edtHelp.BackColor = BackColor;

			tabPageHandles = new List<TabPageHandle>();

			bPreventEvents = false;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool IsChanged
		{
			get {
				return edtScript.IsChanged;
			}
			set {
				edtScript.IsChanged = value;
			}
		}

		[Browsable(false)]
		public string Script
		{
			get {
				return edtScript.Text;
			}
			set {
				edtScript.Text = value;
			}
		}

		public ScriptCompiler ScriptParser
		{
			get {
				return parser;
			}
			set {
				this.parser = value;

				StringBuilder sb = new StringBuilder();
				if (parser != null) {
					sb.Append(parser.ShortHelpText);
				}

				edtHelp.Text = sb.ToString();

				edtHelp.SelectionStart = 0;
				edtHelp.SelectionLength = 0;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void CloseAllTabPages()
		{
			for (int i = tabControl1.TabPages.Count - 1; i >= 1; i--) {
				tabControl1.TabPages.RemoveAt(i);
			}
			tabPageHandles.Clear();
		}

		public ITabPageHandle AddTabPage(string tabPageTitle, Control c)
		{
			TabPage tabPage = new TabPage();
			tabPage.Text = tabPageTitle;
			c.Dock = DockStyle.Fill;
			tabPage.Controls.Add(c);
			tabControl1.TabPages.Add(tabPage);
			TabPageHandle h = new TabPageHandle(tabPageHandles, tabControl1, tabPage, c);
			tabPageHandles.Add(h);
			tabControl1.SelectedTab = tabPage;
			splitContainer1.FixedPanel = FixedPanel.None;
			return h;
		}

		private void tabControl1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) {
				for (int i = 0; i < tabControl1.TabCount; i++) {
					if (i == 0) continue;

					if (tabControl1.GetTabRect(i).Contains(e.Location)) {
						tabControl1.SelectTab(i);
						contextMenuStrip1.Show(tabControl1, e.Location);
					}

				}
			}
		}

		private void miClose_Click(object sender, EventArgs e)
		{
			int n = tabControl1.SelectedIndex;
			if (n <= 0) return;
			tabPageHandles.RemoveAt(n - 1);
			tabControl1.TabPages.RemoveAt(n);
		}

		private void edtScript_OnTextChangedImmediate(SourceCodeTextControl source, string text)
		{
			if (bPreventEvents) return;

			if (OnTextChangedImmediate != null) {
				OnTextChangedImmediate(text);
			}
		}

		private void edtScript_OnTextChangedDelayed(SourceCodeTextControl source, string text)
		{
			if (bPreventEvents) return;

			if (OnTextChangedDelayed != null) {
				OnTextChangedDelayed(text);
			}
		}

	}

}
