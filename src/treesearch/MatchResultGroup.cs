using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.treesearch
{

	public class MatchResultGroup : List<MatchResult>
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

		public MatchResultGroup()
		{
		}

		public MatchResultGroup(IEnumerable<MatchResult> results)
			: base(results)
		{
		}

		public MatchResultGroup(params MatchResult[] results)
			: base(results)
		{
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public MatchingRecord this[string emitID]
		{
			get {
				foreach (MatchResult mr in this) {
					foreach (MatchingRecord mr2 in mr.Emitted) {
						if (mr2.EmitID.Equals(emitID)) return mr2;
					}
				}
				return null;
			}
		}

		public string[] Keys
		{
			get {
				HashSet<string> keys = new HashSet<string>();
				foreach (MatchResult mr in this) {
					foreach (MatchingRecord mr2 in mr.Emitted) {
						keys.Add(mr2.EmitID);
					}
				}
				return keys.ToArray();
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public MatchingRecord Get(string emitID)
		{
			foreach (MatchResult mr in this) {
				foreach (MatchingRecord mr2 in mr.Emitted) {
					if (mr2.EmitID.Equals(emitID)) return mr2;
				}
			}
			return null;
		}

		public List<MatchingRecord> GetAll(string emitID)
		{
			List<MatchingRecord> ret = new List<MatchingRecord>();
			foreach (MatchResult mr in this) {
				foreach (MatchingRecord mr2 in mr.Emitted) {
					if (mr2.EmitID.Equals(emitID)) ret.Add(mr2);
				}
			}
			return ret;
		}

	}

}
