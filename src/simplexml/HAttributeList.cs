using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public class HAttributeList : List<HAttribute>
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

		public HAttributeList()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public HAttribute this[string name]
		{
			get {
				for (int i = 0; i < Count; i++) {
					if (this[i].Name.Equals(name))
						return this[i];
				}
				return null;
			}
		}

		public string[] Names
		{
			get {
				string[] ret = new string[Count];
				for (int i = 0; i < ret.Length; i++) {
					ret[i] = this[i].Name;
				}
				return ret;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public HAttribute SetAttribute(string name, string value)
		{
			for (int i = 0; i < Count; i++) {
				if (base[i].Name.Equals(name)) {
					base[i].Value = value;
					return base[i];
				}
			}
			HAttribute a = new HAttribute(name, value);
			base.Add(a);
			return a;
		}

		public bool Remove(string name)
		{
			for (int i = 0; i < Count; i++) {
				if (base[i].Name.Equals(name)) {
					base.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		public int RemoveAll(string name)
		{
			int count = 0;
			for (int i = Count - 1; i >= 0; i--) {
				if (base[i].Name.Equals(name)) {
					base.RemoveAt(i);
					count++;
				}
			}
			return count;
		}

	}

}
