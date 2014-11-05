using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class DirectChildVisitor : AbstractTreeNodeVisitor
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		HAbstractElement[] children;
		int index;
		HPathWithIndices path;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public DirectChildVisitor()
		{
			index = -1;
		}

		public DirectChildVisitor(HPathWithIndices path)
		{
			Reset(path);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override HAbstractElement CurrentElement
		{
			get {
				if ((index < 0) || (index >= children.Length))
					return null;
				return children[index];
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Reset(HPathWithIndices path)
		{
			PathStruct ps = path.Peek();
			if (ps.Element is HElement) {
				children = ((HElement)(ps.Element)).Children.ToArray();
				index = -1;
				this.path = path;
			} else {
				path = null;
				children = null;
				index = -1;
			}
		}

		public override bool Next()
		{
			if (path == null) return false;

			if (index < 0) {
				if (children.Length == 0) {
					path = null;
					return false;
				}

				index = 0;
				path.Push(new PathStruct(children[index], index));
				return true;
			} else {
				path.Pop();
				index++;
				if (index < children.Length) {
					path.Push(new PathStruct(children[index], index));
					return true;
				} else {
					path = null;
					return false;
				}
			}
		}

		public override string ToString()
		{
			return "DirectChildVisitor";
		}

	}

}
