using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class RegionLockFix : SingletonAccessor
{
	// Singleton Pattern
	private static readonly RegionLockFix singleton = new();

	public static RegionLockFix Instance { get { return singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static RegionLockFix() { }

	// Singleton Pattern End

	public RegionLockFixCustomization Customization { get; set; }

	private RegionLockFix() { }

	public RegionLockFix Apply()
	{
		if (!Customization.Enabled) return this;
		if(Customization.DistanceFilterEnum == LobbyDistanceFilter.Default) return this;

		Matchmaking.AddRequestLobbyListDistanceFilter(Customization.DistanceFilterEnum);

		TeaLog.Info($"RegionLockFix: Set Distance to {Customization.DistanceFilterEnum}.");
		return this;

	}
}
