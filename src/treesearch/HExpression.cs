using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.util;

using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	public class HExpression
	{

		public delegate void OnMatchCallback(MatchResult match);

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private string originalExpressionText;
		private List<ChainElement> chain;
		private int indexOfLastChainElement;
		private List<MatchingRecord> tempResults;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public HExpression()
		{
			this.chain = new List<ChainElement>();
			this.tempResults = new List<MatchingRecord>();
		}

		public HExpression(string originalExpressionText)
		{
			this.originalExpressionText = originalExpressionText;
			this.chain = new List<ChainElement>();
			this.tempResults = new List<MatchingRecord>();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void CollectEmitIDs(out Dictionary<string, EnumElementType> collection, out EnumElementType lastElementType)
		{
			collection = new Dictionary<string, EnumElementType>();
			lastElementType = EnumElementType.Node;
			for (int i = 0; i < chain.Count; i++) {
				chain[i].Matcher.CollectEmitIDs(collection, out lastElementType);
			}
		}

		public EnumElementType DetermineSelectedElementType()
		{
			if (chain.Count == 0) return EnumElementType.Node; // fallback

			Dictionary<string, EnumElementType> collection = new Dictionary<string, EnumElementType>();
			EnumElementType lastElement = EnumElementType.Node;
			for (int i = chain.Count - 1; i >= 0; i--) {
				chain[i].Matcher.CollectEmitIDs(collection, out lastElement);
				if (collection.Count > 0) {
					foreach (KeyValuePair<string, EnumElementType> kvp in collection) {
						return kvp.Value;
					}
				}
			}
			chain[chain.Count - 1].Matcher.CollectEmitIDs(collection, out lastElement);
			return lastElement;
		}

		public EnumElementType DetermineLastElementType()
		{
			if (chain.Count == 0) return EnumElementType.Node; // fallback

			Dictionary<string, EnumElementType> collection = new Dictionary<string, EnumElementType>();
			EnumElementType lastElement = EnumElementType.Node;
			for (int i = 0; i < chain.Count; i++) {
				chain[i].Matcher.CollectEmitIDs(collection, out lastElement);
			}
			return lastElement;
		}

		public void Add(AbstractTreeNodeVisitor visitor, AbstractTreeNodeMatcher matcher)
		{
			if (matcher == null) throw new Exception("matcher == null!");

			if (visitor == null) {
				if (chain.Count == 0) {
					chain.Add(new ChainElement(new DirectNodeVisitor(), matcher));
				} else {
					throw new Exception("visitor == null!");
				}
			} else {
				chain.Add(new ChainElement(visitor, matcher));
			}

			indexOfLastChainElement = chain.Count - 1;
		}

		////////////////////////////////////////////////////////////////

		public MatchResult MatchOne(HAbstractElement element)
		{
			foreach (MatchResult m in Search(element)) {
				return m;
			}
			return null;
		}

		public MatchResultGroup MatchAll(HAbstractElement element)
		{
			return new MatchResultGroup(Search(element));
		}

		public void MatchAll(HAbstractElement element, OnMatchCallback callback)
		{
			foreach (MatchResult mr in Search(element)) {
				callback(mr);
			}
		}

		////////////////////////////////////////////////////////////////

		public IEnumerable<MatchResult> Search(HAbstractElement element)
		{
			if (chain.Count == 0) yield break;

			HPathWithIndices[] pathSegments = new HPathWithIndices[chain.Count];
			HPathWithIndices path = new HPathWithIndices(new PathStruct(element, 0));
			chain[0].Visitor.Reset(path);

			int i = 0;
			while (true) {
				ChainElement ce = chain[i];
				
				// try to proceed to next element
				if (!ce.Visitor.Next()) {
					// not successful -> go one level up the matcher chain
					if (i == 0) {
						// no more matches
						yield break;
					}
					i--;
					continue;
				}

				PathStruct e = path.Peek();

				if (!ce.Matcher.Match(e.Element)) continue;

				pathSegments[i] = HPathWithIndices.Clone(path);

				if (i == indexOfLastChainElement) {
					// end of chain reached! we have a match!

					tempResults.Clear();
					for (int j = 0; j < chain.Count; j++) {
						chain[j].Matcher.Emit(pathSegments[j], tempResults);
					}

					// return data
					yield return new MatchResult(pathSegments[pathSegments.Length - 1], tempResults.ToArray());

					continue;
				}

				i++;
				// initialize iterator and let it prepare for the next area of the tree
				chain[i].Visitor.Reset(path);
			}
		}

		public void Search(HAbstractElement element, OnMatchCallback callback)
		{
			if (chain.Count == 0) return;

			HPathWithIndices[] pathSegments = new HPathWithIndices[chain.Count];
			HPathWithIndices path = new HPathWithIndices(new PathStruct(element, 0));
			chain[0].Visitor.Reset(path);

			int i = 0;
			while (true) {
				ChainElement ce = chain[i];
				
				// try to proceed to next element
				if (!ce.Visitor.Next()) {
					// not successful -> go one level up the matcher chain
					if (i == 0) {
						// no more matches
						return;
					}
					i--;
					continue;
				}

				PathStruct e = path.Peek();

				if (!ce.Matcher.Match(e.Element)) continue;

				pathSegments[i] = HPathWithIndices.Clone(path);

				if (i == indexOfLastChainElement) {
					// end of chain reached! we have a match!

					tempResults.Clear();
					for (int j = 0; j < chain.Count; j++) {
						chain[j].Matcher.Emit(pathSegments[j], tempResults);
					}

					// return data
					callback(new MatchResult(pathSegments[pathSegments.Length - 1], tempResults.ToArray()));

					continue;
				}

				i++;
				// initialize iterator and let it prepare for the next area of the tree
				chain[i].Visitor.Reset(path);
			}
		}

		////////////////////////////////////////////////////////////////

		public static HAbstractElement[] CreateArray(HPath parentPath, HAbstractElement element)
		{
			HAbstractElement[] ret = new HAbstractElement[parentPath.Count + 1];
			parentPath.CopyTo(ret, 0);
			ret[parentPath.Count] = element;
			return ret;
		}

		public override string ToString()
		{
			if (originalExpressionText != null) return originalExpressionText;

			StringBuilder sb = new StringBuilder();

			foreach (ChainElement ce in chain) {
				ce.ToString(sb);
			}

			return sb.ToString();
		}

	}

}
