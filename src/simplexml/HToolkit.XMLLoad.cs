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

		private static void __CopyAttributes(XmlElement xml, HElement node, ref int countAttributes)
		{
			foreach (XmlAttribute a in xml.Attributes) {
				countAttributes++;

				node.Attributes.Add(new HAttribute(a.Name, __EncodeEntities(a.Value)));
			}
		}

		private static string __EncodeEntities(string attrValue)
		{
			StringBuilder sb = new StringBuilder();

			foreach (char c in attrValue) {
				switch (c) {
					case '<':
						sb.Append("&lt;");
						break;
					case '>':
						sb.Append("&gt;");
						break;
					case '&':
						sb.Append("&amp;");
						break;
					case '\"':
						sb.Append("&quot;");
						break;
					default:
						sb.Append(c);
						break;
				}
			}

			return sb.ToString();
		}

		private static void __CopyNodes(XmlNodeList nodes, List<HAbstractElement> target, bool bResolveEntities,
			ref int countElements, ref int countTexts, ref int countAttributes)
		{
			foreach (XmlNode n in nodes) {
				countTexts++;

				if (n is XmlText) {
					string rawText = ((XmlText)n).InnerText;
					if (!bResolveEntities) {
						StringBuilder sb = new StringBuilder();
						foreach (char c in rawText) {
							switch (c) {
								case '&':
									if (sb.Length > 0) {
										target.Add(new HText(sb.ToString()));
										sb.Remove(0, sb.Length);
									}
									target.Add(new HText("&amp;"));
									break;
								case '>':
									if (sb.Length > 0) {
										target.Add(new HText(sb.ToString()));
										sb.Remove(0, sb.Length);
									}
									target.Add(new HText("&gt;"));
									break;
								case '<':
									if (sb.Length > 0) {
										target.Add(new HText(sb.ToString()));
										sb.Remove(0, sb.Length);
									}
									target.Add(new HText("&lt;"));
									break;
								default:
									sb.Append(c);
									break;
							}
						}
						if (sb.Length > 0) {
							rawText = sb.ToString();
							target.Add(new HText(rawText));
						}
					} else {
						target.Add(new HText(rawText));
					}
				} else
				if (n is XmlEntityReference) {
					countTexts++;

					if (bResolveEntities) {
						target.Add(new HText(((XmlEntityReference)n).InnerText));
					} else {
						target.Add(new HText("&" + ((XmlEntityReference)n).Name + ";"));
					}
				} else
				if (n is XmlElement) {
					countElements++;

					XmlElement x = (XmlElement)n;
					string name;
					if ((x.NamespaceURI != null) && (x.NamespaceURI.Length > 0)) {
						string prefix = x.GetPrefixOfNamespace(x.NamespaceURI);
						if (prefix.Length == 0) {
							name = x.Name;
						} else {
							name = prefix + ":" + x.Name;
						}
					} else {
						name = x.Name;
					}
					HElement newNode = new HElement(name);
					__CopyAttributes(x, newNode, ref countAttributes);
					__CopyNodes(x.ChildNodes, newNode.Children, bResolveEntities,
						ref countElements, ref countTexts, ref countAttributes);
					target.Add(newNode);
				}
			}
		}

		////////////////////////////////////////////////////////////////

		public static HElement Convert(XmlDocument doc, bool bResolveEntities)
		{
			int countElements = 1;
			int countTexts = 0;
			int countAttributes = 0;

			HElement rootNode = new HElement(doc.DocumentElement.Name);
			__CopyAttributes(doc.DocumentElement, rootNode, ref countAttributes);

			__CopyNodes(doc.DocumentElement.ChildNodes, rootNode.Children, bResolveEntities,
				ref countElements, ref countTexts, ref countAttributes);

			return rootNode;
		}

		public static HElement LoadXmlFromFile(string filePath, bool bResolveEntities)
		{
			using (XmlTextReader xmlReader = new XmlTextReader(filePath)) {
				xmlReader.EntityHandling = EntityHandling.ExpandCharEntities;

				XmlDocument doc = new XmlDocument();
				doc.Load(xmlReader);
				return Convert(doc, bResolveEntities);
			}
		}

	}

}
