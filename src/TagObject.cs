using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using LibNLPCSharp.util;
using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src
{

	public class TagObject
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public readonly LoadingQueue LoadingQueue;
		public readonly string FilePath;
		public readonly TreeNode Node;
		public readonly HierarchyPath DocumentHierarchyPath;

		internal bool bIsLoaded;

		/// <summary>
		/// After loading data this variable holds the raw data read from some file.
		/// </summary>
		public string OrgTextCache;

		/// <summary>
		/// After loading data this variable holds the raw data converted to plaintext.
		/// </summary>
		public string PlainTextCache;

		/// <summary>
		/// After loading this variable holds the tree root.
		/// </summary>
		public HElement RootElement;

		/// <summary>
		/// After loading this variable may hold a replacement for the tree root.
		/// </summary>
		public HAbstractElement RootElementReplacement;

		public Dictionary<string, object> AdditionalData;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		internal TagObject(LoadingQueue loadingQueue, string filePath, TreeNode node, string[] documentHierarchyPath)
		{
			this.AdditionalData = new Dictionary<string, object>();

			this.LoadingQueue = loadingQueue;
			this.FilePath = filePath;
			this.Node = node;
			this.DocumentHierarchyPath = documentHierarchyPath;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool IsLoaded
		{
			get {
				return bIsLoaded;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Synchroneous method to load the document represented by this TagObject
		/// </summary>
		/// <returns></returns>
		public bool Load()
		{
			if (IsLoaded) return true;

			bIsLoaded = LoadingQueue.LoadData(this);
			if (!bIsLoaded) {
				Unload();
				return false;
			}

			if (RootElementReplacement != null) {
				HToolkit.FillTree(RootElementReplacement, Node, LoadingQueue.StyleNodeDelegate, HToolkit.EnumFillTagMode.IfNull);
			} else {
				HToolkit.FillTree(RootElement, Node, LoadingQueue.StyleNodeDelegate, HToolkit.EnumFillTagMode.IfNull);
			}

			Node.TreeView.SelectedNode = Node;
			Node.EnsureVisible();
			return true;
		}

		public void Unload()
		{
			OrgTextCache = null;
			PlainTextCache = null;
			RootElement = null;
			RootElementReplacement = null;
			bIsLoaded = false;
			Node.Nodes.Clear();
			Node.Nodes.Add("...");
			Node.Collapse();
			LoadingQueue.AfterUnloadingData(this);
			AdditionalData.Clear();
		}

	}

}
