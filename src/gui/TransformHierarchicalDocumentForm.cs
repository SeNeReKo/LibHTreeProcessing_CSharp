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

using LibLightweightGUI.src.textmodel;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src;
using LibHTreeProcessing.src.transformation2;


namespace LibHTreeProcessing.src.gui
{

	public partial class TransformHierarchicalDocumentForm : Form
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		private static readonly Font FONT_MONOSPACED = new Font(FontFamily.GenericMonospace, 8.25f, FontStyle.Regular);

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		IParsingContext parsingContext;
		ScriptCompiler parser;
		HElement result;
		ScriptManager scriptManager;
		HElement docRoot;
		HierarchyPath hierarchyPath;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public TransformHierarchicalDocumentForm(ScriptCompiler parser, IParsingContext parsingContext, ScriptManager scriptManager, TagObject to)
			: this(parser, parsingContext, scriptManager, to.RootElement, to.DocumentHierarchyPath)
		{
		}

		public TransformHierarchicalDocumentForm(ScriptCompiler parser, IParsingContext parsingContext, ScriptManager scriptManager,
			HElement docRoot, HierarchyPath hierarchyPath)
		{
			this.docRoot = docRoot;
			this.hierarchyPath = hierarchyPath;
			this.scriptManager = scriptManager;
			this.parser = parser;
			this.parsingContext = parsingContext;

			InitializeComponent();

			lblError.Text = "";

			simpleTreeVisualizer1.RootNode = docRoot;

			edtPath.Text = hierarchyPath;

			TextModel helpDoc = HelpTextBuilder.CreateHelpText(parser);
			ModelToLightweightControlConverter.Convert(
				helpDoc,
				lwHelp.CreateLayer()
				);
			lwHelp[0].DoLayout();
			string dump = lwHelp.Dump();

			foreach (string fileNameWithoutExt in scriptManager.ScriptFileNames) {
				TabPage tabPage = new TabPage(fileNameWithoutExt);
				tabControl2.TabPages.Add(tabPage);

				SourceCodeTextControl tb = new SourceCodeTextControl();
				tb.Dock = DockStyle.Fill;
				tb.Tag = fileNameWithoutExt;
				tb.Text = scriptManager.Load(fileNameWithoutExt);
				tb.OnTextChangedDelayed += new SourceCodeTextControl.TextEventDelegate(tb_OnTextChangedDelayed);

				tabPage.Controls.Add(tb);
			}

			simpleTreeVisualizer2.AddButton("Save as XML").Click += new EventHandler(btn_SaveAsXML_Click);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		private string SelectedScriptFileName
		{
			get {
				int n = tabControl2.SelectedIndex;
				if (n < 0) return null;
				return tabControl2.TabPages[n].Text;
			}
		}

		private string SelectedScriptContent
		{
			get {
				int n = tabControl2.SelectedIndex;
				if (n < 0) return null;
				TabPage tabPage = tabControl2.TabPages[n];
				return tabPage.Controls[0].Text;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		private static string __ToPath(string[] pathElements)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in pathElements) {
				sb.Append('/');
				sb.Append(s);
			}
			return sb.ToString();
		}

		private TabPage __GetTabPage(string title)
		{
			foreach (TabPage tp in tabControl2.TabPages) {
				if (tp.Text.Equals(title)) return tp;
			}
			return null;
		}

		/*
		private string __GetTabPageContent(string title)
		{
			foreach (TabPage tp in tabControl2.TabPages) {
				if (tp.Text.Equals(title)) {
					return ((SourceCodeTextControl)(tp.Controls[0])).Text;
				}
			}
			return null;
		}
		*/

		private void btnTransform_Click(object sender, EventArgs e)
		{
			string selectedScriptContent = SelectedScriptContent;
			if (selectedScriptContent == null) return;

			IScript script;
			try {
				script = parser.Compile(parsingContext, selectedScriptContent);
			} catch (Exception ee) {
				GUIToolkit.ShowErrorMessage("A parsing error occurred!", ee);
				return;
			}

			/*
			string[] textLines = selectedScriptContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			List<AbstractTransformationRule> rules = new List<AbstractTransformationRule>();
			try {
				int lineNo = 0;
				foreach (string textLine in textLines) {
					lineNo++;

					AbstractTransformationRule rule = parser.ParseLine(textLine, lineNo);
					if (rule != TransformationRuleNoop.Instance) rules.Add(rule);
				}
			} catch (Exception ee) {
				GUIToolkit.ShowErrorMessage("A parsing error occurred!", ee);
				return;
			}
			*/

			// process

			TransformationRuleContext ctx = new TransformationRuleContext(hierarchyPath);
			ScriptRuntimeContext result;

			try {
				result = script.Process(ctx, docRoot);

				// visualize result

				simpleTreeVisualizer2.RootNode = result.Document;
				simpleTreeVisualizer3.RootNode = result.Clipboard;

				tabControl1.SelectedIndex = 2;

				// store result

				this.result = result.Document;
			} catch (Exception ee) {
				GUIToolkit.ShowErrorMessage("Script error!", ee);
			}
		}

		private void miNew_Click(object sender, EventArgs e)
		{
			string fileNameWithoutExt = TextInputForm.Show("Enter a new file name:", "Create new script file");
			if (fileNameWithoutExt == null) return;
			fileNameWithoutExt = fileNameWithoutExt.Trim();
			if (fileNameWithoutExt.Length == 0) return;

			if (scriptManager.Get(fileNameWithoutExt) != null) {
				GUIToolkit.ShowErrorMessage("A file named \"" + fileNameWithoutExt + "\" already exists!");
				return;
			}

			scriptManager.Save("", fileNameWithoutExt);

			TabPage tabPage = new TabPage(fileNameWithoutExt);
			tabControl2.TabPages.Add(tabPage);

			SourceCodeTextControl tb = new SourceCodeTextControl();
			tb.Dock = DockStyle.Fill;
			tb.Tag = fileNameWithoutExt;
			tb.OnTextChangedDelayed += new SourceCodeTextControl.TextEventDelegate(tb_OnTextChangedDelayed);

			tabPage.Controls.Add(tb);
		}

		private void tb_OnTextChangedDelayed(SourceCodeTextControl source, string text)
		{
			string fileNameWithoutExt = (string)(source.Tag);

			string content = source.Text;
			scriptManager.Save(content, fileNameWithoutExt);

			try {
				IScript script = parser.Compile(parsingContext, content);
				lblError.Visible = false;
			} catch (Exception ee) {
				lblError.Text = ee.Message;
				lblError.Visible = true;
			}
		}

		private void tabControl2_MouseDown(object sender, MouseEventArgs e)
		{
			contextMenuStrip1.Show((Control)sender, e.Location);
		}

		private void btn_SaveAsXML_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.CheckPathExists = true;
			sfd.AddExtension = true;
			sfd.DefaultExt = "xml";
			sfd.Title = "Save XML file";
			sfd.FileName = hierarchyPath.LastElement;
			if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

			try {
				HToolkit.XMLWriteSettings settings = new HToolkit.XMLWriteSettings();
				settings.PrintStyle = HToolkit.EnumXMLPrintStyle.Pretty;
				HToolkit.SaveAsXmlToFile(simpleTreeVisualizer2.RootNode, settings, null, true, null, null, sfd.FileName);
			} catch (Exception ee) {
				ErrorForm f = new ErrorForm("Failed to save file: " + sfd.FileName, ee);
				f.ShowDialog();
			}
		}

	}

}
