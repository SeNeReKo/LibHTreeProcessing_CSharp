using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;

using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2
{

	public class ScriptManager
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		DirectoryInfo di;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public ScriptManager(DirectoryInfo di)
		{
			this.di = di;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public FileInfo[] ScriptFiles
		{
			get {
				di.Refresh();
				return di.GetFiles("*.script", SearchOption.TopDirectoryOnly);
			}
		}

		public string[] ScriptFileNames
		{
			get {
				di.Refresh();
				FileInfo[] files = di.GetFiles("*.script", SearchOption.TopDirectoryOnly);
				if (files == null) return new string[0];
				string[] ret = new string[files.Length];
				for (int i = 0; i < ret.Length; i++) {
					ret[i] = Path.GetFileNameWithoutExtension(files[i].Name);
				}
				return ret;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public FileInfo Get(string fileNameWithoutExtension)
		{
			di.Refresh();
			FileInfo fi = new FileInfo(Util.AddSeparatorChar(di.FullName) + fileNameWithoutExtension + ".script");
			if (fi.Exists) return fi;
			return null;
		}

		public string Load(string fileNameWithoutExtension)
		{
			di.Refresh();
			FileInfo fi = new FileInfo(Util.AddSeparatorChar(di.FullName) + fileNameWithoutExtension + ".script");
			if (fi.Exists) return File.ReadAllText(fi.FullName, Encoding.UTF8);
			return null;
		}

		public FileInfo Save(string content, string fileNameWithoutExtension)
		{
			string filePath = Util.AddSeparatorChar(di.FullName) + fileNameWithoutExtension + ".script";
			File.WriteAllText(filePath, content, Encoding.UTF8);
			FileInfo fi = new FileInfo(filePath);
			return fi;
		}

	}

}
