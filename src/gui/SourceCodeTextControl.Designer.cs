namespace LibHTreeProcessing.src.gui
{
	partial class SourceCodeTextControl
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
			timer.Dispose();
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.richTextLineNumbers1 = new LibHTreeProcessing.src.gui.RichTextLineNumbers();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.richTextBox1);
			this.panel1.Controls.Add(this.richTextLineNumbers1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(300, 300);
			this.panel1.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.AcceptsTab = true;
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.DetectUrls = false;
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox1.HideSelection = false;
			this.richTextBox1.Location = new System.Drawing.Point(64, 0);
			this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.Size = new System.Drawing.Size(236, 300);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// richTextLineNumbers1
			// 
			this.richTextLineNumbers1._SeeThroughMode_ = false;
			this.richTextLineNumbers1.AutoSizing = false;
			this.richTextLineNumbers1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.richTextLineNumbers1.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.richTextLineNumbers1.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
			this.richTextLineNumbers1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.richTextLineNumbers1.BorderLines_Color = System.Drawing.Color.SlateGray;
			this.richTextLineNumbers1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
			this.richTextLineNumbers1.BorderLines_Thickness = 1F;
			this.richTextLineNumbers1.Dock = System.Windows.Forms.DockStyle.Left;
			this.richTextLineNumbers1.DockSide = LibHTreeProcessing.src.gui.RichTextLineNumbers.LineNumberDockSide.Left;
			this.richTextLineNumbers1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextLineNumbers1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.richTextLineNumbers1.GridLines_Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.richTextLineNumbers1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
			this.richTextLineNumbers1.GridLines_Thickness = 1F;
			this.richTextLineNumbers1.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
			this.richTextLineNumbers1.LineNrs_AntiAlias = false;
			this.richTextLineNumbers1.LineNrs_AsHexadecimal = false;
			this.richTextLineNumbers1.LineNrs_ClippedByItemRectangle = true;
			this.richTextLineNumbers1.LineNrs_LeadingZeroes = false;
			this.richTextLineNumbers1.LineNrs_Offset = new System.Drawing.Size(0, 0);
			this.richTextLineNumbers1.Location = new System.Drawing.Point(0, 0);
			this.richTextLineNumbers1.Margin = new System.Windows.Forms.Padding(0);
			this.richTextLineNumbers1.MarginLines_Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.richTextLineNumbers1.MarginLines_Side = LibHTreeProcessing.src.gui.RichTextLineNumbers.LineNumberDockSide.Right;
			this.richTextLineNumbers1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			this.richTextLineNumbers1.MarginLines_Thickness = 1F;
			this.richTextLineNumbers1.Name = "richTextLineNumbers1";
			this.richTextLineNumbers1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.richTextLineNumbers1.ParentRichTextBox = this.richTextBox1;
			this.richTextLineNumbers1.Show_BackgroundGradient = false;
			this.richTextLineNumbers1.Show_BorderLines = false;
			this.richTextLineNumbers1.Show_GridLines = false;
			this.richTextLineNumbers1.Show_LineNrs = true;
			this.richTextLineNumbers1.Show_MarginLines = true;
			this.richTextLineNumbers1.Size = new System.Drawing.Size(64, 300);
			this.richTextLineNumbers1.TabIndex = 3;
			// 
			// SourceCodeTextControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SourceCodeTextControl";
			this.Size = new System.Drawing.Size(300, 300);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private RichTextLineNumbers richTextLineNumbers1;
	}
}
