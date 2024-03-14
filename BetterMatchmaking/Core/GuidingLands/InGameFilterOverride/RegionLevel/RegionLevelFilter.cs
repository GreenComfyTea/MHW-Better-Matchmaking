using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class RegionLevelFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly RegionLevelFilter _singleton = new();

	public static RegionLevelFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static RegionLevelFilter() { }

	// Singleton Pattern End

	public RegionLevelFilterCustomization Customization { get; set; }

	private RegionLevelFilter() { }

	public RegionLevelFilter Init()
	{
		InstantiateSingletons();
		return this;
	}

	private RegionLevelFilter Apply()
	{
		if(!Customization.FilterOptions.Level1)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 1...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level1, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Level2)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 2...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level2, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Level3)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 3...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level3, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Level4)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 4...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level4, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Level5)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 5...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level5, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Level6)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 6...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level6, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Level7)
		{
			TeaLog.Info("RegionLevelFilter: Skipping Level 7...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL, (int) RegionLevels.Level7, LobbyComparison.NotEqual);
		}

		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		if(!Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.GuidingLands) return false;
		if(comparison != (int) LobbyComparison.Equal) return false;
		if(!key.Equals(Constants.SEARCH_KEY_GUIDING_LANDS_REGION_LEVEL)) return false;
		if(value != (int) Customization.ReplacementTargetEnum) return false;

		TeaLog.Info("RegionLevelFilter: Skipping Original Filter...");
		Apply();

		return true;
	}

	public RegionLevelFilter ApplyNoPreference()
	{
		if(!Core_I.IsRegionLevelNoPreference) return this;
		if(Core_I.IsExpeditionObjectiveNoPreference) return this;
		if(!Customization.Enabled) return this;
		if(Core_I.CurrentSearchType != SearchTypes.GuidingLands) return this;
		if(Customization.ReplacementTargetEnum != RegionLevels.NoPreference) return this;

		Apply();

		return this;
	}
}
