using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class RewardFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly RewardFilter _singleton = new();

	public static RewardFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static RewardFilter() { }

	// Singleton Pattern End

	public RewardFilterCustomization Customization { get; set; }

	private RewardFilter() { }

	public RewardFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		if(!Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.Quest) return false;
		if((LobbyComparison) comparison != LobbyComparison.Equal) return false;
		if(!key.Equals(Constants.SEARCH_KEY_SESSION_QUEST_REWARDS)) return false;

		TeaLog.Info("RewardFilter: Skipping Original Call...");

		if(!Customization.FilterOptions.NoRewards)
		{
			TeaLog.Info("RewardFilter: Skipping No Rewards...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_REWARDS, (int) Rewards.NoRewards, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.RewardsAvailable)
		{
			TeaLog.Info("RewardFilter: Skipping Rewards Available...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_REWARDS, (int) Rewards.RewardsAvailable, LobbyComparison.NotEqual);
		}

		return true;
	}
}
