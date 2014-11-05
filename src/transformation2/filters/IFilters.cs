﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public static class IFilters
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		public static readonly Type[] ALL_PARSER_COMPONENT_TYPES = new Type[] {
			typeof(FilterAddPrefix_ParserComponent),
			typeof(FilterByRegex_ParserComponent),
			typeof(FilterResolveHtmlEntities_ParserComponent),
			typeof(TransformByClipboardMap_ParserComponent),
			typeof(TransformEntitiesByClipboardMap_ParserComponent),
		};

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

	}

}
