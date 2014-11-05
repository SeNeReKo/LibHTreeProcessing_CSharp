================#===============================#=======================#=======================================================================================================================
IMPLEMENTATION	|	INPUT						|	MODIFIES			|	PATTERN
STATE			|								|	TREE				|
================#===============================#=======================#=======================================================================================================================
TODO			|	Attributelement				|	whole tree			|	move value as text to new child node "path"
IMPLEMENTED		|	Attributelement				|	attribute			|	rename attribute to "newName"
----------------+-------------------------------+-----------------------+-----------------------------------------------------------------------------------------------------------------------
IMPLEMENTED		|	Textelement					|	text				|	convert to lower case
IMPLEMENTED		|	Textelement					|	text				|	convert to upper case
IMPLEMENTED		|	Textelement					|	text				|	normalize text spaces
IMPLEMENTED		|	Textelement					|	parent child list	|	remove text chunk if empty
IMPLEMENTED		|	Textelement					|	text				|	trim text
IMPLEMENTED		|	Textelement					|	text				|	remove text spaces before delimiters "...some.punctuations..."
TODO			|	Textelement					|	parent child list	|	tokenize text with alphabet "someAlphabet" using "tokenWord" and "tokenDelim"
TODO			|	Textelement					|	whole tree			|	filter by regex "regex" add as node to "/some/path"
TODO			|	Textelement					|	whole tree			|	filter by regex "regex" add as text to "/some/path"
TODO			|	Textelement					|	whole tree			|	filter by regex "regex" move as node to "/some/path"
TODO			|	Textelement					|	whole tree			|	filter by regex "regex" move as text to "/some/path"
TODO			|	Textelement					|	whole tree			|	filter by regex "regex" move as attribute "attrName" to "/some/path"
----------------+-------------------------------+-----------------------+-----------------------------------------------------------------------------------------------------------------------
IMPLEMENTED		|	Attribut- oder Textelement	|	parent child		|	set as attribute "attrName" at last node
IMPLEMENTED		|	Attribut- oder Textelement	|	parent child		|	set as attribute "attrName" at last node "nodeName"
IMPLEMENTED		|	Attribut- oder Textelement	|	parent child		|	add as text at last node
IMPLEMENTED		|	Attribut- oder Textelement	|	parent child		|	add as text at last node "nodeName"
IMPLEMENTED		|	Attribut- oder Textelement	|	parent child		|	set as text at last node
IMPLEMENTED		|	Attribut- oder Textelement	|	parent child		|	set as text at last node "nodeName"
----------------+-------------------------------+-----------------------+-----------------------------------------------------------------------------------------------------------------------
IMPLEMENTED		|	Knotenelement				|	node				|	add attribute { "attrName" = "attrValue", ... } to node
TODO			|	Knotenelement				|	child list			|	group nodes { "nodeNameA", "nodeNameB", ... } by attribute "aname" using "newNodeName"
IMPLEMENTED		|	Knotenelement				|	child list			|	group nodes { "nodeNameA", "nodeNameB", ... } using "newNodeName"
IMPLEMENTED		|	Knotenelement				|	child list			|	group node sequences { "nodeNameA", "nodeNameB", ... } using "newNodeName"
TODO			|	Knotenelement				|	child list			|	group nodes from { pattern <patternBegin> } to { pattern <patternEnd> } using "newNodeName"
TODO			|	Knotenelement				|	child list			|	group nodes from { pattern <patternBegin> } to { pattern <patternEnd> } with line matcher "lineMatcher" using "newNodeName"
TODO			|	Knotenelement				|	child list			|	merge node sequence { "nodeNameA", "nodeNameB", ... } using "newNodeName"
IMPLEMENTED		|	Knotenelement				|	child list			|	merge text chunks
IMPLEMENTED		|	Knotenelement				|	attribute list		|	remove attribute { "attrName", ... }
IMPLEMENTED		|	Knotenelement				|	attribute list		|	remove all attributes
IMPLEMENTED		|	Knotenelement				|	child list			|	remove all children
IMPLEMENTED		|	Knotenelement				|	child list			|	remove all child text chunks
IMPLEMENTED		|	Knotenelement				|	child list			|	remove all child nodes
IMPLEMENTED		|	Knotenelement				|	child list			|	remove empty child nodes
IMPLEMENTED		|	Knotenelement				|	parent child list	|	remove node merge child elements
IMPLEMENTED		|	Knotenelement				|	node				|	rename node to name provided by attribute "attributeName"
IMPLEMENTED		|	Knotenelement				|	node				|	rename node to "newNodeName"
IMPLEMENTED		|	Knotenelement				|	node				|	invoke myProcedureName(node)
----------------+-------------------------------+-----------------------+-----------------------------------------------------------------------------------------------------------------------
IMPLEMENTED		|	Knoten- oder Textelement	|	whole path			|	create new parent "someNodeName"
----------------+-------------------------------+-----------------------+-----------------------------------------------------------------------------------------------------------------------
IMPLEMENTED		|	beliebiges Element			|	whole tree			|	move to "/some/path"
IMPLEMENTED		|	beliebiges Element			|	nothing				|	noop
IMPLEMENTED		|	beliebiges Element			|	parent child list	|	remove
----------------+-------------------------------+-----------------------+-----------------------------------------------------------------------------------------------------------------------


