using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;


using LibNLPCSharp.util;


namespace LibHTreeProcessing.src.simplexml
{

	public partial class HToolkit
	{

		[Flags]
		public enum EnumXMLFlags : int
		{
			None = 0,
			ConserveInlineEntities = 1,
		}

		public enum EnumXMLTextOutputEncoding
		{
			AlwaysAsIs,
			OnReservedCharsOutputTextAsCData,
			EncodeReservedCharsAsEntities
		}

		public enum EnumXMLPrintStyle
		{
			SingleLine,
			Simple,
			Pretty
		}

		public class XMLWriteSettings
		{
			public EnumXMLPrintStyle PrintStyle = EnumXMLPrintStyle.Pretty;
			public EnumXMLTextOutputEncoding TextEncoding = EnumXMLTextOutputEncoding.EncodeReservedCharsAsEntities;
			public EnumXMLTextOutputEncoding AttributeEncoding = EnumXMLTextOutputEncoding.EncodeReservedCharsAsEntities;
		}

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

		private static void __WriteXmlSpecialTag(HElement e, TextWriter w)
		{
			w.Write("<?");
			w.Write(e.Name);
			if (e.Attributes.Count > 0) {
				foreach (HAttribute a in e.Attributes) {
					w.Write(' ');
					w.Write(a.Name);
					if (a.Value != null) {
						w.Write("=\"");
						w.Write(a.Value);
						w.Write('\"');
					}
				}
			}
			w.Write("?>");
		}

		private static void __WriteXmlOpeningTag(XMLWriteSettings xmlWriteSettings, HElement e, TextWriter w)
		{
			w.Write('<');
			w.Write(e.Name);
			if (e.Attributes.Count > 0) {
				foreach (HAttribute a in e.Attributes) {
					w.Write(' ');
					w.Write(a.Name);
					w.Write("=\"");
					if ((a.Value != null) && (a.Value.Length > 0)) __AddXmlAttributeValue(xmlWriteSettings, a.Value, w);
					w.Write("\"");
				}
			}
			w.Write('>');
		}

		private static void __WriteXmlOpeningClosingTag(XMLWriteSettings xmlWriteSettings, HElement e, TextWriter w)
		{
			w.Write('<');
			w.Write(e.Name);
			if (e.Attributes.Count > 0) {
				foreach (HAttribute a in e.Attributes) {
					w.Write(' ');
					w.Write(a.Name);
					w.Write("=\"");
					if ((a.Value != null) && (a.Value.Length > 0)) __AddXmlAttributeValue(xmlWriteSettings, a.Value, w);
					w.Write("\"");
				}
			}
			w.Write("/>");
		}

		private static void __WriteXmlClosingTag(HElement e, TextWriter w)
		{
			w.Write("</");
			w.Write(e.Name);
			w.Write('>');
		}

		private static void __AddXmlText(XMLWriteSettings xmlWriteSettings, HText htext, TextWriter w)
		{
			string text = htext.Text;
			if (text.Length == 0) return;

			switch (xmlWriteSettings.TextEncoding) {
				case EnumXMLTextOutputEncoding.AlwaysAsIs: {
						w.Write(text);
					}
					break;
				case EnumXMLTextOutputEncoding.EncodeReservedCharsAsEntities: {
						StringBuilder sb = new StringBuilder();
						foreach (char c in text) {
							switch (c) {
								case '"':
									sb.Append("&quot;");
									break;
								case '&':
									sb.Append("&amp;");
									break;
								case '<':
									sb.Append("&lt;");
									break;
								case '>':
									sb.Append("&gt;");
									break;
								default:
									sb.Append(c);
									break;
							}
						}
						w.Write(sb.ToString());
					}
					break;
				case EnumXMLTextOutputEncoding.OnReservedCharsOutputTextAsCData: {
						if ((text.IndexOf('&') >= 0) || (text.IndexOf('>') >= 0) || (text.IndexOf('<') >= 0)) {
							w.Write("<![CDATA[");
							if (text.Contains("<![CDATA["))
								throw new Exception("Text may not contain \"<![CDATA[\"! Recursive CDATA-definitions are not allowed!");
							w.Write(text);
							w.Write("]]>");
						} else {
							w.Write(text);
						}
					}
					break;
				default:
					throw new ImplementationErrorException();
			}
		}

		private static void __AddXmlAttributeValue(XMLWriteSettings xmlWriteSettings, string text, TextWriter w)
		{
			if (text.Length == 0) return;

			switch (xmlWriteSettings.AttributeEncoding) {
				case EnumXMLTextOutputEncoding.AlwaysAsIs: {
						w.Write(text);
					}
					break;
				case EnumXMLTextOutputEncoding.EncodeReservedCharsAsEntities: {
						StringBuilder sb = new StringBuilder();
						foreach (char c in text) {
							switch (c) {
								case '"':
									sb.Append("&quot;");
									break;
								case '&':
									sb.Append("&amp;");
									break;
								case '<':
									sb.Append("&lt;");
									break;
								case '>':
									sb.Append("&gt;");
									break;
								default:
									sb.Append(c);
									break;
							}
						}
						w.Write(sb.ToString());
					}
					break;
				case EnumXMLTextOutputEncoding.OnReservedCharsOutputTextAsCData:
					throw new Exception("Attributes are not allowed to contain CData!");
				default:
					throw new ImplementationErrorException();
			}
		}

		private static void __AddXmlPretty(XMLWriteSettings xmlWriteSettings, string indent, HElement e, CheckOutputTextAsInlineDelegate checkInline,
			bool bForceInline, TextWriter w)
		{
			if (bForceInline) {
				__WriteXmlOpeningTag(xmlWriteSettings, e, w);
				foreach (HAbstractElement eChild in e.Children) {
					if (eChild is HElement) {
						__AddXmlPretty(xmlWriteSettings, "", (HElement)eChild, null, true, w);
					} else
					if (eChild is HText) {
						__AddXmlText(xmlWriteSettings, (HText)eChild, w);
					} else
						throw new ImplementationErrorException();
				}
				__WriteXmlClosingTag(e, w);
				return;
			}

			if (e.Children.Count == 0) {
				w.Write(indent);
				__WriteXmlOpeningClosingTag(xmlWriteSettings, e, w);
				w.Write(Util.CRLF);
			} else {
				if (e.HasOnlyTexts) {
					w.Write(indent);
					__WriteXmlOpeningTag(xmlWriteSettings, e, w);
					foreach (HAbstractElement eChild in e.Children) {
						if (eChild is HText) {
							__AddXmlText(xmlWriteSettings, (HText)eChild, w);
						} else
							throw new ImplementationErrorException();
					}
					__WriteXmlClosingTag(e, w);
					w.Write(Util.CRLF);
				} else {
					w.Write(indent);
					__WriteXmlOpeningTag(xmlWriteSettings, e, w);
					w.Write(Util.CRLF);
					string indent2 = indent + "\t";
					foreach (HAbstractElement eChild in e.Children) {
						if (eChild is HElement) {
							__AddXmlPretty(xmlWriteSettings, indent2, (HElement)eChild,
								checkInline,
								(checkInline == null) ? false : checkInline.Invoke((HElement)eChild),
								w);
						} else
						if (eChild is HText) {
							__AddXmlText(xmlWriteSettings, (HText)eChild, w);
						} else
							throw new ImplementationErrorException();
					}
					w.Write(indent);
					__WriteXmlClosingTag(e, w);
					w.Write(Util.CRLF);
				}
			}
		}

		private static void __AddXmlSingleLine(XMLWriteSettings xmlWriteSettings, HElement e, TextWriter w)
		{
			if (e.Children.Count == 0) {
				__WriteXmlOpeningClosingTag(xmlWriteSettings, e, w);
			} else {

				__WriteXmlOpeningTag(xmlWriteSettings, e, w);
				foreach (HAbstractElement eChild in e.Children) {
					if (eChild is HText) {
						__AddXmlText(xmlWriteSettings, (HText)eChild, w);
					} else
					if (eChild is HElement) {
						__AddXmlSingleLine(xmlWriteSettings, (HElement)eChild, w);
					} else
						throw new ImplementationErrorException();
				}
				__WriteXmlClosingTag(e, w);

			}
		}

		private static void __AddXmlSimple(XMLWriteSettings xmlWriteSettings, HElement e, bool bParentIsMixedContent, TextWriter w)
		{
			if (e.Children.Count == 0) {
				__WriteXmlOpeningClosingTag(xmlWriteSettings, e, w);
				w.WriteLine();
			} else
			if (e.Children.HasTexts) {

				__WriteXmlOpeningTag(xmlWriteSettings, e, w);
				foreach (HAbstractElement eChild in e.Children) {
					if (eChild is HElement) {
						__AddXmlSimple(xmlWriteSettings, (HElement)eChild, true, w);
					} else
					if (eChild is HText) {
						__AddXmlText(xmlWriteSettings, (HText)eChild, w);
					} else
						throw new ImplementationErrorException();
				}
				__WriteXmlClosingTag(e, w);
				if (!bParentIsMixedContent) w.WriteLine();

			} else {

				__WriteXmlOpeningTag(xmlWriteSettings, e, w);
				w.WriteLine();
				foreach (HAbstractElement eChild in e.Children) {
					if (eChild is HElement) {
						__AddXmlSimple(xmlWriteSettings, (HElement)eChild, false, w);
					} else
						throw new ImplementationErrorException();
				}
				__WriteXmlClosingTag(e, w);
				w.WriteLine();

			}
		}

		////////////////////////////////////////////////////////////////

		public static void WriteAsXML(HElement root, XMLWriteSettings xmlWriteSettings, CheckOutputTextAsInlineDelegate checkInline,
			bool bXmlHeader, TextWriter w)
		{
			WriteAsXML(root, xmlWriteSettings, checkInline, bXmlHeader, null, null, w);
		}

		public static void WriteAsXML(HElement root, XMLWriteSettings xmlWriteSettings, CheckOutputTextAsInlineDelegate checkInline, bool bXmlHeader,
			IEnumerable<string> specialElements2, IEnumerable<HElement> specialElements, TextWriter w)
		{
			if (bXmlHeader) {
				w.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				if (xmlWriteSettings.PrintStyle != EnumXMLPrintStyle.SingleLine)
					w.Write(Util.CRLF);
			}
			if (specialElements2 != null) {
				foreach (string specialElement in specialElements2) {
					w.Write(specialElement);
					if (xmlWriteSettings.PrintStyle != EnumXMLPrintStyle.SingleLine)
						w.Write(Util.CRLF);
				}
			}
			if (specialElements != null) {
				foreach (HElement specialElement in specialElements) {
					__WriteXmlSpecialTag(specialElement, w);
					if (xmlWriteSettings.PrintStyle != EnumXMLPrintStyle.SingleLine)
						w.Write(Util.CRLF);
				}
			}
			switch (xmlWriteSettings.PrintStyle) {
				case EnumXMLPrintStyle.Pretty:
					__AddXmlPretty(xmlWriteSettings, "", root, checkInline, false, w);
					break;
				case EnumXMLPrintStyle.Simple:
					__AddXmlSimple(xmlWriteSettings, root, false, w);
					break;
				case EnumXMLPrintStyle.SingleLine:
					__AddXmlSingleLine(xmlWriteSettings, root, w);
					w.WriteLine();
					break;
				default:
					throw new ImplementationErrorException();
			}
		}

		public static void WriteAsXML(HElement root, XMLWriteSettings xmlWriteSettings, CheckOutputTextAsInlineDelegate checkInline, bool bXmlHeader,
			string[] specialElements2, HElement[] specialElements, TextWriter w)
		{
			if (bXmlHeader) {
				w.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				if (xmlWriteSettings.PrintStyle != EnumXMLPrintStyle.SingleLine)
					w.Write(Util.CRLF);
			}
			if (specialElements2 != null) {
				foreach (string specialElement in specialElements2) {
					w.Write(specialElement);
					if (xmlWriteSettings.PrintStyle != EnumXMLPrintStyle.SingleLine)
						w.Write(Util.CRLF);
				}
			}
			if (specialElements != null) {
				foreach (HElement specialElement in specialElements) {
					__WriteXmlSpecialTag(specialElement, w);
					if (xmlWriteSettings.PrintStyle != EnumXMLPrintStyle.SingleLine)
						w.Write(Util.CRLF);
				}
			}
			switch (xmlWriteSettings.PrintStyle) {
				case EnumXMLPrintStyle.Pretty:
					__AddXmlPretty(xmlWriteSettings, "", root, checkInline, false, w);
					break;
				case EnumXMLPrintStyle.Simple:
					__AddXmlSimple(xmlWriteSettings, root, false, w);
					break;
				case EnumXMLPrintStyle.SingleLine:
					__AddXmlSingleLine(xmlWriteSettings, root, w);
					w.WriteLine();
					break;
				default:
					throw new ImplementationErrorException();
			}
		}

		public static string WriteAsXML(HElement root, XMLWriteSettings xmlWriteSettings, CheckOutputTextAsInlineDelegate checkInline,
			bool bXmlHeader, IEnumerable<string> specialElements2, IEnumerable<HElement> specialElements)
		{
			StringWriter w = new StringWriter();
			WriteAsXML(root, xmlWriteSettings, checkInline, bXmlHeader, specialElements2, specialElements, w);
			return w.ToString();
		}

		public static string WriteAsXML(HElement root, XMLWriteSettings xmlWriteSettings, CheckOutputTextAsInlineDelegate checkInline,
			bool bXmlHeader, string[] specialElements2, params HElement[] specialElements)
		{
			StringWriter w = new StringWriter();
			WriteAsXML(root, xmlWriteSettings, checkInline, bXmlHeader, specialElements2, specialElements, w);
			return w.ToString();
		}

		////////////////////////////////////////////////////////////////

		public static void SaveAsXmlToFile(HElement root, XMLWriteSettings xmlWriteSettings, CheckOutputTextAsInlineDelegate checkInline,
			bool bXmlHeader, string[] specialElements2, HElement[] specialElements, string filePath)
		{
			using (StreamWriter w = new StreamWriter(filePath, false, Encoding.UTF8)) {
				WriteAsXML(root, xmlWriteSettings, checkInline, bXmlHeader, specialElements2, specialElements, w);
			}
		}

	}

}
