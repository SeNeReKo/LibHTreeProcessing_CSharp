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

		public class XMLLoadSettings
		{
			public EnumResolveXmlEntitiesMode ResolveXmlEntitiesAtNodes = EnumResolveXmlEntitiesMode.ResolvePredefinedXmlEntitiesPreserveAllOthersAsText;
			public EnumResolveXmlEntitiesMode ResolveXmlEntitiesAtAttributes = EnumResolveXmlEntitiesMode.ResolvePredefinedXmlEntitiesPreserveAllOthersAsText;
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

		private static void __CopyAttributes(XmlElement xml, HElement node, EnumResolveXmlEntitiesMode mode, ref int countAttributes)
		{
			foreach (XmlAttribute a in xml.Attributes) {
				countAttributes++;

				StringBuilder sb = new StringBuilder();
				foreach (XmlNode n in a.ChildNodes) {
					____Decode(n, mode, sb);
				}

				node.Attributes.Add(new HAttribute(a.Name, sb.ToString()));
			}
		}

		/// <summary>
		/// Performs encoding of '<', '>', '&', '\"' to "&lt;", "&gt;", "&amp;" and "&quot;"
		/// </summary>
		/// <param name="attrValue"></param>
		/// <returns></returns>
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

		private static void ____Decode(XmlNode n, EnumResolveXmlEntitiesMode mode, ICollection<HAbstractElement> target)
		{
			if (n is XmlText) {

				string rawText = ((XmlText)n).InnerText;

				switch (mode) {
					case EnumResolveXmlEntitiesMode.ResolvePredefinedXmlEntitiesPreserveAllOthersAsText:
						target.Add(new HText(rawText));
						break;

					case EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText:
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
								case '\"':
									if (sb.Length > 0) {
										target.Add(new HText(sb.ToString()));
										sb.Remove(0, sb.Length);
									}
									target.Add(new HText("&quot;"));
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
						break;

					case EnumResolveXmlEntitiesMode.ResolveAllEntities:
						target.Add(new HText(rawText));
						break;

					default:
						throw new ImplementationErrorException();
				}

			} else
			if (n is XmlEntityReference) {

				XmlEntityReference r = (XmlEntityReference)n;

				string text;
				switch (mode) {
					case EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText:
						text = "&" + r.Name + ";";
						break;
					case EnumResolveXmlEntitiesMode.ResolveAllEntities:
						text = r.InnerText;
						break;
					case EnumResolveXmlEntitiesMode.ResolvePredefinedXmlEntitiesPreserveAllOthersAsText:
						if (r.Name.Equals("lt")) {
							text = r.InnerText;
						} else
						if (r.Name.Equals("gt")) {
							text = r.InnerText;
						} else
						if (r.Name.Equals("amp")) {
							text = r.InnerText;
						} else
						if (r.Name.Equals("quot")) {
							text = r.InnerText;
						} else
							text = "&" + r.Name + ";";
						break;
					default:
						throw new ImplementationErrorException();
				}

				target.Add(new HText(text));
			
			} else {
				throw new ImplementationErrorException("Invalid method call!");
			}
		}

		private static void ____Decode(XmlNode n, EnumResolveXmlEntitiesMode mode, StringBuilder target)
		{
			if (n is XmlText) {

				string rawText = ((XmlText)n).InnerText;

				switch (mode) {
					case EnumResolveXmlEntitiesMode.ResolvePredefinedXmlEntitiesPreserveAllOthersAsText:
						target.Append(rawText);
						break;

					case EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText:
						StringBuilder sb = new StringBuilder();
						foreach (char c in rawText) {
							switch (c) {
								case '&':
									if (sb.Length > 0) {
										target.Append(sb.ToString());
										sb.Remove(0, sb.Length);
									}
									target.Append("&amp;");
									break;
								case '>':
									if (sb.Length > 0) {
										target.Append(sb.ToString());
										sb.Remove(0, sb.Length);
									}
									target.Append("&gt;");
									break;
								case '<':
									if (sb.Length > 0) {
										target.Append(sb.ToString());
										sb.Remove(0, sb.Length);
									}
									target.Append("&lt;");
									break;
								case '\"':
									if (sb.Length > 0) {
										target.Append(sb.ToString());
										sb.Remove(0, sb.Length);
									}
									target.Append("&quot;");
									break;
								default:
									sb.Append(c);
									break;
							}
						}
						if (sb.Length > 0) {
							rawText = sb.ToString();
							target.Append(rawText);
						}
						break;

					case EnumResolveXmlEntitiesMode.ResolveAllEntities:
						target.Append(rawText);
						break;

					default:
						throw new ImplementationErrorException();
				}

			} else
			if (n is XmlEntityReference) {

				XmlEntityReference r = (XmlEntityReference)n;

				string text;
				switch (mode) {
					case EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText:
						text = "&" + r.Name + ";";
						break;
					case EnumResolveXmlEntitiesMode.ResolveAllEntities:
						text = r.InnerText;
						break;
					case EnumResolveXmlEntitiesMode.ResolvePredefinedXmlEntitiesPreserveAllOthersAsText:
						if (r.Name.Equals("lt")) {
							text = r.InnerText;
						} else
						if (r.Name.Equals("gt")) {
							text = r.InnerText;
						} else
						if (r.Name.Equals("amp")) {
							text = r.InnerText;
						} else
						if (r.Name.Equals("quot")) {
							text = r.InnerText;
						} else
							text = "&" + r.Name + ";";
						break;
					default:
						throw new ImplementationErrorException();
				}

				target.Append(text);
			} else {
				throw new ImplementationErrorException("Invalid method call!");
			}
		}

		private static void __CopyNodes(
			XmlNodeList nodes,
			List<HAbstractElement> target,
			XMLLoadSettings settings,
			ref int countElements,
			ref int countTexts,
			ref int countAttributes)
		{
			foreach (XmlNode n in nodes) {
				if (n is XmlText) {
					countTexts++;

					____Decode(n, settings.ResolveXmlEntitiesAtNodes, target);

				} else
				if (n is XmlEntityReference) {
					countTexts++;

					____Decode(n, settings.ResolveXmlEntitiesAtNodes, target);

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
					__CopyAttributes(x, newNode, settings.ResolveXmlEntitiesAtAttributes, ref countAttributes);
					__CopyNodes(x.ChildNodes, newNode.Children, settings,
						ref countElements, ref countTexts, ref countAttributes);
					target.Add(newNode);
				}
			}
		}

		////////////////////////////////////////////////////////////////

		public static HElement Convert(XmlDocument doc, bool bResolveEntities)
		{
			XMLLoadSettings settings = new XMLLoadSettings();
			settings.ResolveXmlEntitiesAtNodes = bResolveEntities ? EnumResolveXmlEntitiesMode.ResolveAllEntities : EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText;
			settings.ResolveXmlEntitiesAtAttributes = bResolveEntities ? EnumResolveXmlEntitiesMode.ResolveAllEntities : EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText;
			return Convert(doc, settings);
		}

		public static HElement Convert(XmlDocument doc, XMLLoadSettings settings)
		{
			int countElements = 1;
			int countTexts = 0;
			int countAttributes = 0;

			HElement rootNode = new HElement(doc.DocumentElement.Name);
			__CopyAttributes(doc.DocumentElement, rootNode, settings.ResolveXmlEntitiesAtAttributes, ref countAttributes);

			__CopyNodes(doc.DocumentElement.ChildNodes, rootNode.Children, settings,
				ref countElements, ref countTexts, ref countAttributes);

			return rootNode;
		}

		public static HElement LoadXmlFromFile(string filePath, bool bResolveEntities)
		{
			XMLLoadSettings settings = new XMLLoadSettings();
			settings.ResolveXmlEntitiesAtAttributes = bResolveEntities ? EnumResolveXmlEntitiesMode.ResolveAllEntities : EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText;
			settings.ResolveXmlEntitiesAtNodes = bResolveEntities ? EnumResolveXmlEntitiesMode.ResolveAllEntities : EnumResolveXmlEntitiesMode.PreserveAllEntitiesAsText;
			return LoadXmlFromFile(filePath, settings);
		}

		public static HElement LoadXmlFromFile(string filePath, XMLLoadSettings settings)
		{
			using (XmlTextReader xmlReader = new XmlTextReader(filePath)) {
				xmlReader.EntityHandling = EntityHandling.ExpandCharEntities;

				XmlDocument doc = new XmlDocument();
				doc.Load(xmlReader);
				return Convert(doc, settings);
			}
		}

		public static HElement LoadXmlFromFile(TextReader textReader, XMLLoadSettings settings)
		{
			using (XmlTextReader xmlReader = new XmlTextReader(textReader)) {
				xmlReader.EntityHandling = EntityHandling.ExpandCharEntities;

				XmlDocument doc = new XmlDocument();
				doc.Load(xmlReader);
				return Convert(doc, settings);
			}
		}

	}

}
