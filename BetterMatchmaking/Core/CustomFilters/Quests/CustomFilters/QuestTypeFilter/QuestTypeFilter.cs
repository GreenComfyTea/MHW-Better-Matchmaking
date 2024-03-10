using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class QuestTypeFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly QuestTypeFilter _singleton = new();

	public static QuestTypeFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static QuestTypeFilter() { }

	// Singleton Pattern End

	public QuestTypeFilterCustomization Customization { get; set; }

	private QuestTypeFilter() { }

	public QuestTypeFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		if(!Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.Quest) return false;
		if((LobbyComparison) comparison != LobbyComparison.Equal) return false;
		if(!key.Equals(Constants.SEARCH_KEY_SESSION_QUEST_TYPE)) return false;
		if(value != (int) Customization.QuestTypeReplacementTargetEnum) return false;

		TeaLog.Info("QuestTypeFilter: Skipping Original Call...");

		if(!Customization.FilterOptions.OptionalQuests)
		{
			TeaLog.Info("QuestTypeFilter: Skipping Optional Quests...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_TYPE, (int) QuestTypes.OptionalQuests, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Assignments)
		{
			TeaLog.Info("QuestTypeFilter: Skipping Assignments...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_TYPE, (int) QuestTypes.Assignments, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Investigations)
		{
			TeaLog.Info("QuestTypeFilter: Skipping Investigations...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_TYPE, (int) QuestTypes.Investigations, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Expeditions)
		{
			TeaLog.Info("QuestTypeFilter: Skipping Expeditions...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_TYPE, (int) QuestTypes.Expeditions, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.EventQuests)
		{
			TeaLog.Info("QuestTypeFilter: Skipping Event Quests...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_TYPE, (int) QuestTypes.EventQuests, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.SpecialInvestigations)
		{
			TeaLog.Info("QuestTypeFilter: Skipping Special Investigations...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_TYPE, (int) QuestTypes.SpecialInvestigations, LobbyComparison.NotEqual);
		}

		return true;
	}
}
