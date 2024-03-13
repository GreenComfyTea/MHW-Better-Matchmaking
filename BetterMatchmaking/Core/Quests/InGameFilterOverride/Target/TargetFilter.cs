using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class TargetFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly TargetFilter _singleton = new();

	public static TargetFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static TargetFilter() { }

	// Singleton Pattern End

	public TargetFilterCustomization Customization { get; set; }

	private TargetFilter() { }

	public TargetFilter Init()
	{
		InstantiateSingletons();
		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		var comparisonEnum = (LobbyComparison) comparison;

		if (!Customization.Enabled) return false;
		if (Core_I.CurrentSearchType != SearchTypes.Quest) return false;
		if (comparisonEnum == LobbyComparison.NotEqual) return false;
		if (!key.Equals(Constants.SEARCH_KEY_QUEST_TARGET)) return false;
		if (comparisonEnum == LobbyComparison.Equal && value != (int)Customization.ReplacementTargetEnum) return false;

		if (comparisonEnum == LobbyComparison.EqualToOrLessThan
		&& value == (int) Targets.SilverRathalos
		&& Customization.ReplacementTargetEnum != Targets.None)
		{
			return false;
		}

		var filterOptions = Customization.FilterOptions;
		var generalFilterOptions = filterOptions.General;

		TeaLog.Info("TargetFilter: Skipping Original Filter...");

		if (!generalFilterOptions.None)
		{
			TeaLog.Info("TargetFilter: Skipping None...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int) Targets.None, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.SmallMonsters)
		{
			TeaLog.Info("TargetFilter: Skipping Small Monsters...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int) Targets.SmallMonsters, LobbyComparison.NotEqual);
		}

		QuestPreferenceTargetFilter_I.Apply(
			Constants.SEARCH_KEY_QUEST_TARGET,
			filterOptions.BaseGameMsqMonsters,
			filterOptions.BaseGameEndgameMonsters,
			filterOptions.IceborneMSQMonsters,
			filterOptions.IceborneEndgameMonsters
		);

		return true;
	}
}
