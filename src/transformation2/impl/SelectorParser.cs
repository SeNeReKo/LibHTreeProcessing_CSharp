using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.transformation2.selectors;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class SelectorParser : AbstractComponentBasedParser<AbstractSelectorParserComponent, AbstractSelector>
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

		public SelectorParser()
		{
		}

		public SelectorParser(params AbstractSelectorParserComponent[] parserComponents)
			: base(parserComponents)
		{
		}

		public SelectorParser(IEnumerable<AbstractSelectorParserComponent> parserComponents)
			: base(parserComponents)
		{
		}

		public SelectorParser(params Type[] parserComponentTypes)
			: base(parserComponentTypes)
		{
		}

		public SelectorParser(IEnumerable<Type> parserComponentTypes)
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
