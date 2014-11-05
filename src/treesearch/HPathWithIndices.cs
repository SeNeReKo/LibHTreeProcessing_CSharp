using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class HPathWithIndices : AbstractPathStack<PathStruct>
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

		public HPathWithIndices()
			: base()
		{
		}

		public HPathWithIndices(HAbstractElement element)
			: base(new PathStruct(element, 0))
		{
		}

		public HPathWithIndices(PathStruct element)
			: base(element)
		{
		}

		private HPathWithIndices(PathStruct[] copyFromThis, int count)
			: base(copyFromThis, count)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		protected override void ItemToString(PathStruct item, StringBuilder sb)
		{
			sb.Append(item.Element.ToString());
			sb.Append('[');
			sb.Append(item.Index + "]");
		}

		public AbstractPathStack<PathStruct> Push(HAbstractElement element, int index)
		{
			return Push(new PathStruct(element, index));
		}

		public HPath ToPath()
		{
			HPath path = new HPath(count);
			for (int i = 0; i < count; i++) {
				path.Push(items[i].Element);
			}
			return path;
		}

		public static HPathWithIndices Clone(HPathWithIndices p)
		{
			return new HPathWithIndices(p.items, p.count);
		}

		public PathStruct ParentOf(HAbstractElement e)
		{
			if (e is HAttribute) {
				HAttribute a = (HAttribute)e;
				for (int i = count - 1; i >= 0; i--) {
					if (items[i].Element is HElement) {
						HElement he = (HElement)(items[i].Element);
						if (he.Attributes.Contains(a)) return items[i];
					}
				}
				return default(PathStruct);
			} else {
				for (int i = count - 1; i >= 1; i--) {
					if (items[i].Element == e) return items[i - 1];
				}
				return default(PathStruct);
			}
		}

	}

}
