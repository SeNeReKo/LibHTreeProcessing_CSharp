using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class DirectNodeVisitor : AbstractTreeNodeVisitor
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		HAbstractElement node;
		HAbstractElement current;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public DirectNodeVisitor()
		{
		}

		public DirectNodeVisitor(HPathWithIndices path)
		{
			Reset(path);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override HAbstractElement CurrentElement
		{
			get {
				return current;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Reset(HPathWithIndices path)
		{
			node = path.Peek().Element;
			current = null;
		}

		public override bool Next()
		{
			if (node != null) {
				current = node;
				node = null;
				return true;
			} else {
				current = null;
				return false;
			}
		}

		public override string ToString()
		{
			return "DirectNodeVisitor";
		}

	}

}
