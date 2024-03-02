using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class MaxSearchResultLimit : SingletonAccessor
{
	// Singleton Pattern
	private static readonly MaxSearchResultLimit _singleton = new();

	public static MaxSearchResultLimit Instance { get { return _singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static MaxSearchResultLimit() { }

	// Singleton Pattern End

	public MaxSearchResultLimitCustomization Customization { get; set; }

	private MaxSearchResultLimit() { }

	public MaxSearchResultLimit Apply(SearchTypes searchType, ref int maxResultsRef)
	{
		if (searchType == SearchTypes.None) return this;

		MaxSearchResultLimitLobbyCustomization customization;
		int maxResults;

		switch (searchType)
		{
			case SearchTypes.Session:

				customization = Customization.Sessions;
				maxResults = customization.Value;
				break;

			case SearchTypes.Quest:

				customization = Customization.Quests;
				maxResults = customization.Value + 1;
				break;

			default:
				return this;
		}

		if (!customization.Enabled) return this;

		maxResultsRef = maxResults;

		TeaLog.Info($"MaxSearchResultLimit: Set to {maxResults}.");
		return this;
	}
}