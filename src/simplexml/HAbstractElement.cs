using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.simplexml
{

	public abstract class HAbstractElement
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

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public abstract HAbstractElement Clone();

		public abstract bool ToPlainText(TextWriter tw);

		public bool DoVisit(IHVisitor visitor)
		{
			visitor.VisitingBegin();
			bool b = DoVisit(new Stack<HAbstractElement>(), visitor, true);
			visitor.VisitingEnd(b);
			return b;
		}

		public bool DoVisit<T>(IHVisitorCTX<T> visitor, T context)
		{
			visitor.VisitingBegin(context);
			bool b = DoVisit(new Stack<HAbstractElement>(), visitor, true, context);
			visitor.VisitingEnd(context, b);
			return b;
		}

		internal abstract bool DoVisit(Stack<HAbstractElement> parentElements, IHVisitor visitor, bool bDescend);

		internal abstract bool DoVisit<T>(Stack<HAbstractElement> parentElements, IHVisitorCTX<T> visitor, bool bDescend, T context);

	}

}
