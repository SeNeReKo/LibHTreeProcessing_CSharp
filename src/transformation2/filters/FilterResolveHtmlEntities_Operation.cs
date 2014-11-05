using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;
using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2.filters
{

	public class FilterResolveHtmlEntities_Operation : AbstractFilter
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		// see: http://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references
		private static readonly Dictionary<string, string> ENTITYREF_MAP = new Dictionary<string, string>() {
			{ "quot", "\"" },
			{ "amp", "&" },
			{ "apos", "'" },
			{ "lt", "<" },
			{ "gt", ">" },
			{ "nbsp", " " },
			{ "iexcl", "¡" },
			{ "cent", "¢" },
			{ "pound", "£" },
			{ "curren", "\u00a4" },
			{ "yen", "¥" },
			{ "brvbar", "¦" },
			{ "sect", "§" },
			{ "uml", "¨" },		// ????
			{ "copy", "©" },
			{ "ordf", "ª" },
			{ "laquo", "«" },
			{ "not", "¬" },
			{ "shy", "" },		// ????
			{ "reg", "®" },
			{ "macr", "¯" },		// ????
			{ "deg", "°" },
			{ "plusmn", "±" },
			{ "sup2", "²" },
			{ "sup3", "³" },
			{ "acute", "´" },		// ????
			{ "micro", "µ" },
			{ "para", "¶" },
			{ "middot", "·" },
			{ "cedil", "¸" },		// ????
			{ "sup1", "¹" },
			{ "ordm", "º" },
			{ "raquo", "»" },
			{ "frac14", "¼" },
			{ "frac12", "½" },
			{ "frac34", "¾" },
			{ "iquest", "¿" },
			{ "Agrave", "À" },
			{ "Aacute", "Á" },
			{ "Acirc", "Â" },
			{ "Atilde", "Ã" },
			{ "Auml", "Ä" },
			{ "Aring", "Å" },
			{ "AElig", "Æ" },
			{ "Ccedil", "Ç" },
			{ "Egrave", "È" },
			{ "Eacute", "É" },
			{ "Ecirc", "Ê" },
			{ "Euml", "Ë" },
			{ "Igrave", "Ì" },
			{ "Iacute", "Í" },
			{ "Icirc", "Î" },
			{ "Iuml", "Ï" },
			{ "ETH", "Ð" },
			{ "Ntilde", "Ñ" },
			{ "Ograve", "Ò" },
			{ "Oacute", "Ó" },
			{ "Ocirc", "Ô" },
			{ "Otilde", "Õ" },
			{ "Ouml", "Ö" },
			{ "times", "×" },
			{ "Oslash", "Ø" },
			{ "Ugrave", "Ù" },
			{ "Uacute", "Ú" },
			{ "Ucirc", "Û" },
			{ "Uuml", "Ü" },
			{ "Yacute", "Ý" },
			{ "THORN", "Þ" },
			{ "szlig", "ß" },
			{ "agrave", "à" },
			{ "aacute", "á" },
			{ "acirc", "â" },
			{ "atilde", "ã" },
			{ "auml", "ä" },
			{ "aring", "å" },
			{ "aelig", "æ" },
			{ "ccedil", "ç" },
			{ "egrave", "è" },
			{ "eacute", "é" },
			{ "ecirc", "ê" },
			{ "euml", "ë" },
			{ "igrave", "ì" },
			{ "iacute", "í" },
			{ "icirc", "î" },
			{ "iuml", "ï" },
			{ "eth", "ð" },
			{ "ntilde", "ñ" },
			{ "ograve", "ò" },
			{ "oacute", "ó" },
			{ "ocirc", "ô" },
			{ "otilde", "õ" },
			{ "ouml", "ö" },
			{ "divide", "÷" },
			{ "oslash", "ø" },
			{ "ugrave", "ù" },
			{ "uacute", "ú" },
			{ "ucirc", "û" },
			{ "uuml", "ü" },
			{ "yacute", "ý" },
			{ "thorn", "þ" },
			{ "yuml", "ÿ" },
			{ "OElig", "Œ" },
			{ "oelig", "œ" },
			{ "Scaron", "Š" },
			{ "scaron", "š" },
			{ "Yuml", "Ÿ" },
			{ "fnof", "ƒ" },
			{ "circ", "ˆ" },
			{ "tilde", "˜˜˜" },	// ????
			{ "Alpha", "Α" },
			{ "Beta", "Β" },
			{ "Gamma", "Γ" },
			{ "Delta", "Δ" },
			{ "Epsilon", "Ε" },
			{ "Zeta", "Ζ" },
			{ "Eta", "Η" },
			{ "Theta", "Θ" },
			{ "Iota", "Ι" },
			{ "Kappa", "Κ" },
			{ "Lambda", "Λ" },
			{ "Mu", "Μ" },
			{ "Nu", "Ν" },
			{ "Xi", "Ξ" },
			{ "Omicron", "Ο" },
			{ "Pi", "Π" },
			{ "Rho", "Ρ" },
			{ "Sigma", "Σ" },
			{ "Tau", "Τ" },
			{ "Upsilon", "Υ" },
			{ "Phi", "Φ" },
			{ "Chi", "Χ" },
			{ "Psi", "Ψ" },
			{ "Omega", "Ω" },
			{ "alpha", "α" },
			{ "beta", "β" },
			{ "gamma", "γ" },
			{ "delta", "δ" },
			{ "epsilon", "ε" },
			{ "zeta", "ζ" },
			{ "eta", "η" },
			{ "theta", "θ" },
			{ "iota", "ι" },
			{ "kappa", "κ" },
			{ "lambda", "λ" },
			{ "mu", "μ" },
			{ "nu", "ν" },
			{ "xi", "ξ" },
			{ "omicron", "ο" },
			{ "pi", "π" },
			{ "rho", "ρ" },
			{ "sigmaf", "ς" },
			{ "sigma", "σ" },
			{ "tau", "τ" },
			{ "upsilon", "υ" },
			{ "phi", "φ" },
			{ "chi", "χ" },
			{ "psi", "ψ" },
			{ "omega", "ω" },
			{ "thetasym", "ϑ" },
			{ "upsih", "ϒ" },
			{ "piv", "ϖ" },
			{ "ndash", "–" },
			{ "mdash", "—" },
			{ "lsquo", "‘" },
			{ "rsquo", "’" },
			{ "sbquo", "‚" },
			{ "ldquo", "“" },
			{ "rdquo", "”" },
			{ "bdquo", "„" },
			{ "dagger", "†" },
			{ "Dagger", "‡" },
			{ "bull", "•" },
			{ "hellip", "…" },
			{ "permil", "‰" },
			{ "prime", "′" },
			{ "Prime", "″" },
			{ "lsaquo", "‹" },
			{ "rsaquo", "›" },
			{ "oline", "‾" },
			{ "frasl", "⁄" },
			{ "euro", "€" },
			{ "image", "ℑ" },
			{ "weierp", "℘" },
			{ "real", "ℜ" },
			{ "trade", "™" },
			{ "alefsym", "ℵ" },
			{ "larr", "←" },
			{ "uarr", "↑" },
			{ "rarr", "→" },
			{ "darr", "↓" },
			{ "harr", "↔" },
			{ "crarr", "↵" },
			{ "lArr", "⇐" },
			{ "uArr", "⇑" },
			{ "rArr", "⇒" },
			{ "dArr", "⇓" },
			{ "hArr", "⇔" },
			{ "forall", "∀" },
			{ "part", "∂" },
			{ "exist", "∃" },
			{ "empty", "∅" },
			{ "nabla", "∇" },
			{ "isin", "∈" },
			{ "notin", "∉" },
			{ "ni", "∋" },
			{ "prod", "∏" },
			{ "sum", "∑" },
			{ "minus", "−" },
			{ "lowast", "∗" },
			{ "radic", "√" },
			{ "prop", "∝" },
			{ "infin", "∞" },
			{ "ang", "∠" },
			{ "and", "∧" },
			{ "or", "∨" },
			{ "cap", "∩" },
			{ "cup", "∪" },
			{ "int", "∫" },
			{ "there4", "∴" },
			{ "sim", "∼" },
			{ "cong", "≅" },
			{ "asymp", "≈" },
			{ "ne", "≠" },
			{ "equiv", "≡" },
			{ "le", "≤" },
			{ "ge", "≥" },
			{ "sub", "⊂" },
			{ "sup", "⊃" },
			{ "nsub", "⊄" },
			{ "sube", "⊆" },
			{ "supe", "⊇" },
			{ "oplus", "⊕" },
			{ "otimes", "⊗" },
			{ "perp", "⊥" },
			{ "sdot", "⋅" },
			{ "vellip", "⋮" },
			{ "lceil", "\u2308" },
			{ "rceil", "\u2309" },
			{ "lfloor", "\u230A" },
			{ "rfloor", "\u230B" },
			{ "lang", "\u2329" },
			{ "rang", "\u232A" },
			{ "loz", "◊" },
			{ "spades", "♠" },
			{ "clubs", "♣" },
			{ "hearts", "♥" },
			{ "diams", "♦" },
		};

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="lineNo">Line number of first token from parsing this selector.</param>
		public FilterResolveHtmlEntities_Operation(int lineNo)
			: base(lineNo)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public override HAbstractElement Process(TransformationRuleContext ctx, ScriptRuntimeContext dataCtx, HPathWithIndices currentPath, HAbstractElement currentElement)
		{
			if (currentElement is HText) {
				HText t = (HText)currentElement;
				return new HText(__ResolveEntities(t.Text));
			} else
			if (currentElement is HAttribute) {
				HAttribute a = (HAttribute)currentElement;
				return new HAttribute(a.Name, __ResolveEntities(a.Value));
			} else {
				throw new ImplementationErrorException();
			}
		}

		private static string __ResolveEntities(string text)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilder sbEntity = new StringBuilder();
			bool bInEntity = false;

			foreach (char c in text) {
				if (bInEntity) {
					if (c == ';') {
						// process entity
						string entityName = sbEntity.ToString();
						sbEntity.Remove(0, sbEntity.Length);

						string replacement;
						if (ENTITYREF_MAP.TryGetValue(entityName, out replacement)) {
							// found in map
							sb.Append(replacement);
						} else {
							// not found in map
							sb.Append('&');
							sb.Append(entityName);
							sb.Append(';');
						}

						bInEntity = false;
					} else {
						sbEntity.Append(c);
					}
				} else {
					if (c == '&') {
						bInEntity = true;
					} else {
						sb.Append(c);
					}
				}
			}

			if (bInEntity) {
				// deal with this error
				sb.Append('&');
				sb.Append(sbEntity.ToString());
			}

			return sb.ToString();
		}
		
	}

}
