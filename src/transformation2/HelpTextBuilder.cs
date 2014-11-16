using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibLightweightGUI.src;
using LibLightweightGUI.src.textmodel;

using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2
{

	public class HelpTextBuilder
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		private static readonly TextStyle HEADING_1_STYLE = new TextStyle(Color.Black, new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold));
		private static readonly TextStyle SELECTABLE_STYLE = new TextStyle(Color.DarkBlue, new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Regular));
		private static readonly TextStyle TEXT_STYLE = new TextStyle(Color.FromArgb(255, 64, 64, 64), new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Regular));

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		private HelpTextBuilder()
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public static TextModel CreateHelpText(ScriptCompiler compiler)
		{
			TextSection sectionSpecialCommands = new TextSection(HEADING_1_STYLE, "Special Commands:");
			BuildHelpText(compiler.ExtraCmdsParser.ParserComponents, sectionSpecialCommands.Paragraphs);

			TextSection sectionSelectors = new TextSection(HEADING_1_STYLE, "Selectors:");
			BuildHelpText(compiler.SelectorsParser.ParserComponents, sectionSelectors.Paragraphs);

			TextSection sectionFilters = new TextSection(HEADING_1_STYLE, "Filters:");
			BuildHelpText(compiler.FiltersParser.ParserComponents, sectionFilters.Paragraphs);

			TextSection sectionOperations = new TextSection(HEADING_1_STYLE, "Operations:");
			BuildHelpText(compiler.OperationsParser.ParserComponents, sectionOperations.Paragraphs);

			TextModel doc = new TextModel(
				sectionSpecialCommands,
				sectionSelectors,
				sectionFilters,
				sectionOperations
				);

			return doc;
		}

		private static void BuildHelpText<T>(
			IEnumerable<IParserComponent<T>> parserComponents,
			IList<AbstractTextElement> output)
			where T : class
		{
			BuildHelpText((IParserComponent<T>[])(parserComponents.ToArray()), output);
		}

		private static void BuildHelpText<T>(
			IParserComponent<T>[] parserComponents,
			IList<AbstractTextElement> output)
			where T : class
		{
			IParserComponent<T>[] list = (new List<IParserComponent<T>>(parserComponents)).ToArray();
			Array.Sort(list, new TransformationRuleParserComponentSorter<T>());

			foreach (IParserComponent<T> a in list) {

				// ----

				AbstractTextElement mainParagraph;
				if (a.ShortHelp.Length > 1) {
					TextSequence seq = new TextSequence();
					foreach (string s in a.ShortHelp) {
						seq.Paragraphs.Add(new TextParagraph(SELECTABLE_STYLE, s, false));
					}
					mainParagraph = seq;
				} else {
					mainParagraph = new TextParagraph(SELECTABLE_STYLE, a.ShortHelp[0], false);
				}

				// ----

				TextSequence extraParagraphs = new TextSequence(6);
				string t = __DataTypesToStr(a.ValidInputDataTypes);
				if (t != null) extraParagraphs.Paragraphs.Add(new TextParagraph(TEXT_STYLE, "Input data types:    " + t, true));
				t = __DataTypesToStr(a.OutputDataTypes);
				if (t != null) extraParagraphs.Paragraphs.Add(new TextParagraph(TEXT_STYLE, "Output data types:    " + t, true));
				foreach (string s in a.LongHelpText) {
					extraParagraphs.Paragraphs.Add(new TextParagraph(TEXT_STYLE, s, true));
				}

				// ----

				TextExpandableSegment exp = new TextExpandableSegment(mainParagraph, extraParagraphs);
				output.Add(exp);
			}
		}

		private static string __DataTypesToStr(EnumDataType[] dataTypes)
		{
			if ((dataTypes == null) || (dataTypes.Length == 0)) return null;

			StringBuilder sb = new StringBuilder();
			foreach (EnumDataType dt in dataTypes) {
				if (sb.Length > 0) sb.Append(", ");
				switch (dt) {
					case EnumDataType.SingleAttribute:
						sb.Append("attribute");
						break;
					case EnumDataType.SingleNode:
						sb.Append("node");
						break;
					case EnumDataType.SingleText:
						sb.Append("text chunk");
						break;
				}
			}
			return sb.ToString();
		}

	}

}
