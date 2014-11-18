namespace LibHTreeProcessing.src.gui
{
	partial class TransformHierarchicalDocumentForm
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.simpleTreeVisualizer1 = new LibHTreeProcessing.src.gui.SimpleTreeVisualizer();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.btnTransform = new System.Windows.Forms.Button();
			this.lblError = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.simpleTreeVisualizer2 = new LibHTreeProcessing.src.gui.SimpleTreeVisualizer();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.simpleTreeVisualizer3 = new LibHTreeProcessing.src.gui.SimpleTreeVisualizer();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miNew = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.edtPath = new System.Windows.Forms.TextBox();
			this.helpPanel1 = new LibSimpleScriptEditor.src.HelpPanel();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 32);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(952, 601);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.simpleTreeVisualizer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(944, 575);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Original";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// simpleTreeVisualizer1
			// 
			this.simpleTreeVisualizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleTreeVisualizer1.Location = new System.Drawing.Point(3, 3);
			this.simpleTreeVisualizer1.Name = "simpleTreeVisualizer1";
			this.simpleTreeVisualizer1.RootNode = null;
			this.simpleTreeVisualizer1.Size = new System.Drawing.Size(938, 569);
			this.simpleTreeVisualizer1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.splitContainer1);
			this.tabPage2.Controls.Add(this.btnTransform);
			this.tabPage2.Controls.Add(this.lblError);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(944, 575);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Transformation Rules";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(9, 10);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.helpPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(924, 520);
			this.splitContainer1.SplitterDistance = 310;
			this.splitContainer1.SplitterWidth = 20;
			this.splitContainer1.TabIndex = 4;
			// 
			// tabControl2
			// 
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(310, 520);
			this.tabControl2.TabIndex = 2;
			this.tabControl2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl2_MouseDown);
			// 
			// btnTransform
			// 
			this.btnTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTransform.Location = new System.Drawing.Point(861, 544);
			this.btnTransform.Name = "btnTransform";
			this.btnTransform.Size = new System.Drawing.Size(75, 23);
			this.btnTransform.TabIndex = 0;
			this.btnTransform.Text = "Transform";
			this.btnTransform.UseVisualStyleBackColor = true;
			this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
			// 
			// lblError
			// 
			this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblError.AutoSize = true;
			this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblError.ForeColor = System.Drawing.Color.DarkRed;
			this.lblError.Location = new System.Drawing.Point(6, 544);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(41, 13);
			this.lblError.TabIndex = 1;
			this.lblError.Text = "label1";
			this.lblError.Click += new System.EventHandler(this.lblError_Click);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.simpleTreeVisualizer2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(944, 575);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Transformation Result";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// simpleTreeVisualizer2
			// 
			this.simpleTreeVisualizer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simpleTreeVisualizer2.Location = new System.Drawing.Point(3, 3);
			this.simpleTreeVisualizer2.Name = "simpleTreeVisualizer2";
			this.simpleTreeVisualizer2.RootNode = null;
			this.simpleTreeVisualizer2.Size = new System.Drawing.Size(938, 569);
			this.simpleTreeVisualizer2.TabIndex = 0;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.simpleTreeVisualizer3);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(944, 575);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Clipboard";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// simpleTreeVisualizer3
			// 
			this.simpleTreeVisualizer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.simpleTreeVisualizer3.Location = new System.Drawing.Point(3, 3);
			this.simpleTreeVisualizer3.Name = "simpleTreeVisualizer3";
			this.simpleTreeVisualizer3.RootNode = null;
			this.simpleTreeVisualizer3.Size = new System.Drawing.Size(938, 569);
			this.simpleTreeVisualizer3.TabIndex = 1;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(111, 26);
			// 
			// miNew
			// 
			this.miNew.Name = "miNew";
			this.miNew.Size = new System.Drawing.Size(110, 22);
			this.miNew.Text = "&New ...";
			this.miNew.Click += new System.EventHandler(this.miNew_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.edtPath);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(952, 32);
			this.panel1.TabIndex = 1;
			// 
			// edtPath
			// 
			this.edtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.edtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edtPath.Location = new System.Drawing.Point(6, 6);
			this.edtPath.Name = "edtPath";
			this.edtPath.ReadOnly = true;
			this.edtPath.Size = new System.Drawing.Size(939, 13);
			this.edtPath.TabIndex = 0;
			// 
			// helpPanel1
			// 
			this.helpPanel1.AutoScroll = true;
			this.helpPanel1.BackColor = System.Drawing.Color.White;
			this.helpPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpPanel1.HelpText = null;
			this.helpPanel1.Location = new System.Drawing.Point(0, 0);
			this.helpPanel1.Name = "helpPanel1";
			this.helpPanel1.Size = new System.Drawing.Size(594, 520);
			this.helpPanel1.TabIndex = 0;
			// 
			// TransformHierarchicalDocumentForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(952, 633);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.Name = "TransformHierarchicalDocumentForm";
			this.Text = "TransformPaliDocumentForm";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.Button btnTransform;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem miNew;
		private src.gui.SimpleTreeVisualizer simpleTreeVisualizer1;
		private src.gui.SimpleTreeVisualizer simpleTreeVisualizer2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox edtPath;
		private System.Windows.Forms.TabPage tabPage4;
		private SimpleTreeVisualizer simpleTreeVisualizer3;
		private LibSimpleScriptEditor.src.HelpPanel helpPanel1;
	}
}