﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.util
{

	public class StringSet : List<string>
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

		public StringSet()
		{
		}

		public StringSet(IEnumerable<string> elements)
			: base(elements)
		{
		}

		public StringSet(params string[] elements)
			: base(elements)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in this) {
				if (sb.Length > 0) {
					sb.Append(", ");
				}
				sb.Append(s);
			}
			return sb.ToString();
		}

		public StringSet CloneObject()
		{
			return new StringSet(this);
		}

	}

}