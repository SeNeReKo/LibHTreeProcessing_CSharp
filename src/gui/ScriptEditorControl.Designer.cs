namespace LibHTreeProcessing.src.gui
{
	partial class ScriptEditorControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.edtHelp = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miClose = new System.Windows.Forms.ToolStripMenuItem();
			this.edtScript = new LibHTreeProcessing.src.gui.SourceCodeTextControl();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.edtScript);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(829, 428);
			this.splitContainer1.SplitterDistance = 311;
			this.splitContainer1.SplitterWidth = 20;
			this.splitContainer1.TabIndex = 5;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(498, 428);
			this.tabControl1.TabIndex = 4;
			this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.edtHelp);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(490, 402);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Help";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// edtHelp
			// 
			this.edtHelp.BackColor = System.Drawing.SystemColors.Window;
			this.edtHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.edtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edtHelp.ForeColor = System.Drawing.Color.DimGray;
			this.edtHelp.Location = new System.Drawing.Point(3, 3);
			this.edtHelp.Multiline = true;
			this.edtHelp.Name = "edtHelp";
			this.edtHelp.ReadOnly = true;
			this.edtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.edtHelp.Size = new System.Drawing.Size(484, 396);
			this.edtHelp.TabIndex = 3;
			this.edtHelp.WordWrap = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClose});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
			// 
			// miClose
			// 
			this.miClose.Name = "miClose";
			this.miClose.Size = new System.Drawing.Size(103, 22);
			this.miClose.Text = "&Close";
			this.miClose.Click += new System.EventHandler(this.miClose_Click);
			// 
			// edtScript
			// 
			this.edtScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edtScript.IsChanged = false;
			this.edtScript.Location = new System.Drawing.Point(0, 0);
			this.edtScript.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.edtScript.Name = "edtScript";
			this.edtScript.Size = new System.Drawing.Size(311, 428);
			this.edtScript.TabIndex = 0;
			this.edtScript.OnTextChangedDelayed += new LibHTreeProcessing.src.gui.SourceCodeTextControl.TextEventDelegate(this.edtScript_OnTextChangedDelayed);
			this.edtScript.OnTextChangedImmediate += new LibHTreeProcessing.src.gui.SourceCodeTextControl.TextEventDelegate(this.edtScript_OnTextChangedImmediate);
			// 
			// ScriptEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ScriptEditorControl";
			this.Size = new System.Drawing.Size(829, 428);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem miClose;
		private System.Windows.Forms.TextBox edtHelp;
		private SourceCodeTextControl edtScript;
	}
}
