using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class DifficultyFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly DifficultyFilter _singleton = new();

	public static DifficultyFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static DifficultyFilter() { }

	// Singleton Pattern End

	public DifficultyFilterCustomization Customization { get; set; }

	private DifficultyFilter() { }

	public DifficultyFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	private DifficultyFilter Apply()
	{
		if(!Customization.FilterOptions.LowRank.LowRank1)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Low Rank 1...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.LowRank1, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.LowRank.LowRank2)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Low Rank 2...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.LowRank2, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.LowRank.LowRank3)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Low Rank 3...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.LowRank3, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.LowRank.LowRank4)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Low Rank 4...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.LowRank4, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.LowRank.LowRank5)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Low Rank 5...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.LowRank5, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.HighRank.HighRank6)
		{
			TeaLog.Info($"DifficultyFilter: Skipping High Rank 6...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.HighRank6, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.HighRank.HighRank7)
		{
			TeaLog.Info($"DifficultyFilter: Skipping High Rank 7...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.HighRank7, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.HighRank.HighRank8)
		{
			TeaLog.Info($"DifficultyFilter: Skipping High Rank 8...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.HighRank8, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.HighRank.HighRank9)
		{
			TeaLog.Info($"DifficultyFilter: Skipping High Rank 9...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.HighRank9, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.MasterRank.MasterRank1)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Master Rank 1...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.MasterRank1, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.MasterRank.MasterRank2)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Master Rank 2...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.MasterRank2, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.MasterRank.MasterRank3)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Master Rank 3...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.MasterRank3, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.MasterRank.MasterRank4)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Master Rank 4...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.MasterRank4, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.MasterRank.MasterRank5)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Master Rank 5...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.MasterRank5, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.MasterRank.MasterRank6)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Master Rank 6...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY, (int) Difficulties.MasterRank6, LobbyComparison.NotEqual);
		}

		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		var comparisonEnum = (LobbyComparison) comparison;

		if (!Customization.Enabled) return false;
		if (Core_I.CurrentSearchType != SearchTypes.Quest) return false;
		if(comparisonEnum == LobbyComparison.NotEqual) return false;
		if(!key.Equals(Constants.SEARCH_KEY_SESSION_QUEST_DIFFICULTY)) return false;

		TeaLog.Info(Customization.ReplacementTargetEnum.ToString());

		// Low Rank Search
		// Rank <= LR5
		if(comparisonEnum == LobbyComparison.EqualToOrLessThan
		&& value == (int) Difficulties.LowRank5
		&& Customization.ReplacementTargetEnum == Difficulties.LowRank)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Original Call...");
			Apply();

			return true;
		}

		// High Rank Search
		// Rank > LR5
		if(comparisonEnum == LobbyComparison.EqualToOrGreaterThan
		&& value == (int) (Difficulties.LowRank5)
		&& Customization.ReplacementTargetEnum == Difficulties.HighRank)
		{
			TeaLog.Info($"DifficultyFilter: High Rank Search -> Lower Threshold -> Skipping Original Call...");

			return true;
		}

		// Rank <= HR9
		if(comparisonEnum == LobbyComparison.EqualToOrLessThan
		&& value == (int) (Difficulties.HighRankSearch)
		&& Customization.ReplacementTargetEnum == Difficulties.HighRank)
		{
			TeaLog.Info($"DifficultyFilter: High Rank Search -> Upper Threshold -> Skipping Original Call...");
			Apply();

			return true;
		}

		// Master Rank Search
		// Rank > HR9
		if(comparisonEnum == LobbyComparison.GreaterThan
		&& value == (int) (Difficulties.HighRankSearch)
		&& Customization.ReplacementTargetEnum == Difficulties.MasterRank)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Original Call...");
			Apply();

			return true;
		}

		if(comparisonEnum == LobbyComparison.Equal
		&& value == (int) Customization.ReplacementTargetEnum)
		{
			TeaLog.Info($"DifficultyFilter: Skipping Original Call...");
			Apply();

			return true;
		}

		return false;
	}
}
