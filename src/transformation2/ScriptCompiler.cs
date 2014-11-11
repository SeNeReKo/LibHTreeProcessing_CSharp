using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.simpletokenizing;
using LibNLPCSharp.util;

using LibLightweightGUI.src.textmodel;

using LibHTreeProcessing.src.treesearch;
using LibHTreeProcessing.src.transformation2.operations;
using LibHTreeProcessing.src.transformation2.selectors;
using LibHTreeProcessing.src.transformation2.filters;
using LibHTreeProcessing.src.transformation2.impl;


namespace LibHTreeProcessing.src.transformation2
{

	public class ScriptCompiler
	{

		private class MyParsingContext
		{
			public readonly IParsingContext ParsingContext;

			public readonly Dictionary<string, SimpleProcedure> Procedures;

			public MyParsingContext(IParsingContext ctx)
			{
				this.ParsingContext = ctx;
				this.Procedures = new Dictionary<string, SimpleProcedure>();
			}
		}

		private enum EnumRedirectionType
		{
			Optional,
			Mandatory
		}

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		private static readonly TokenPattern PATTERN_EOL = TokenPattern.MatchDelimiter((char)13);
		private static readonly TokenPattern PATTERN_CBOPEN = TokenPattern.MatchDelimiter('{');
		private static readonly TokenPattern PATTERN_CBCLOSE = TokenPattern.MatchDelimiter('}');

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		ExtraCommandsParser extraCmdsParser;
		SelectorParser selectorsParser;
		OperationsParser operationsParser;
		FilterParser filtersParser;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public ScriptCompiler(ExtraCommandsParser extraCmdsParser, SelectorParser selectorsParser, FilterParser filtersParser,
			OperationsParser operationsParser)
		{
			this.extraCmdsParser = extraCmdsParser;
			this.selectorsParser = selectorsParser;
			this.filtersParser = filtersParser;
			this.operationsParser = operationsParser;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public ExtraCommandsParser ExtraCmdsParser
		{
			get {
				return extraCmdsParser;
			}
		}

		public SelectorParser SelectorsParser
		{
			get {
				return selectorsParser;
			}
		}

		public OperationsParser OperationsParser
		{
			get {
				return operationsParser;
			}
		}

		public FilterParser FiltersParser
		{
			get {
				return filtersParser;
			}
		}

		public string ShortHelpText
		{
			get {
				StringBuilder sb = new StringBuilder();

				sb.Append("Special Commands:");
				sb.Append(Util.CRLF);
				ParsingUtils.BuildShortHelpText(extraCmdsParser.ParserComponents, "\t", sb);

				sb.Append(Util.CRLF);
				sb.Append(Util.CRLF);

				sb.Append("Selectors:");
				sb.Append(Util.CRLF);
				ParsingUtils.BuildShortHelpText(selectorsParser.ParserComponents, "\t", sb);

				sb.Append(Util.CRLF);
				sb.Append(Util.CRLF);

				sb.Append("Filters:");
				sb.Append(Util.CRLF);
				ParsingUtils.BuildShortHelpText(filtersParser.ParserComponents, "\t", sb);

				sb.Append(Util.CRLF);
				sb.Append(Util.CRLF);

				sb.Append("Operations:");
				sb.Append(Util.CRLF);
				ParsingUtils.BuildShortHelpText(operationsParser.ParserComponents, "\t", sb);

				return sb.ToString();
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// Parse the specified script. An exception of type <code>ScriptException</code> is thrown on error.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="script">The script to compile</param>
		/// <returns>Returns an object of type <code>Script</code> on success. An exception is thrown on error.</returns>
		public IScript Compile(IParsingContext ctx, string script)
		{
			return Compile(ctx, new ScriptTokenProvider(script));
		}

		/// <summary>
		/// Parse the specified script. An exception of type <code>ScriptException</code> is thrown on error.
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="tokens">The tokens that make up the script to compile.</param>
		/// <returns>Returns an object of type <code>Script</code> on success. An exception is thrown on error.</returns>
		protected IScript Compile(IParsingContext ctx, IEnumerable<Token> tokens)
		{
			TokenStream ts = new TokenStream(tokens);

			MyParsingContext ctx2 = new MyParsingContext(ctx);

			List<AbstractScriptCommand> ret = TryEatCOMMANDS(ctx2, 0, ts);

			TryEatEOLS(ts);

			if (!ts.IsEOS)
				throw ScriptException.CreateError_ExcessiveTextEncountered(ts.LineNumber);

			Script script = new Script(
				ctx2.Procedures,
				(ret == null) ? new List<AbstractScriptCommand>() : ret
				);

			script.Link();

			return script;
		}

		////////////////////////////////////////////////////////////////

		private List<AbstractScriptCommand> TryEatCOMMANDBLOCK(MyParsingContext ctx, int nestingLevel, TokenStream ts)
		{
			if (ts.TryEat(PATTERN_CBOPEN) == null) return null;

			if (!TryEatEOLS(ts))
				throw ScriptException.CreateError_EOLExpected(ts.LineNumber);

			List<AbstractScriptCommand> ret = TryEatCOMMANDS(ctx, nestingLevel, ts);
			if (ret == null) {
				if (ts.TryEat(PATTERN_CBCLOSE) != null) {
					ret = new List<AbstractScriptCommand>();
				} else {
					throw ScriptException.CreateError_OneOrMoreCommandsExpected(ts.LineNumber);
				}
			} else {
				if (ts.TryEat(PATTERN_CBCLOSE) == null)
					throw ScriptException.CreateError_ClosingCurleyBracesExpected(ts.LineNumber);
			}

			return ret;
		}

		private List<AbstractScriptCommand> TryEatCOMMANDS(MyParsingContext ctx, int nestingLevel, TokenStream ts)
		{
			TryEatEOLS(ts);

			AbstractScriptCommand cmd = TryEatCMD(ctx, nestingLevel, ts);
			if (cmd == null) return null;

			List<AbstractScriptCommand> ret = new List<AbstractScriptCommand>();
			ret.Add(cmd);

			while (!ts.IsEOS) {
				TryEatEOLS(ts);

				cmd = TryEatCMD(ctx, nestingLevel, ts);
				if (cmd == null) break;
				ret.Add(cmd);
			}

			return ret;
		}

		private EnumRedirectionType? TryEatREDIR(MyParsingContext ctx, TokenStream ts)
		{
			Token[] tokensMatched;

			// ----

			tokensMatched = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('>'),
				TokenPattern.MatchDelimiter('>')
				);
			if (tokensMatched == null) return null;

			tokensMatched = ts.TryEatSequence(
				TokenPattern.MatchDelimiter('>'),
				TokenPattern.MatchDelimiter('>')
				);
			if (tokensMatched == null) {
				return EnumRedirectionType.Optional;
			} else {
				return EnumRedirectionType.Mandatory;
			}
		}

		private bool TryEatCMDFILTEROP(MyParsingContext ctx, TokenStream ts, out AbstractFilter filter, out AbstractOperation op)
		{
			TokenStream.IMarker m = ts.Mark();

			filter = filtersParser.TryParse(ctx.ParsingContext, ts);
			if (filter != null) {
				EnumRedirectionType? redir = TryEatREDIR(ctx, ts);
				if (!redir.HasValue) {
					m.Reset();
					filter = null;
				} else {
					filter.FailIfNoData = redir.Value == EnumRedirectionType.Mandatory;
				}
			}

			op = operationsParser.TryParse(ctx.ParsingContext, ts);
			if (op == null) {
				m.Reset();
				filter = null;
				return false;
			} else {
				return true;
			}
		}

		private SimpleProcedure TryEatPROCEDUREDEF(MyParsingContext ctx, int nestingLevel, TokenStream ts)
		{
			Token[] tokensMatched = ts.TryEatSequence(
				TokenPattern.MatchWord("procedure"),
				TokenPattern.MatchAnyWord(),
				TokenPattern.MatchDelimiter('('),
				TokenPattern.MatchAnyWord(),
				TokenPattern.MatchDelimiter(')')
				);

			if (tokensMatched == null) return null;

			if (nestingLevel > 0) {
				throw ScriptException.CreateError_Unknown(
					ts.LineNumber,
					"Sorry, you can't define a procedure here!"
					);
			}

			string procName = tokensMatched[1].Text;

			if (tokensMatched[3].Text.Equals("node")) {
				
				TryEatEOLS(ts);

				List<AbstractScriptCommand> cmds = TryEatCOMMANDBLOCK(ctx, nestingLevel + 1, ts);
				if (cmds == null)
					throw ScriptException.CreateError_CommandBlockExpected(ts.LineNumber);

				return new SimpleProcedure(tokensMatched[0].LineNumber, procName, EnumDataType.SingleNode, cmds);

			} else {

				throw ScriptException.CreateError_Unknown(
					ts.LineNumber,
					"Data type \"node\" as procedure argument expected!"
					);

			}
		}

		private AbstractScriptCommand TryEatCMD(MyParsingContext ctx, int nestingLevel, TokenStream ts)
		{
			Token[] tokensMatched;

			// ----

			SimpleProcedure simpleProc = TryEatPROCEDUREDEF(ctx, nestingLevel, ts);
			if (simpleProc != null) {
				if (!TryEatEOLS(ts))
					throw ScriptException.CreateError_EOLExpected(ts.LineNumber);

				SimpleProcedure simpleProcExisting;
				if (ctx.Procedures.TryGetValue(simpleProc.Name, out simpleProcExisting)) {
					throw ScriptException.CreateError_ProcedureAlreadyDefined(
						simpleProc.LineNumber,
						simpleProc.Name,
						simpleProcExisting.LineNumber);
				}
				ctx.Procedures.Add(simpleProc.Name, simpleProc);

				return new cmds.Noop_TransformationRule(-1);
			}

			// ----

			AbstractScriptCommand cmd = extraCmdsParser.TryParse(ctx.ParsingContext, ts);
			if (cmd != null) {
				if (!TryEatEOLS(ts))
					throw ScriptException.CreateError_EOLExpected(ts.LineNumber);
				return cmd;
			}

			// ----

			AbstractSelector selector = selectorsParser.TryParse(ctx.ParsingContext, ts);
			if (selector != null) {
				EnumRedirectionType? redir = TryEatREDIR(ctx, ts);
				if (!redir.HasValue) {
					throw ScriptException.CreateError_PipeExpected(ts.LineNumber);
				}

				selector.FailIfNothingSelected = redir.Value == EnumRedirectionType.Mandatory;

				AbstractFilter filter;
				AbstractOperation operation;
				if (!TryEatCMDFILTEROP(ctx, ts, out filter, out operation)) {
					throw ScriptException.CreateError_ValidFilterOrOperationExpected(ts.LineNumber);
				}

				return new SelectorBasedScriptCommand(selector, filter, operation);
			}

			// ----

			if ((tokensMatched = ts.TryEatSequence(
				TokenPattern.MatchWord("select"),
				TokenPattern.MatchWord("subtrees"),
				TokenPattern.MatchAnyStringDQ()
				)) != null) {

				HExpression he;
				try {
					he = HExpressionCompiler.Compile(tokensMatched[2].Text, false);
				} catch (Exception ee) {
					throw ScriptException.CreateError_InvalidPatternExpressionSpecified(ts.LineNumber, tokensMatched[2].Text);
				}

				List<AbstractScriptCommand> commands = TryEatCOMMANDBLOCK(ctx, nestingLevel + 1, ts);
				if (commands == null)
					throw ScriptException.CreateError_CommandBlockExpected(ts.LineNumber);
				if (!TryEatEOLS(ts))
					throw ScriptException.CreateError_EOLExpected(ts.LineNumber);

				return new SubtreeCommandMultiple(
					tokensMatched[0].LineNumber,
					he,
					new CommandSequenceCommand((commands.Count == 0) ? tokensMatched[0].LineNumber : commands[0].LineNumber, commands)
					);
			}

			// ----

			if ((tokensMatched = ts.TryEatSequence(
				TokenPattern.MatchWord("select"),
				TokenPattern.MatchWord("single"),
				TokenPattern.MatchWord("subtree"),
				TokenPattern.MatchAnyStringDQ()
				)) != null) {

				HExpression he;
				try {
					he = HExpressionCompiler.Compile(tokensMatched[3].Text, false);
				} catch (Exception ee) {
					throw ScriptException.CreateError_InvalidPatternExpressionSpecified(ts.LineNumber, tokensMatched[3].Text);
				}

				List<AbstractScriptCommand> commands = TryEatCOMMANDBLOCK(ctx, nestingLevel + 1, ts);
				if (commands == null)
					throw ScriptException.CreateError_CommandBlockExpected(ts.LineNumber);
				if (!TryEatEOLS(ts))
					throw ScriptException.CreateError_EOLExpected(ts.LineNumber);

				return new SubtreeCommandSingle(
					tokensMatched[0].LineNumber,
					he,
					new CommandSequenceCommand((commands.Count == 0) ? tokensMatched[0].LineNumber : commands[0].LineNumber, commands)
					);
			}

			// ----

			return null;
		}

		private bool TryEatEOLS(TokenStream ts)
		{
			Token t = ts.TryEat(PATTERN_EOL);
			if (t == null) return false;
			while ((t = ts.TryEat(PATTERN_EOL)) != null);
			return true;
		}

	}

}
