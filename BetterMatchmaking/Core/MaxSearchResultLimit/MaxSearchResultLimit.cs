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
	private static readonly MaxSearchResultLimit singleton = new();

	public static MaxSearchResultLimit Instance { get { return singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static MaxSearchResultLimit() { }

	// Singleton Pattern End

	public MaxSearchResultLimitCustomization Customization { get; set; }

	private MaxSearchResultLimit() { }

	public MaxSearchResultLimit Apply(ref int maxResults)
	{
		if (!Customization.Enabled) return this;
		if(Customization.Value == Constants.DEFAULT_MAX_SEARCH_RESULT_LIMIT) return this;

		maxResults = 50;

		TeaLog.Info($"MaxSearchResultLimit: Set Value to {Customization.Value}.");

		return this;

	}
}
