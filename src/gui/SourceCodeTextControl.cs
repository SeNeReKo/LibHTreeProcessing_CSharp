using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using LibNLPCSharp.util;


namespace LibHTreeProcessing.src.gui
{

	[DefaultEvent("OnTextChangedImmediate")]
	public partial class SourceCodeTextControl : UserControl
	{

		public delegate void TextEventDelegate(SourceCodeTextControl source, string text);

		public event TextEventDelegate OnTextChangedImmediate;
		public event TextEventDelegate OnTextChangedDelayed;

		private delegate void OnTextChangedHelperDelegate();

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private System.Threading.Timer timer;
		private volatile bool bPreventEvents;
		private string eventData;
		private bool bChanged;
		private OnTextChangedHelperDelegate d;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public SourceCodeTextControl()
		{
			bPreventEvents = true;

			InitializeComponent();

			timer = new System.Threading.Timer(timer_Elapsed, null,
				System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

			d = new OnTextChangedHelperDelegate(Tick);

			richTextBox1.TextChanged += new EventHandler(richTextBox1_TextChanged);

			bPreventEvents = false;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool IsChanged
		{
			get {
				return bChanged;
			}
			set {
				this.bChanged = value;
			}
		}

		public override string Text
		{
			get {
				return richTextBox1.Text;
			}
			set {
				bPreventEvents = true;
				richTextBox1.Text = value;
				bChanged = false;
				bPreventEvents = false;
				__RescheduleTimer(richTextBox1.Text);
			}
		}

		public override Font Font
		{
			get {
				return richTextBox1.Font;
			}
			set {
				if (value == null) return;
				richTextBox1.Font = value;
				richTextLineNumbers1.Font = new Font(value.FontFamily.Name, value.Size * 0.9f, FontStyle.Bold);
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			if (bPreventEvents) return;

			bChanged = true;

			string text = richTextBox1.Text;

			if (OnTextChangedImmediate != null) {
				OnTextChangedImmediate(this, text);
			}

			__RescheduleTimer(text);
		}

		private void __RescheduleTimer(string text)
		{
			timer.Change(1000, System.Threading.Timeout.Infinite);
			eventData = text;
		}

		private void timer_Elapsed(object state)
		{
			if (InvokeRequired) {
				Invoke(d); 
			} else {
				Tick();
			}
		}

		private void Tick()
		{
			if (OnTextChangedDelayed != null) {
				OnTextChangedDelayed(this, eventData);
			}
		}

	}

}
