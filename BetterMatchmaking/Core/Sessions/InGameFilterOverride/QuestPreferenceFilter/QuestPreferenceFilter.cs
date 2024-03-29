using SharpPluginLoader.Core.Entities;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class QuestPreferenceFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly QuestPreferenceFilter _singleton = new();

	public static QuestPreferenceFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static QuestPreferenceFilter() { }

	// Singleton Pattern End

	public QuestPreferenceFilterCustomization Customization { get; set; }

	private QuestPreferenceFilter() { }

	public QuestPreferenceFilter Init()
	{
		InstantiateSingletons();
		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		var comparisonEnum = (LobbyComparison)comparison;

		if (!Customization.Enabled) return false;
		if (Core_I.CurrentSearchType != SearchTypes.Session) return false;
		if (comparisonEnum == LobbyComparison.NotEqual) return false;
		if (!key.Equals(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE)) return false;
		if (comparisonEnum == LobbyComparison.Equal && value != (int)Customization.ReplacementTargetEnum) return false;

		if (comparisonEnum == LobbyComparison.EqualToOrLessThan
		&& value == (int) Targets.SilverRathalos
		&& Customization.ReplacementTargetEnum != Targets.None)
		{
			return false;
		}

		var filterOptions = Customization.FilterOptions;
		var generalFilterOptions = filterOptions.General;


		TeaLog.Info("QuestPreferenceFilter: Skipping Original Filter...");

		if (!generalFilterOptions.None)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping None...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.None, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.Assignments)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Assignments...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.Assignments, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.Optional)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Optional...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.Optional, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.Investigation)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Investigation...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.Investigation, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.TheGuidingLandsExpedition)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping The Guiding Lands Expedition...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.TheGuidingLandsExpedition, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.EventQuests)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Event Quests...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.EventQuests, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.SpecialAssignments)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Special Assignments...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.SpecialAssignments, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.Arena)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Arena...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.Arena, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.Expeditions)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Expeditions...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.Expeditions, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.TemperedMonsters)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Tempered Monsters...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.TemperedMonsters, LobbyComparison.NotEqual);
		}

		if (!generalFilterOptions.SmallMonsters)
		{
			TeaLog.Info("QuestPreferenceFilter: Skipping Small Monsters...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int) Targets.SmallMonsters, LobbyComparison.NotEqual);
		}

		UniversalTargetFilter_I.Apply(
			Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE,
			filterOptions.BaseGameMsqMonsters,
			filterOptions.BaseGameEndgameMonsters,
			filterOptions.IceborneMSQMonsters,
			filterOptions.IceborneEndgameMonsters
		);

		return true;
	}
}
