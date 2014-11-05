using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public class HAttribute : HAbstractElement
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public string Name;
		public string Value;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public HAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Clone()
		{
			return new HAttribute(Name, Value);
		}

		public override string ToString()
		{
			return Name + "=\"" + Value + "\"";
		}

		/// <summary>
		/// Empty implementation: This method should never be called (and is never called by the simplexml-framework).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parentElements"></param>
		/// <param name="visitor"></param>
		/// <param name="bDescend"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		internal override bool DoVisit<T>(Stack<HAbstractElement> parentElements, IHVisitorCTX<T> visitor, bool bDescend, T context)
		{
			return true;
		}

		/// <summary>
		/// Empty implementation: This method should never be called (and is never called by the simplexml-framework).
		/// </summary>
		/// <param name="parentElements"></param>
		/// <param name="visitor"></param>
		/// <param name="bDescend"></param>
		/// <returns></returns>
		internal override bool DoVisit(Stack<HAbstractElement> parentElements, IHVisitor visitor, bool bDescend)
		{
			return true;
		}

		/// <summary>
		/// Empty implementation: This method should never be called (and is never called by the simplexml-framework).
		/// </summary>
		/// <param name="tw"></param>
		/// <returns></returns>
		public override bool ToPlainText(System.IO.TextWriter tw)
		{
			return false;
		}

	}

}
