namespace LibHTreeProcessing.src.gui
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miSelectNodeByPath = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpenInTransformationEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.edtOriginalText = new System.Windows.Forms.TextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.simpleTreeVisualizer1 = new LibHTreeProcessing.src.gui.SimpleTreeVisualizer();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.edtProcessedText = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.edtPlainText = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnTokenize = new System.Windows.Forms.Button();
			this.backgroundTaskList1 = new LibNLPCSharp.bgtask.BackgroundTaskList();
			this.contextMenuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(356, 643);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSelectNodeByPath,
            this.miOpenInTransformationEditor});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(245, 48);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// miSelectNodeByPath
			// 
			this.miSelectNodeByPath.Name = "miSelectNodeByPath";
			this.miSelectNodeByPath.Size = new System.Drawing.Size(244, 22);
			this.miSelectNodeByPath.Text = "&Select node by path ...";
			this.miSelectNodeByPath.Click += new System.EventHandler(this.miSelectNodeByPath_Click);
			// 
			// miOpenInTransformationEditor
			// 
			this.miOpenInTransformationEditor.Name = "miOpenInTransformationEditor";
			this.miOpenInTransformationEditor.Size = new System.Drawing.Size(244, 22);
			this.miOpenInTransformationEditor.Text = "&Open in transformation editor ...";
			this.miOpenInTransformationEditor.Click += new System.EventHandler(this.miOpenInTransformationEditor_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(1069, 643);
			this.splitContainer1.SplitterDistance = 356;
			this.splitContainer1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(709, 643);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.edtOriginalText);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(701, 617);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Original Text";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// edtOriginalText
			// 
			this.edtOriginalText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edtOriginalText.Location = new System.Drawing.Point(3, 3);
			this.edtOriginalText.Multiline = true;
			this.edtOriginalText.Name = "edtOriginalText";
			this.edtOriginalText.ReadOnly = true;
			this.edtOriginalText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.edtOriginalText.Size = new System.Drawing.Size(695, 611);
			this.edtOriginalText.TabIndex = 0;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.simpleTreeVisualizer1);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(701, 640);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Tree View";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// simpleTreeVisualizer1
			// 
			this.simpleTreeVisualizer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simpleTreeVisualizer1.Location = new System.Drawing.Point(3, 3);
			this.simpleTreeVisualizer1.Name = "simpleTreeVisualizer1";
			this.simpleTreeVisualizer1.RootNode = null;
			this.simpleTreeVisualizer1.Size = new System.Drawing.Size(695, 634);
			this.simpleTreeVisualizer1.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.edtProcessedText);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(701, 640);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Processed Text";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// edtProcessedText
			// 
			this.edtProcessedText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edtProcessedText.Location = new System.Drawing.Point(3, 3);
			this.edtProcessedText.Multiline = true;
			this.edtProcessedText.Name = "edtProcessedText";
			this.edtProcessedText.ReadOnly = true;
			this.edtProcessedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.edtProcessedText.Size = new System.Drawing.Size(695, 634);
			this.edtProcessedText.TabIndex = 1;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.edtPlainText);
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(701, 640);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Plaintext";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// edtPlainText
			// 
			this.edtPlainText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edtPlainText.Location = new System.Drawing.Point(0, 48);
			this.edtPlainText.Multiline = true;
			this.edtPlainText.Name = "edtPlainText";
			this.edtPlainText.ReadOnly = true;
			this.edtPlainText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.edtPlainText.Size = new System.Drawing.Size(701, 592);
			this.edtPlainText.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnTokenize);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(701, 48);
			this.panel1.TabIndex = 2;
			// 
			// btnTokenize
			// 
			this.btnTokenize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTokenize.Location = new System.Drawing.Point(618, 9);
			this.btnTokenize.Name = "btnTokenize";
			this.btnTokenize.Size = new System.Drawing.Size(75, 23);
			this.btnTokenize.TabIndex = 0;
			this.btnTokenize.Text = "Tokenize";
			this.btnTokenize.UseVisualStyleBackColor = true;
			this.btnTokenize.Click += new System.EventHandler(this.btnTokenize_Click);
			// 
			// backgroundTaskList1
			// 
			this.backgroundTaskList1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.backgroundTaskList1.Location = new System.Drawing.Point(0, 643);
			this.backgroundTaskList1.Name = "backgroundTaskList1";
			this.backgroundTaskList1.Size = new System.Drawing.Size(1069, 23);
			this.backgroundTaskList1.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1069, 666);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.backgroundTaskList1);
			this.Name = "MainForm";
			this.Text = "Main Corpus View";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.contextMenuStrip1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox edtOriginalText;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox edtPlainText;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnTokenize;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox edtProcessedText;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem miOpenInTransformationEditor;
		private System.Windows.Forms.TabPage tabPage4;
		private SimpleTreeVisualizer simpleTreeVisualizer1;
		private System.Windows.Forms.ToolStripMenuItem miSelectNodeByPath;
		private LibNLPCSharp.bgtask.BackgroundTaskList backgroundTaskList1;
	}
}

