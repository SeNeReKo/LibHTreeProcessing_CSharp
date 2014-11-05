﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.stringmatching
{

	public class StringMatcherEndsWith : AbstractStringMatcher
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		string textFragment;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public StringMatcherEndsWith(string textFragment)
		{
			this.textFragment = textFragment;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override bool Match(string text)
		{
			return text.EndsWith(textFragment);
		}

		public override string ToString()
		{
			return "ends with \"" + textFragment + "\"";
		}

	}

}
