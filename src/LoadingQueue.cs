using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src
{

	/// <summary>
	/// This class manages the list of loaded files.
	/// </summary>
	public class LoadingQueue
	{

		/// <summary>
		/// Perform loading. Throw an exception on error.
		/// </summary>
		/// <param name="to"></param>
		public delegate void OnLoadDataDelegate(TagObject to);

		public event OnLoadDataDelegate OnLoadData;

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private LinkedList<TagObject> loadedNodes;
		private HToolkit.StyleNodeDelegate styleNodeDelegate;

		private TreeView treeView;

		private int maxExpandedNodes;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public LoadingQueue(TreeView treeView, HToolkit.StyleNodeDelegate styleNodeDelegate, int maxExpandedNodes)
		{
			this.styleNodeDelegate = styleNodeDelegate;
			this.treeView = treeView;
			this.loadedNodes = new LinkedList<TagObject>();
			this.maxExpandedNodes = maxExpandedNodes;

			treeView.BeforeExpand += new TreeViewCancelEventHandler(treeView_BeforeExpand);
			treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		internal HToolkit.StyleNodeDelegate StyleNodeDelegate
		{
			get {
				return styleNodeDelegate;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private string[] __BuildTitlePath(TreeNode node)
		{
			return node.FullPath.Split('\\');
		}

		public TagObject CreateTagObjectForNode(TreeNode node, string filePath)
		{
			List<string> titlePath = __BuildTitlePath(node).ToList();
			titlePath.RemoveAt(0);

			TagObject to = new TagObject(this, filePath, node, titlePath.ToArray());
			node.Tag = to;
			return to;
		}

		internal bool LoadData(TagObject to)
		{
			try {
				if (to.bIsLoaded) return true;
				OnLoadData(to);
				loadedNodes.AddFirst(to);

				if (loadedNodes.Count > maxExpandedNodes) {
					TagObject toToUnload = loadedNodes.Last.Value;
					toToUnload.Unload();
				}

				return true;
			} catch (Exception ee) {
				Console.Out.WriteLine(ee);
				return false;
			}
		}

		internal void AfterUnloadingData(TagObject to)
		{
			loadedNodes.Remove(to);
		}

		private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Tag == null) return;
			if (e.Node.Tag is TagObject) {
				TagObject to = (TagObject)(e.Node.Tag);
				to.Load();
			}
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
		}

		public void BringToTop(TagObject to)
		{
			if ((loadedNodes.First != null) && (loadedNodes.First.Value == to)) return;

			loadedNodes.Remove(to);
			loadedNodes.AddFirst(to);
		}

	}

}
