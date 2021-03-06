﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public abstract class AbstractTreeNodeVisitor
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

		public AbstractTreeNodeVisitor()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public abstract HAbstractElement CurrentElement
		{
			get;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public abstract void Reset(HPathWithIndices path);

		public abstract bool Next();

		public abstract override string ToString();

	}

}
