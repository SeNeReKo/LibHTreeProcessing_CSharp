using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibHTreeProcessing.src.simplexml;


namespace LibHTreeProcessing.src.treesearch
{

	/// <summary>
	/// This class represents a single result of a matching attempt.
	/// </summary>
	public class MatchResult : IEnumerable<MatchingRecord>
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		public readonly HPathWithIndices Path;
		public readonly MatchingRecord[] Emitted;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public MatchResult(HPathWithIndices path, IList<MatchingRecord> data)
		{
			Emitted = data.ToArray();
		}

		public MatchResult(HPathWithIndices path, params MatchingRecord[] data)
		{
			Path = path;
			Emitted = data;
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public string[] Keys
		{
			get {
				HashSet<string> keys = new HashSet<string>();
				foreach (MatchingRecord r in Emitted) {
					keys.Add(r.EmitID);
				}
				return keys.ToArray();
			}
		}

		public MatchingRecord this[int index]
		{
			get {
				if ((index < 0) || (index >= Emitted.Length)) return null;
				return Emitted[index];
			}
		}

		public MatchingRecord this[string id]
		{
			get {
				for (int i = 0; i < Emitted.Length; i++) {
					if (Emitted[i].EmitID.Equals(id)) return Emitted[i];
				}
				return null;
			}
		}

		public int Count
		{
			get {
				return Emitted.Length;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public IEnumerator<MatchingRecord> GetEnumerator()
		{
			for (int i = 0; i < Emitted.Length; i++) {
				yield return Emitted[i];
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			for (int i = 0; i < Emitted.Length; i++) {
				yield return Emitted[i];
			}
		}

		public MatchingRecord[] ToArray()
		{
			return Emitted;
		}

		public HAttribute GetSingleElementSelected(out HPathWithIndices path, out HAttribute attr)
		{
			HAbstractElement he;
			GetSingleElementSelected(out path, out he);

			if (!(he is HAttribute))
				throw new Exception("Can't process: Last element of path is not of type \"attribute\"! Path: " + path.ToString());

			attr = (HAttribute)he;

			return attr;
		}

		public HAttribute GetSingleElementSelected(out HAttribute attr)
		{
			HPathWithIndices path;
			HAbstractElement he;
			GetSingleElementSelected(out path, out he);

			if (!(he is HAttribute))
				throw new Exception("Can't process: Last element of path is not of type \"attribute\"! Path: " + path.ToString());

			attr = (HAttribute)he;

			return attr;
		}

		public HElement GetSingleElementSelected(out HPathWithIndices path, out HElement element)
		{
			HAbstractElement he;
			GetSingleElementSelected(out path, out he);

			if (!(he is HElement))
				throw new Exception("Can't process: Last element of path is not of type \"node\"! Path: " + path.ToString());

			element = (HElement)he;

			return element;
		}

		public HElement GetSingleElementSelected(out HElement element)
		{
			HPathWithIndices path;
			HAbstractElement he;
			GetSingleElementSelected(out path, out he);

			if (!(he is HElement))
				throw new Exception("Can't process: Last element of path is not of type \"node\"! Path: " + path.ToString());

			element = (HElement)he;

			return element;
		}

		public HText GetSingleElementSelected(out HPathWithIndices path, out HText element)
		{
			HAbstractElement he;
			GetSingleElementSelected(out path, out he);

			if (!(he is HText))
				throw new Exception("Can't process: Last element of path is not of type \"text\"! Path: " + path.ToString());

			element = (HText)he;

			return element;
		}

		public HText GetSingleElementSelected(out HText element)
		{
			HPathWithIndices path;
			HAbstractElement he;
			GetSingleElementSelected(out path, out he);

			if (!(he is HText))
				throw new Exception("Can't process: Last element of path is not of type \"text\"! Path: " + path.ToString());

			element = (HText)he;

			return element;
		}

		public HAbstractElement GetSingleElementSelected(out HPathWithIndices path, out HAbstractElement he)
		{
			if (Count == 0) {
				path = Path;
				he = Path[Path.Count - 1].Element;
			} else
				if (Count == 1) {
					path = Path;
					he = this[0].Element;
				} else
					throw new Exception("More than one items emitted!");

			return he;
		}

		public HAbstractElement GetSingleElementSelected(out HAbstractElement he)
		{
			if (Count == 0) {
				he = Path[Path.Count - 1].Element;
			} else
				if (Count == 1) {
					he = this[0].Element;
				} else
					throw new Exception("More than one items emitted!");

			return he;
		}

	}

}
