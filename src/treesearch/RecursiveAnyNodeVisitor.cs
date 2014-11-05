using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class RecursiveAnyNodeVisitor : AbstractTreeNodeVisitor
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Holds the path variable that will be iterated
		/// </summary>
		HPathWithIndices path;

		/// <summary>
		/// Holds visitors of previous levels
		/// </summary>
		Stack<DirectChildVisitor> stack;

		/// <summary>
		/// Holds the current visitor. This variable is null if this object has not yet been initialized or there is no more data.
		/// </summary>
		DirectChildVisitor currentVisitor;

		/// <summary>
		/// This element gets a value if we should descend next
		/// </summary>
		HElement tryToDescend;

		HAbstractElement firstElement;

		bool bFirst;
		bool bClearFirst;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public RecursiveAnyNodeVisitor()
		{
			stack = new Stack<DirectChildVisitor>();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public override HAbstractElement CurrentElement
		{
			get {
				if (bClearFirst) return firstElement;
				if (currentVisitor == null) return null;
				return currentVisitor.CurrentElement;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override void Reset(HPathWithIndices path)
		{
			this.path = path;
			stack.Clear();
			currentVisitor = new DirectChildVisitor(path);
			tryToDescend = null;

			firstElement = path.Peek().Element;
			bFirst = true;
			bClearFirst = false;
		}

		public override bool Next()
		{
			if (bFirst) {
				bFirst = false;
				bClearFirst = true;
				return true;
			}
			if (bClearFirst) {
				bClearFirst = false;
				firstElement = null;
			}

			while (currentVisitor != null) {
				if (tryToDescend != null) {
					// try to push the current iterator on the stack and initialize a new one
					stack.Push(currentVisitor);
					currentVisitor = new DirectChildVisitor(path);
					tryToDescend = null;
				}

				if (currentVisitor.Next()) {
					// we succeeded -> must return true later

					PathStruct ps = path.Peek();
					if (ps.Element is HElement) {	// can we descend next time this method is called?
						HElement he = (HElement)(ps.Element);
						if (he.Children.Count > 0) {
							// yes -> prepare for descending
							tryToDescend = he;
						}
					}

					return true;
				}

				// the current visitor reached it's end of life => ascend

				currentVisitor = (stack.Count > 0) ? stack.Pop() : null;
			}

			return false;	// no more data
		}

		public override string ToString()
		{
			return "RecursiveAnyNodeVisitor";
		}

	}

}
