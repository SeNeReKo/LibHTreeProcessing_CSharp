using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.gui
{

	public class SearchFormController
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		TreeView treeView;
		HToolkit.StyleNodeDelegate styleNodeCallback;
		SearchForm searchForm;
		bool bVisible;
		bool bEnabled;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public SearchFormController(TreeView treeView, HToolkit.StyleNodeDelegate styleNodeCallback)
		{
			this.treeView = treeView;
			this.styleNodeCallback = styleNodeCallback;

			treeView.Disposed += new EventHandler(treeView_Disposed);
			treeView.VisibleChanged += new EventHandler(treeView_VisibleChanged);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool Enabled
		{
			get {
				return bEnabled;
			}
			set {
				if (bEnabled != value) {
					bEnabled = value;
					if (value) {
						// enabled
						if (bVisible) {
							searchForm.Visible = true;
							searchForm.BringToFront();
						}
					} else {
						// disabled
						if (searchForm != null) {
							if (searchForm.Visible) searchForm.Visible = false;
						}
					}
				}
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void Show()
		{
			if (searchForm == null) {
				searchForm = new SearchForm(this, treeView, styleNodeCallback);
				searchForm.Show();
			} else {
				searchForm.Visible = true;
				searchForm.BringToFront();
			}
			bEnabled = true;
			bVisible = true;
		}

		public void Hide()
		{
			bVisible = false;
			if (searchForm != null) {
				searchForm.Visible = false;
			}
		}

		private void treeView_Disposed(object sender, EventArgs e)
		{
			bVisible = false;
			if (searchForm != null) {
				searchForm.Dispose();
			}
			bEnabled = false;
			bVisible = false;
		}

		private void treeView_VisibleChanged(object sender, EventArgs e)
		{
			if ((searchForm != null) && bEnabled) {
				searchForm.Visible = bVisible;
			}
		}

	}

}
