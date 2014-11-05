using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.transformation2.operations;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public class OperationsParser : AbstractComponentBasedParser<AbstractOperationParserComponent, AbstractOperation>
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

		public OperationsParser()
			: base()
		{
		}

		public OperationsParser(params AbstractOperationParserComponent[] parserComponents)
			: base(parserComponents)
		{
		}

		public OperationsParser(IEnumerable<AbstractOperationParserComponent> parserComponents)
			: base(parserComponents)
		{
		}

		public OperationsParser(params Type[] parserComponentTypes)
			: base(parserComponentTypes)
		{
		}

		public OperationsParser(IEnumerable<Type> parserComponentTypes)
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
