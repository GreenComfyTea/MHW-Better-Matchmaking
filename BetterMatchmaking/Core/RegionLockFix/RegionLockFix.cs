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

internal class RegionLockFix : SingletonAccessor
{
	// Singleton Pattern
	private static readonly RegionLockFix _singleton = new();

	public static RegionLockFix Instance { get { return _singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static RegionLockFix() { }

	// Singleton Pattern End

	//private delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	//private MarshallingHook<numericalFilter_Delegate> numericalFilter;

	public RegionLockFixCustomization Customization { get; set; }

	private RegionLockFix() { }

	public RegionLockFix Init()
	{
		// 0x7FFE2A0B5700
		//var numericalFilterAddress = SteamApi.GetVirtualFunction(SteamApi.VirtualFunctionIndex.AddRequestLobbyListNumericalFilter);
		//numericalFilter = MarshallingHook.Create<numericalFilter_Delegate>(numericalFilterAddress, (steamInterface, keyAddress, value, comparison) => {});

		return this;
	}

	public RegionLockFix Apply(SearchTypes searchType)
	{
		if (searchType == SearchTypes.None) return this;

		RegionLockFixLobbyCustomization customization;

		switch (searchType)
		{
			case SearchTypes.Session:

				customization = Customization.Sessions;
				break;

			case SearchTypes.Quest:

				customization = Customization.Quests;
				break;

			default:
				return this;
		}

		if (!customization.Enabled) return this;
		if (customization.DistanceFilterEnum == LobbyDistanceFilter.Default) return this;

		Matchmaking.AddRequestLobbyListDistanceFilter(customization.DistanceFilterEnum);

		TeaLog.Info($"RegionLockFix: Set Distance to {customization.DistanceFilterEnum}.");
		return this;
	}
}
