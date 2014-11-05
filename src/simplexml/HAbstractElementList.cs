using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public class HAbstractElementList : List<HAbstractElement>
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

		public HAbstractElementList()
		{
		}

		public HAbstractElementList(IEnumerable<HAbstractElement> collection)
			: base(collection)
		{
		}

		public HAbstractElementList(params HAbstractElement[] collection)
			: base(collection)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public bool HasOnlyTexts
		{
			get {
				for (int i = 0; i < Count; i++) {
					if (!(base[i] is HText)) return false;
				}
				return true;
			}
		}

		public bool HasTexts
		{
			get {
				for (int i = 0; i < Count; i++) {
					if (base[i] is HText) return true;
				}
				return false;
			}
		}

		public bool HasOnlyElements
		{
			get {
				for (int i = 0; i < Count; i++) {
					if (!(base[i] is HElement)) return false;
				}
				return true;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Visit all elements (recursively).
		/// </summary>
		/// <param name="parentElements">The parent elements or <code>null</code>.</param>
		/// <param name="visitor">The visitor</param>
		/// <param name="bDescend">If <code>true</code> recursively descend the tree</param>
		/// <returns>Returns <code>false</code> if the visitor has returned <code>false</code> once.</returns>
		public bool DoVisit(Stack<HAbstractElement> parentElements, IHVisitor visitor, bool bDescend)
		{
			HAbstractElement[] temp = ToArray();
			for (int i = 0; i < temp.Length; i++) {
				if (!temp[i].DoVisit(parentElements, visitor, bDescend))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Visit all elements (recursively).
		/// </summary>
		/// <param name="parentElements">The parent elements or <code>null</code>.</param>
		/// <param name="visitor">The visitor</param>
		/// <param name="bDescend">If <code>true</code> recursively descend the tree</param>
		/// <returns>Returns <code>false</code> if the visitor has returned <code>false</code> once.</returns>
		public bool DoVisit<T>(Stack<HAbstractElement> parentElements, IHVisitorCTX<T> visitor, bool bDescend, T context)
		{
			HAbstractElement[] temp = ToArray();
			for (int i = 0; i < temp.Length; i++) {
				if (!temp[i].DoVisit(parentElements, visitor, bDescend, context))
					return false;
			}
			return true;
		}

		/// <summary>
		/// If all items of this list are of type <code>HElement</code> this method collects them, packs them into an array
		/// and returns them. <code>null</code> is returned otherwise.
		/// </summary>
		/// <returns></returns>
		public HElement[] ToElementArray()
		{
			for (int i = 0; i < Count; i++) {
				if (!(this[i] is HElement)) return null;
			}
			HElement[] he = new HElement[Count];
			for (int i = 0; i < Count; i++) {
				he[i] = (HElement)(this[i]);
			}
			return he;
		}

		public void RemoveAllText()
		{
			for (int i = Count - 1; i >= 0; i--) {
				if (this[i] is HText) RemoveAt(i);
			}
		}

		public void RemoveAllElements()
		{
			for (int i = Count - 1; i >= 0; i--) {
				if (this[i] is HElement) RemoveAt(i);
			}
		}

	}

}
