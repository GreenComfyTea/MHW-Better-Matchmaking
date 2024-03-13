using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly ExpeditionObjectiveFilter _singleton = new();

	public static ExpeditionObjectiveFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static ExpeditionObjectiveFilter() { }

	// Singleton Pattern End

	public ExpeditionObjectiveFilterCustomization Customization { get; set; }

	private ExpeditionObjectiveFilter() { }

	public ExpeditionObjectiveFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	private ExpeditionObjectiveFilter Apply()
	{
		var generalFilterOptions = Customization.FilterOptions.General;
		var fieldResearchFilterOptions = Customization.FilterOptions.FieldResearch;
		var miningFilterOptions = Customization.FilterOptions.Mining;
		var boneResearchFilterOptions = Customization.FilterOptions.BoneResearch;
		var fixedRegionResearchFilterOptions = Customization.FilterOptions.FixedRegion;

		if(!generalFilterOptions.None)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping None...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.None, LobbyComparison.NotEqual);
		}

		if(!fieldResearchFilterOptions.FieldResearchForest)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Field Research: Forest...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FieldResearchForest, LobbyComparison.NotEqual);
		}

		if(!fieldResearchFilterOptions.FieldResearchWildspire)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Field Research: Wildspire...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FieldResearchWildspire, LobbyComparison.NotEqual);
		}

		if(!fieldResearchFilterOptions.FieldResearchCoral)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Field Research: Coral...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FieldResearchCoral, LobbyComparison.NotEqual);
		}

		if(!fieldResearchFilterOptions.FieldResearchRotted)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Field Research: Rotted...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FieldResearchRotted, LobbyComparison.NotEqual);
		}


		if(!fieldResearchFilterOptions.FieldResearchVolcanic)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Field Research: Volcanic...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FieldResearchVolcanic, LobbyComparison.NotEqual);
		}

		if(!fieldResearchFilterOptions.FieldResearchTundra)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Field Research: Tundra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FieldResearchTundra, LobbyComparison.NotEqual);
		}



		if(!miningFilterOptions.MiningForest)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Mining: Forest...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.MiningForest, LobbyComparison.NotEqual);
		}

		if(!miningFilterOptions.MiningWildspire)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Mining: Wildspire...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.MiningWildspire, LobbyComparison.NotEqual);
		}

		if(!miningFilterOptions.MiningCoral)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Mining: Coral...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.MiningCoral, LobbyComparison.NotEqual);
		}

		if(!miningFilterOptions.MiningRotted)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Mining: Rotted...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.MiningRotted, LobbyComparison.NotEqual);
		}


		if(!miningFilterOptions.MiningVolcanic)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Mining: Volcanic...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.MiningVolcanic, LobbyComparison.NotEqual);
		}

		if(!miningFilterOptions.MiningTundra)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Mining: Tundra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.MiningTundra, LobbyComparison.NotEqual);
		}



		if(!boneResearchFilterOptions.BoneResearchForest)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Bone Research: Forest...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.BoneResearchForest, LobbyComparison.NotEqual);
		}

		if(!boneResearchFilterOptions.BoneResearchWildspire)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Bone Research: Wildspire...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.BoneResearchWildspire, LobbyComparison.NotEqual);
		}

		if(!boneResearchFilterOptions.BoneResearchCoral)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Bone Research: Coral...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.BoneResearchCoral, LobbyComparison.NotEqual);
		}

		if(!boneResearchFilterOptions.BoneResearchRotted)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Bone Research: Rotted...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.BoneResearchRotted, LobbyComparison.NotEqual);
		}


		if(!boneResearchFilterOptions.BoneResearchVolcanic)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Bone Research: Volcanic...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.BoneResearchVolcanic, LobbyComparison.NotEqual);
		}

		if(!boneResearchFilterOptions.BoneResearchTundra)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Bone Research: Tundra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.BoneResearchTundra, LobbyComparison.NotEqual);
		}



		if(!fixedRegionResearchFilterOptions.FixedRegionForest)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Fixed Region: Forest...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FixedRegionForest, LobbyComparison.NotEqual);
		}

		if(!fixedRegionResearchFilterOptions.FixedRegionWildspire)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Fixed Region: Wildspire...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FixedRegionWildspire, LobbyComparison.NotEqual);
		}

		if(!fixedRegionResearchFilterOptions.FixedRegionCoral)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Fixed Region: Coral...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FixedRegionCoral, LobbyComparison.NotEqual);
		}

		if(!fixedRegionResearchFilterOptions.FixedRegionRotted)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Fixed Region: Rotted...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FixedRegionRotted, LobbyComparison.NotEqual);
		}


		if(!fixedRegionResearchFilterOptions.FixedRegionVolcanic)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Fixed Region: Volcanic...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FixedRegionVolcanic, LobbyComparison.NotEqual);
		}

		if(!fixedRegionResearchFilterOptions.FixedRegionTundra)
		{
			TeaLog.Info("ExpeditionObjectiveFilter: Skipping Fixed Region: Tundra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE, (int) ExpeditionObjectives.FixedRegionTundra, LobbyComparison.NotEqual);
		}

		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		if(!Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.GuidingLands) return false;
		if(comparison != (int) LobbyComparison.Equal) return false;
		if(!key.Equals(Constants.SEARCH_KEY_GUIDING_LANDS_EXPEDITION_OBJECTIVE)) return false;
		if(value != (int) Customization.ReplacementTargetEnum) return false;

		TeaLog.Info("ExpeditionObjectiveFilter: Skipping Original Filter...");
		Apply();

		return true;
	}

	public ExpeditionObjectiveFilter ApplyNoPreference()
	{
		if(!Core_I.IsExpeditionObjectiveNoPreference) return this;
		if(!Customization.Enabled) return this;
		if(Core_I.CurrentSearchType != SearchTypes.GuidingLands) return this;
		if(Customization.ReplacementTargetEnum != ExpeditionObjectives.None) return this;

		Apply();

		return this;
	}
}
