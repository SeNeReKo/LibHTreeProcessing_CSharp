using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.stringmatching;
using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class AttributeMatcher
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public AbstractStringMatcher NameMatcher;

		public AbstractStringMatcher ValueMatcher;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AttributeMatcher()
		{
		}

		public AttributeMatcher(AbstractStringMatcher nameMatcher, AbstractStringMatcher valueMatcher)
		{
			this.NameMatcher = nameMatcher;
			this.ValueMatcher = valueMatcher;
		}

		public AttributeMatcher(string emitID, AbstractStringMatcher nameMatcher, AbstractStringMatcher valueMatcher)
		{
			this.EmitID = emitID;
			this.NameMatcher = nameMatcher;
			this.ValueMatcher = valueMatcher;
		}

		public AttributeMatcher(string expectedName, string expectedValue)
		{
			if (expectedName != null) {
				this.NameMatcher = new StringMatcherEndsWith(expectedName);
			}
			if (expectedValue != null) {
				this.ValueMatcher = new StringMatcherEquals(expectedValue);
			}
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public string EmitID
		{
			get;
			set;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public bool Match(HAttribute attribute)
		{
			if (NameMatcher != null) {
				if (!NameMatcher.Match(attribute.Name)) return false;
			}

			if (ValueMatcher != null) {
				if (!ValueMatcher.Match(attribute.Value)) return false;
			}

			return true;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("[is attribute");
			if (NameMatcher != null) {
				sb.Append(", name ");
				sb.Append(NameMatcher.ToString());
			}
			if (ValueMatcher != null) {
				sb.Append(", value ");
				sb.Append(ValueMatcher.ToString());
			}
			sb.Append("]");

			return sb.ToString();
		}

	}

}
