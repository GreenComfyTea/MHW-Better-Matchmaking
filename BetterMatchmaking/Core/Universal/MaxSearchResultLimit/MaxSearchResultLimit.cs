using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class MaxSearchResultLimit : SingletonAccessor
{
	// Singleton Pattern
	private static readonly MaxSearchResultLimit _singleton = new();

	public static MaxSearchResultLimit Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static MaxSearchResultLimit() { }

	// Singleton Pattern End

	public MaxSearchResultLimitCustomization SessionCustomization { get; set; }
	public MaxSearchResultLimitCustomization QuestCustomization { get; set; }
	public MaxSearchResultLimitCustomization GuidingLandsCustomization { get; set; }
	private MaxSearchResultLimit()
	{
		InstantiateSingletons();
	}

	public MaxSearchResultLimit Apply(ref int maxResultsRef)
	{
		if(Core_I.CurrentSearchType == SearchTypes.None) return this;

		var customization = Core_I.CurrentSearchType switch
		{
			SearchTypes.Session => SessionCustomization,
			SearchTypes.Quest => QuestCustomization,
			SearchTypes.GuidingLands => GuidingLandsCustomization,
			_ => null
		};

		if(!customization.Enabled) return this;

		maxResultsRef = customization.Value;

		TeaLog.Info($"MaxSearchResultLimit: Set to {customization.Value}.");
		return this;
	}
}