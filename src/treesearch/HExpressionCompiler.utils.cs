using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;
using LibNLPCSharp.simpletokenizing;

using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public partial class HExpressionCompiler
	{

		private struct SEQ
		{
			public readonly string Name;
			public readonly TokenPattern[] Sequence;
			public readonly int[] PatternContent;

			public SEQ(string name, params TokenPattern[] sequence)
			{
				this.Name = name;
				this.Sequence = sequence;

				List<int> ret = new List<int>();
				for (int i = 0; i < sequence.Length; i++) {
					if (sequence[i].IsContent)
						ret.Add(i);
				}
				PatternContent = ret.ToArray();
			}

		}

		private struct SEQResult
		{
			public static readonly SEQResult None = new SEQResult();

			public readonly bool IsMatch;
			public readonly string Name;
			public readonly Token[] Tokens;
			public readonly Token[] TokensContent;

			public SEQResult(string name, Token[] tokens, int[] indicesContent)
			{
				this.IsMatch = true;

				this.Name = name;
				this.Tokens = tokens;

				List<Token> ret = new List<Token>();
				foreach (int i in indicesContent) {
					ret.Add(tokens[i]);
				}
				TokensContent = ret.ToArray();
			}

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

		private static SEQResult TryEatAlternatives(TokenStream ts, params SEQ[] sequences)
		{
			foreach (SEQ sequence in sequences) {
				Token[] tokens = ts.TryEatSequence(sequence.Sequence);
				if (tokens != null) {
					return new SEQResult(sequence.Name, tokens, sequence.PatternContent);
				}
			}
			return SEQResult.None;
		}

	}

}
