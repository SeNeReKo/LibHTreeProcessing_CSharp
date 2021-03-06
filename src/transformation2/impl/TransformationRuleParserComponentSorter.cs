﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class TransformationRuleParserComponentSorter<T> : IComparer<IParserComponent<T>>
			where T : class
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public TransformationRuleParserComponentSorter()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public int Compare(IParserComponent<T> x, IParserComponent<T> y)
		{
			string xx = __Join(x.ShortHelp);
			string yy = __Join(y.ShortHelp);
			return xx.CompareTo(yy);
		}

		private static string __Join(string[] list)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < list.Length; i++) {
				if (i > 0)
					sb.Append(' ');
				sb.Append(list[i].Trim());
			}
			return sb.ToString();
		}
	}

}
