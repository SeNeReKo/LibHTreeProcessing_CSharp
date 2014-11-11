using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LibNLPCSharp.simpletokenizing;


namespace LibHTreeProcessing.src.transformation2.impl
{

	public abstract class AbstractComponentBasedParser<T, U> : IParserComponent<U>
		where T : IParserComponent<U>
		where U : class
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private List<T> _parserComponents;

		protected T[] parserComponents;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AbstractComponentBasedParser()
		{
			this._parserComponents = new List<T>();
		}

		public AbstractComponentBasedParser(params T[] parserComponents)
		{
			this._parserComponents = new List<T>();
			Register(parserComponents);
		}

		public AbstractComponentBasedParser(IEnumerable<T> parserComponents)
		{
			this._parserComponents = new List<T>();
			Register(parserComponents);
		}

		public AbstractComponentBasedParser(params Type[] parserComponentTypes)
		{
			this._parserComponents = new List<T>();
			Register(parserComponentTypes);
		}

		public AbstractComponentBasedParser(IEnumerable<Type> parserComponentTypes)
		{
			this._parserComponents = new List<T>();
			Register(parserComponentTypes);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public T[] ParserComponents
		{
			get {
				return _parserComponents.ToArray();
			}
		}

		public virtual string[] ShortHelp
		{
			get {
				return null;
			}
		}

		public virtual EnumDataType[] ValidInputDataTypes
		{
			get {
				return null;
			}
		}

		public virtual EnumDataType[] OutputDataTypes
		{
			get {
				return null;
			}
		}

		public virtual string[] LongHelpText
		{
			get {
				return null;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void Register(Type parserComponentType)
		{
			T parserComponent = (T)(
				parserComponentType.GetConstructor(new Type[0]).Invoke(new object[0])
				);
			_parserComponents.Add(parserComponent);

			__RebuildListOfParserComponents();
		}

		public void Register(params Type[] parserComponentTypes)
		{
			foreach (Type parserComponentType in parserComponentTypes) {
				T parserComponent = (T)(
					parserComponentType.GetConstructor(new Type[0]).Invoke(new object[0])
					);
				_parserComponents.Add(parserComponent);
			}

			__RebuildListOfParserComponents();
		}

		public void Register(IEnumerable<Type> parserComponentTypes)
		{
			foreach (Type parserComponentType in parserComponentTypes) {
				T parserComponent = (T)(
					parserComponentType.GetConstructor(new Type[0]).Invoke(new object[0])
					);
				_parserComponents.Add(parserComponent);
			}

			__RebuildListOfParserComponents();
		}

		public void Register(T parserComponent)
		{
			_parserComponents.Add(parserComponent);

			__RebuildListOfParserComponents();
		}

		public void Register(params T[] parserComponents)
		{
			this._parserComponents.AddRange(parserComponents);

			__RebuildListOfParserComponents();
		}

		public void Register(IEnumerable<T> parserComponents)
		{
			this._parserComponents.AddRange(parserComponents);

			__RebuildListOfParserComponents();
		}

		////////////////////////////////////////////////////////////////

		private void __RebuildListOfParserComponents()
		{
			T[] parserComponents = _parserComponents.ToArray();
			Array.Sort(parserComponents, ____Comparator);
			this.parserComponents = parserComponents;
		}

		private int ____Comparator(T x, T y)
		{
			string xs = ____Max(x.ShortHelp);
			string ys = ____Max(y.ShortHelp);

			if (xs.Length < ys.Length) return 1;
			if (xs.Length > ys.Length) return -1;

			return xs.CompareTo(ys);
		}

		private static string ____Max(string[] lines)
		{
			string line = lines[0];
			for (int i = 1; i < lines.Length; i++) {
				if (lines[i].Length > line.Length) {
					line = lines[i];
				}
			}
			return line;
		}

		////////////////////////////////////////////////////////////////

		public U TryParse(IParsingContext ctx, TokenStream tokens)
		{
			foreach (T parserComponent in parserComponents) {
				U obj = parserComponent.TryParse(ctx, tokens);
				if (obj != null) return obj;
			}

			return null;
		}

	}

}
