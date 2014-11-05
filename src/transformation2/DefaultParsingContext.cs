using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.transformation2
{

	public class DefaultParsingContext : IParsingContext
	{

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private Dictionary<string, ILanguageStrategy> languageStrategies;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public DefaultParsingContext()
		{
			languageStrategies = new Dictionary<string, ILanguageStrategy>();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		public void AddStrategy(ILanguageStrategy strategy)
		{
			languageStrategies.Add(strategy.Name, strategy);
		}

		public ILanguageStrategy GetStrategy(string name)
		{
			ILanguageStrategy m;
			if (languageStrategies.TryGetValue(name, out m)) return m;
			return null;
		}

		public ILanguageStrategy GetStrategyE(string name)
		{
			ILanguageStrategy m;
			if (languageStrategies.TryGetValue(name, out m)) return m;
			throw new Exception("No such language strategy: " + name);
		}

	}

}
