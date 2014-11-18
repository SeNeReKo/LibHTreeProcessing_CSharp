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
using LibNLPCSharp.gui;
using LibNLPCSharp.bgtask;

using LibHTreeProcessing.src;
using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.transformation2;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.gui
{

	public partial class MainForm : Form
	{

		public delegate void OnInitializeTransformationRuleParserDelegate(
			ScriptCompiler transformationRuleParser,
			DefaultParsingContext parsingContext);

		public event OnInitializeTransformationRuleParserDelegate OnInitializeTransformationRuleParser;

		public delegate void OnLoadFileDelegate(TagObject to);

		public event OnLoadFileDelegate OnLoadFile;

		public delegate void OnInitializeTreeDelegate(LoadingQueue loadingQueue, TreeNodeCollection treeNodeCollection);

		/// <summary>Provide an event handler for <code>OnInitializeTree</code> in order to create the basic tree hierarchy:
		/// Create all tree nodes that represent the basic hierarchy following the general tree conventions.</summary>
		public event OnInitializeTreeDelegate OnInitializeTree;

		private static readonly Font FONT = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Regular);

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private LoadingQueue loadingQueue;
		private Font regularFont;
		private ScriptManager scriptManager;
		private ScriptCompiler parser;
		private IParsingContext parsingContext;
		private PersistentProperties pp;

		private List<ToolStripItem> extraContextMenuItems;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MainForm(PersistentProperties pp, ScriptManager scriptManager, int maxExpandedNodes)
		{
			this.pp = pp;
			this.scriptManager = scriptManager;
			this.extraContextMenuItems = new List<ToolStripItem>();

			InitializeComponent();

			regularFont = new Font(treeView1.Font, FontStyle.Regular);

			loadingQueue = new LoadingQueue(treeView1, __StyleNode, maxExpandedNodes);
			loadingQueue.OnLoadData += new LoadingQueue.OnLoadDataDelegate(loadingQueue_OnLoadData);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public ScriptCompiler ScriptParser
		{
			get {
				if (parser == null) __InitializeParser();
				return parser;
			}
		}

		public IParsingContext ParsingContext
		{
			get {
				return parsingContext;
			}
		}

		public BackgroundTaskList.TaskList Tasks
		{
			get {
				return backgroundTaskList1.Tasks;
			}
		}

		public TreeNode SelectedNode
		{
			get {
				return treeView1.SelectedNode;
			}
		}

		public HElement SelectedNodeElement
		{
			get {
				TreeNode n = treeView1.SelectedNode;
				if (n == null) return null;
				if (!(n.Tag is HElement)) return null;
				HElement he = (HElement)(n.Tag);
				return he;
			}
		}

		public TagObject SelectedNodeTag
		{
			get {
				TreeNode n = treeView1.SelectedNode;
				if (n == null) return null;
				if (!(n.Tag is TagObject)) return null;
				TagObject he = (TagObject)(n.Tag);
				return he;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private void __InitializeParser()
		{
			parser = new ScriptCompiler(
				new ExtraCommandsParser(transformation2.cmds.IExtraCommands.ALL_PARSER_COMPONENT_TYPES),
				new SelectorParser(transformation2.selectors.ISelectors.ALL_PARSER_COMPONENT_TYPES),
				new FilterParser(transformation2.filters.IFilters.ALL_PARSER_COMPONENT_TYPES),
				new OperationsParser(transformation2.operations.IOperations.ALL_PARSER_COMPONENT_TYPES)
				);

			DefaultParsingContext ctx = new DefaultParsingContext();

			if (OnInitializeTransformationRuleParser != null) {
				OnInitializeTransformationRuleParser(parser, ctx);
			}

			this.parsingContext = ctx;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (OnInitializeTree != null) {
				treeView1.BeginUpdate();
				treeView1.SuspendLayout();
				OnInitializeTree(loadingQueue, treeView1.Nodes);
				treeView1.ResumeLayout();
				treeView1.EndUpdate();
			}
		}

		private void loadingQueue_OnLoadData(TagObject to)
		{
			try {
				if (OnLoadFile != null) {
					OnLoadFile(to);
				}
			} catch (Exception ee) {
				GUIToolkit.ShowErrorMessage("ERROR", ee);
				to.Unload();
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TagObject to = __FindTagObject(e.Node);
			if (to == null) {
				edtOriginalText.Text = "";
				simpleTreeVisualizer1.RootNode = null;
				edtProcessedText.Text = "";
				edtPlainText.Text = "";
				return;
			}

			loadingQueue.BringToTop(to);

			if (!to.IsLoaded) {
				edtOriginalText.Text = "";
				simpleTreeVisualizer1.RootNode = null;
				edtProcessedText.Text = "";
				edtPlainText.Text = "";
				return;
			}

			edtOriginalText.Text = to.OrgTextCache;
			edtProcessedText.Text = "";
			edtPlainText.Text = to.PlainTextCache;
			simpleTreeVisualizer1.RootNode = to.RootElement;
		}

		private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
		{
			treeView1_AfterSelect(null, e);
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			TreeNode n = treeView1.SelectedNode;
			if (n == null) {
				e.Cancel = true;
				return;
			}

			if (n.Tag is TagObject) {
				TagObject to = (TagObject)(n.Tag);
				miOpenInTransformationEditor.Enabled = true;
			} else {
				miOpenInTransformationEditor.Enabled = false;
			}
		}

		private void btnTokenize_Click(object sender, EventArgs e)
		{
		}

		private void miOpenInTransformationEditor_Click(object sender, EventArgs e)
		{
			TreeNode n = treeView1.SelectedNode;
			if (n == null) return;
			if (!(n.Tag is TagObject)) return;
			TagObject to = (TagObject)(n.Tag);

			TransformHierarchicalDocumentForm form = new TransformHierarchicalDocumentForm(
				ScriptParser,
				parsingContext, scriptManager, to);
			form.Show();
		}

		private void miSelectNodeByPath_Click(object sender, EventArgs e)
		{
			string path = TextInputForm.Show("Please enter a path to select a specific node in the hierarchy:", "Select node by path");
			try {
				HierarchyPath p = HierarchyPath.Parse(path);
				SelecteNodeByPath(p);
			} catch (Exception ee) {
			}
		}

		public void SelecteNodeByPath(HierarchyPath path)
		{
			__SelecteNodeByPath(treeView1.Nodes, path, 0);
		}

		private void __SelecteNodeByPath(TreeNodeCollection nodes, HierarchyPath path, int index)
		{
			string pathElement = path[index];
			for (int i = 0; i < nodes.Count; i++) {
				if (nodes[i].Text.Equals(pathElement)) {
					if (index == path.Count - 1) {
						// last element
						treeView1.SelectedNode = nodes[i];
					} else {
						// other element
						__SelecteNodeByPath(nodes[i].Nodes, path, index + 1);
					}
					break;
				}
			}
		}

		public ToolStripMenuItem AddContextMenuItem(string text)
		{
			if (extraContextMenuItems.Count == 0) {
				contextMenuStrip1.Items.Add(new ToolStripSeparator());
			}

			ToolStripMenuItem mi = new ToolStripMenuItem(text);
			extraContextMenuItems.Add(mi);
			contextMenuStrip1.Items.Add(mi);

			return mi;
		}

		public IBackgroundTask StartTask(IBackgroundTask task, string argDisplayText)
		{
			return StartTask(task, task.GetType().Name, argDisplayText);
		}

		public IBackgroundTask StartTask(IBackgroundTask task, string ppID, string argDisplayText)
		{
			Argument[] arguments;
			if (task.ArgumentDescriptions.Length > 0) {
				ArgumentEditorForm form = new ArgumentEditorForm(pp, ppID, argDisplayText, task.ArgumentDescriptions);
				if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK) {
					return null;
				} else {
					arguments = form.Arguments;
				}
			} else {
				arguments = new Argument[0];
			}

			StartTask(task, new ArgumentList(arguments));

			return task;
		}

		public IBackgroundTask StartTask(IBackgroundTask task, ArgumentList arguments)
		{
			backgroundTaskList1.Tasks.Add(task);
			task.Start(arguments);
			return task;
		}

	}

}
