using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class Core : SingletonAccessor, IDisposable
{
	// Singleton Pattern
	private static readonly Core _singleton = new();

	public static Core Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static Core() { }

	// Singleton Pattern End

	public SearchTypes CurrentSearchType { get; set; } = SearchTypes.None;

	public bool IsLanguageAny { get; set; } = false;
	public bool IsQuestTypeNoPreference { get; set; } = false;
	public bool IsQuestRewardsNoPreference { get; set; } = false;
	public bool IsExpeditionObjectiveNoPreference { get; set; } = false;
	public bool IsRegionLevelNoPreference { get; set; } = false;
	public bool IsTargetMonsterNoPreference { get; set; } = false;

	private delegate int startRequest_Delegate(nint netCore, nint netRequest);
	private Hook<startRequest_Delegate> StartRequestHook { get; set; }

	private delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	private Hook<numericalFilter_Delegate> NumericalFilterHook { get; set; }

	private Core() { }

	public Core Init()
	{
		TeaLog.Info("Core: Initializing Hooks...");

		InstantiateSingletons();

		StartRequestHook = Hook.Create<startRequest_Delegate>(0x1421e2430, OnStartRequest);

		// 0x7FFE2A0B5700
		var numericalFilterAddress = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListNumericalFilter);
		NumericalFilterHook = Hook.Create<numericalFilter_Delegate>(numericalFilterAddress, OnNumericalFilter);

		TeaLog.Info("Core: Hook Initialization Done!");

		return this;
	}

	public string GetSearchKeyName(string key)
	{
		if(CurrentSearchType == SearchTypes.Quest)
		{
			if(key == "SearchKey0") return "Search Type";
			if(key == "SearchKey1") return "???";
			if(key == "SearchKey2") return "Rewards Available";
			if(key == "SearchKey3") return "Target";
			if(key == "SearchKey4") return "Difficulty";
			if(key == "SearchKey5") return "Language";
			if(key == "SearchKey6") return "Quest Type";
			if(key == "SearchKey7") return "???";
			if(key == "SearchKey8") return "???";

			return "";
		}

		if(CurrentSearchType == SearchTypes.Session)
		{
			if(key == "SearchKey0") return "Search Type";
			if(key == "SearchKey1") return "Player Type";
			if(key == "SearchKey2") return "Quest Preference";
			if(key == "SearchKey3") return "???";
			if(key == "SearchKey4") return "Language";
			if(key == "SearchKey5") return "Similar Hunter Rank";
			if(key == "SearchKey6") return "Similar Master Rank";
			if(key == "SearchKey7") return "Min Master Rank";
			if(key == "SearchKey8") return "Max Master Rank";

			return "";
		}

		if(CurrentSearchType == SearchTypes.GuidingLands)
		{
			if(key == "SearchKey0") return "Search Type";
			if(key == "SearchKey1") return "Guiding Lands";
			if(key == "SearchKey2") return "Expedition Objective";
			if(key == "SearchKey3") return "Target Monster";
			if(key == "SearchKey4") return "Conditions";
			if(key == "SearchKey5") return "???";
			if(key == "SearchKey6") return "Region Level";
			if(key == "SearchKey7") return "???";
			if(key == "SearchKey8") return "???";

			return "";
		}

		return "";
	}

	public static string GetComparisonSign(int comparison)
	{
		if (comparison == -2) return "<=";
		if (comparison == -1) return "<";
		if (comparison == 0) return "==";
		if (comparison == 1) return ">";
		if (comparison == 2) return ">=";
		if (comparison == 3) return "!=";

		return "";
	}

	private void AnalyzeSearchKeys(nint netRequest)
	{
		var requestArguments = MemoryUtil.Read<int>(netRequest + 0x58);
		var searchKeyCount = MemoryUtil.Read<int>(requestArguments + 0x14);

		var searchKeyData = requestArguments + 0x1C;

		var isLanguageUpdated = false;
		var isQuestRewardsUpdated = false;
		var isQuestTypeUpdated = false;
		var isExpeditionObjectiveUpdated = false;
		var isRegionLevelUpdated = false;
		var isTargetMonsterUpdated = false;

		for(int i = 0; i < searchKeyCount; i++)
		{
			var keyID = MemoryUtil.Read<int>(searchKeyData - 0x4);
			var value = MemoryUtil.Read<int>(searchKeyData + 0x8);

			if(keyID == Constants.SEARCH_KEY_SEARCH_TYPE_ID)
			{
				CurrentSearchType = value switch
				{
					(int) SearchTypes.Session => SearchTypes.Session,
					(int) SearchTypes.Quest => SearchTypes.Quest,
					_ => SearchTypes.None
				};

				searchKeyData += 0x10;
				continue;
			}

			if(CurrentSearchType == SearchTypes.Session)
			{
				if(keyID == (int) SessionSearchKeyIDs.Language)
				{
					IsLanguageAny = false;
					isLanguageUpdated = true;
				}

				searchKeyData += 0x10;
				continue;
			}

			if(CurrentSearchType == SearchTypes.Quest)
			{
				if(keyID == (int) GuidingLandsSearchKeyIDs.IsGuidingLands)
				{
					if(value == (int) GuidingLands.Yes)
					{
						CurrentSearchType = SearchTypes.GuidingLands;
					}

					searchKeyData += 0x10;
					continue;
				}
				
				if(keyID == (int) QuestSearchKeyIDs.RewardsAvailable)
				{
					IsQuestRewardsNoPreference = false;
					isQuestRewardsUpdated = true;

					searchKeyData += 0x10;
					continue;
				}
				
				if(keyID == (int) QuestSearchKeyIDs.Language)
				{
					IsLanguageAny = false;
					isLanguageUpdated = true;

					searchKeyData += 0x10;
					continue;
				}
				
				if(keyID == (int) QuestSearchKeyIDs.QuestType)
				{
					IsQuestTypeNoPreference = false;
					isQuestTypeUpdated = true;

					searchKeyData += 0x10;
					continue;
				}
			}

			if(CurrentSearchType == SearchTypes.GuidingLands)
			{
				if(keyID == (int) GuidingLandsSearchKeyIDs.ExpeditionObjective)
				{
					IsExpeditionObjectiveNoPreference = false;
					isExpeditionObjectiveUpdated = true;

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) GuidingLandsSearchKeyIDs.RegionLevel)
				{
					IsRegionLevelNoPreference = false;
					isRegionLevelUpdated = true;

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) GuidingLandsSearchKeyIDs.TargetMonster)
				{
					IsTargetMonsterNoPreference = false;
					isTargetMonsterUpdated = true;

					searchKeyData += 0x10;
					continue;
				}
			}

			searchKeyData += 0x10;
		}

		if(!isLanguageUpdated) IsLanguageAny = true;
		if(!isQuestTypeUpdated) IsQuestTypeNoPreference = true;
		if(!isQuestRewardsUpdated) IsQuestRewardsNoPreference = true;
		if(!isExpeditionObjectiveUpdated) IsExpeditionObjectiveNoPreference = true;
		if(!isRegionLevelUpdated) IsRegionLevelNoPreference = true;
		if(!isTargetMonsterUpdated) IsTargetMonsterNoPreference = true;
	}

	private int OnStartRequest(nint netCore, nint netRequest)
	{
		try
		{
			// Phase Check
			var phase = MemoryUtil.Read<int>(netRequest + 0xE0);

			if(phase != 0)
			{
				CurrentSearchType = SearchTypes.None;
				return StartRequestHook!.Original(netCore, netRequest);
			}

			TeaLog.Info("OnStartRequest");

			AnalyzeSearchKeys(netRequest);

			if(CurrentSearchType == SearchTypes.None) return StartRequestHook!.Original(netCore, netRequest);

			// Max Results

			ref var maxResultsRef = ref MemoryUtil.GetRef<int>(netRequest + 0x60);

			// Apply Stuff

			MaxSearchResultLimit_I.Apply(ref maxResultsRef);
			SteamRegionLockFix_I.Apply();
			SessionPlayerCountFilter_I.ApplyMin().ApplyMax();

			LanguageFilter_I.ApplyAnyLanguage();
			QuestTypeFilter_I.ApplyNoPreference();
			RewardFilter_I.ApplyNoPreference();

			ExpeditionObjectiveFilter_I.ApplyNoPreference();
			RegionLevelFilter_I.ApplyNoPreference();
			TargetMonsterFilter_I.ApplyNoPreference();
		}
		catch(Exception exception)
		{
			DebugManager_I.Report("Core.OnStartRequest()", exception.ToString());
		}

		return StartRequestHook!.Original(netCore, netRequest);
	}

	private void OnNumericalFilter(nint steamInterface, nint keyAddress, int value, int comparison)
	{
		var skip = false;

		try
		{
			if(CurrentSearchType == SearchTypes.None)
			{
				NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
				return;
			}

			var key = MemoryUtil.ReadString(keyAddress);

			TeaLog.Info($"{key} ({GetSearchKeyName(key)}) {GetComparisonSign(comparison)} {value}");

			skip = PlayerTypeFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = QuestPreferenceFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = LanguageFilter_I.ApplySameLanguage(ref key, ref value, ref comparison) || skip;

			skip = QuestTypeFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = DifficultyFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = RewardFilter_I.ApplyRewardsAvailable(ref key, ref value, ref comparison) || skip;
			skip = TargetFilter_I.Apply(ref key, ref value, ref comparison) || skip;

			skip = ExpeditionObjectiveFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = RegionLevelFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = TargetMonsterFilter_I.Apply(ref key, ref value, ref comparison) || skip;
		}
		catch(Exception exception)
		{
			DebugManager_I.Report("Core.OnNumericalFilter()", exception.ToString());
		}

		if(!skip) NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
	}

	public void Dispose()
	{
		TeaLog.Info("Core: Disposing Hooks...");
		if(StartRequestHook != null) StartRequestHook?.Dispose();
		if(NumericalFilterHook != null) NumericalFilterHook?.Dispose();
	}
}
