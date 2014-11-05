using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class MatchingRecord
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// This is the ID the selected data is stored as in the matching context
		/// </summary>
		public readonly string EmitID;

		/// <summary>
		/// This is the path starting at the beginning of the expression till the current match
		/// </summary>
		public readonly HPathWithIndices Path;

		/// <summary>
		/// The text that has been selected: Either an attribute value or the content of a text
		/// element. If a node has been selected this element contains the node name.
		/// </summary>
		public readonly string Text;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MatchingRecord(string emitID, HPathWithIndices path, string text)
		{
			this.EmitID = emitID;
			this.Path = path;
			this.Text = text;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public HAbstractElement Parent
		{
			get {
				if (Path.Count <= 1) return null;
				PathStruct ps = Path[Path.Count - 2];
				if (ps.Element == null) return null;
				return ps.Element;
			}
		}

		/// <summary>
		/// The last element in the path denoting the element that contains the selected value.
		/// </summary>
		public HAbstractElement Element
		{
			get {
				PathStruct ps = Path.Peek();
				if (ps.Element == null) return null;
				return ps.Element;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

	}

}
