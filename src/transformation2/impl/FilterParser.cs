using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.transformation2.filters;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class FilterParser : AbstractComponentBasedParser<AbstractFilterParserComponent, AbstractFilter>
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

		public FilterParser()
			: base()
		{
		}

		public FilterParser(params AbstractFilterParserComponent[] parserComponents)
			: base(parserComponents)
		{
		}

		public FilterParser(IEnumerable<AbstractFilterParserComponent> parserComponents)
			: base(parserComponents)
		{
		}

		public FilterParser(params Type[] parserComponentTypes)
			: base(parserComponentTypes)
		{
		}

		public FilterParser(IEnumerable<Type> parserComponentTypes)
			: base(parserComponentTypes)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

	}

}
