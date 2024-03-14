using SharpPluginLoader.Core.Experimental;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class SteamRegionLockFix : SingletonAccessor
{
	// Singleton Pattern
	private static readonly SteamRegionLockFix _singleton = new();

	public static SteamRegionLockFix Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static SteamRegionLockFix() { }

	// Singleton Pattern End

	//private delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	//private MarshallingHook<numericalFilter_Delegate> numericalFilter;

	public SteamRegionLockFixCustomization SessionCustomization { get; set; }
	public SteamRegionLockFixCustomization QuestCustomization { get; set; }
	public SteamRegionLockFixCustomization GuidingLandsCustomization { get; set; }

	private SteamRegionLockFix()
	{
		InstantiateSingletons();
	}

	public SteamRegionLockFix Apply()
	{
		if (Core_I.CurrentSearchType == SearchTypes.None) return this;

		var customization = Core_I.CurrentSearchType switch
		{
			SearchTypes.Session => SessionCustomization,
			SearchTypes.Quest => QuestCustomization,
			SearchTypes.GuidingLands => GuidingLandsCustomization,
			_ => null
		};

		if (!customization.Enabled) return this;
		if (customization.DistanceFilterEnum == LobbyDistanceFilter.Default) return this;

		Matchmaking.AddRequestLobbyListDistanceFilter(customization.DistanceFilterEnum);

		TeaLog.Info($"RegionLockFix: Set Distance to {customization.DistanceFilterEnum}.");
		return this;
	}
}
