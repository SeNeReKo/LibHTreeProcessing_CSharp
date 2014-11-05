using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.transformation2.operations
{

	public static class IOperations
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		public static readonly Type[] ALL_PARSER_COMPONENT_TYPES = new Type[] {
			typeof(AddAsTextAtLastNode_ParserComponent),
			typeof(AddChildrenAtLastNode_ParserComponent),
			typeof(AppendTokenToAttributeAtNode_ParserComponent),
			typeof(CloneNode_ParserComponent),
			typeof(ConvertTextToLowerCase_ParserComponent),
			typeof(ConvertTextToUpperCase_ParserComponent),
			typeof(CopyToClipboard_ParserComponent),
			typeof(CopyFromClipboard_ParserComponent),
			typeof(CreateNewChildNode_ParserComponent),
			typeof(EnumerateNode_ParserComponent),
			typeof(GroupNodes_ParserComponent),
			typeof(GroupNodeSequences_ParserComponent),
			typeof(InjectParentNode_ParserComponent),
			typeof(Invoke_ParserComponent),
			typeof(MergeTextChunks_ParserComponent),
			typeof(MoveFromClipboard_ParserComponent),
			typeof(MoveTo_ParserComponent),
			typeof(MoveToClipboard_ParserComponent),
			typeof(Noop_ParserComponent),
			typeof(NormalizeTextSpaces_ParserComponent),
			typeof(Remove_ParserComponent),
			typeof(RemoveAllAttributes_ParserComponent),
			typeof(RemoveAllChildNodes_ParserComponent),
			typeof(RemoveAllChildren_ParserComponent),
			typeof(RemoveAllChildTexts_ParserComponent),
			typeof(RemoveAttributes_ParserComponent),
			typeof(RemoveEmptyChildNodes_ParserComponent),
			typeof(RemoveEmptyText_ParserComponent),
			typeof(RemoveNodeMergeChildElements_ParserComponent),
			typeof(RemoveTextSpacesBeforeDelimiters_ParserComponent),
			typeof(RenameAttribute_ParserComponent),
			typeof(RenameNode_ParserComponent),
			typeof(RenameNodeByAttributeValue_ParserComponent),
			typeof(SetAsAttributeAtLastNode_ParserComponent),
			typeof(SetAsAttributeAtNode_ParserComponent),
			typeof(SetAsTextAtLastNode_ParserComponent),
			typeof(SetAsTextAtNode_ParserComponent),
			typeof(SetAttribute_ParserComponent),
			typeof(SetAtNode_ParserComponent),
			typeof(SetChildrenAtLastNode_ParserComponent),
			typeof(TrimText_ParserComponent),
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
