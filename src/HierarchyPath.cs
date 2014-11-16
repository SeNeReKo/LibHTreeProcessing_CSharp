using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src
{

	public struct HierarchyPath
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		public static readonly HierarchyPath EMPTY = new HierarchyPath();

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string[] documentHierarchyPath;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public HierarchyPath(IEnumerable<string> documentHierarchyPath)
		{
			this.documentHierarchyPath = (new List<string>(documentHierarchyPath)).ToArray();
		}

		public HierarchyPath(params string[] documentHierarchyPath)
		{
			this.documentHierarchyPath = documentHierarchyPath;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public string LastElement
		{
			get {
				if (documentHierarchyPath == null) return null;
				int n = documentHierarchyPath.Length - 1;
				if (n < 0) return null;
				return documentHierarchyPath[n];
			}
		}

		public string[] Elements
		{
			get {
				return documentHierarchyPath;
			}
			/*
			set {
				if (value == null) value = new string[0];
				this.documentHierarchyPath = value;
			}
			*/
		}

		public string this[int index]
		{
			get {
				if ((documentHierarchyPath == null) || (index < 0) || (index >= documentHierarchyPath.Length)) return null;
				return documentHierarchyPath[index];
			}
		}

		public int Count
		{
			get {
				if (documentHierarchyPath == null) return 0;
				return documentHierarchyPath.Length;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (documentHierarchyPath != null) {
				for (int i = 0; i < documentHierarchyPath.Length; i++) {
					if (i > 0) sb.Append(" | ");
					sb.Append(documentHierarchyPath[i]);
				}
			}
			return sb.ToString();
		}

		public static implicit operator string(HierarchyPath path)
		{
			return path.ToString();
		}

		public static implicit operator string[](HierarchyPath path)
		{
			if (path.documentHierarchyPath == null) return new string[0];
			return path.documentHierarchyPath;
		}

		public static implicit operator HierarchyPath(string[] pathElements)
		{
			return new HierarchyPath(pathElements);
		}

		public static implicit operator HierarchyPath(string path)
		{
			return Parse(path);
		}

		public static HierarchyPath Parse(string path)
		{
			if (path.StartsWith("/")) {
				return new HierarchyPath(path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
			} else {
				string[] list = path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < list.Length; i++) {
					list[i] = list[i].Trim();
				}
				return new HierarchyPath(list);
			}
		}

	}

}
