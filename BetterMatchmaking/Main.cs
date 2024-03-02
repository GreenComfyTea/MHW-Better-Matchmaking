using SharpPluginLoader.Core;
using ImGuiNET;
using System.Diagnostics;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using SharpPluginLoader.Core.Actions;
using SharpPluginLoader.Core.Entities;
using System.IO;
using System;
using SharpPluginLoader.Core.Experimental;
using SharpPluginLoader.Core.Networking;
using System.Net.WebSockets;
using System.Net;

namespace BetterMatchmaking;

internal class BetterMatchmakingPlugin : IPlugin
{
	public string Name => $"{Constants.MOD_NAME} v{Constants.VERSION}";

	public string Author => $"{Constants.MOD_AUTHOR}";

	private bool AreHooksInitialized { get; set; } = false;

	private LocalizationManager localizationManager;
	private ConfigManager configManager;
	private RegionLockFix regionLockFix;
	private MaxSearchResultLimit maxSearchResultLimit;
	private SessionPlayerCountFilter sessionPlayerCountFilter;

	private delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	private Hook<numericalFilter_Delegate> numericalFilterHook;


	private delegate int startRequest_Delegate(nint netCore, nint netRequest);
	private static Hook<startRequest_Delegate> startRequestHook;

	public PluginData Initialize()
	{
		return new PluginData
		{
			ImGuiWrappedInTreeNode = false

		};
	}

	public void Init()
	{
		try
		{
			TeaLog.Info("Plugin Loaded!");

			SteamApi.Init();

			localizationManager = LocalizationManager.Instance;
			configManager = ConfigManager.Instance;
			regionLockFix = RegionLockFix.Instance;
			maxSearchResultLimit = MaxSearchResultLimit.Instance;
			sessionPlayerCountFilter = SessionPlayerCountFilter.Instance;

			localizationManager.Init();
			configManager.Init();
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
		}
	}

	private void InitHooks()
	{
		TeaLog.Info("Hooks: Initializing...");

		AreHooksInitialized = true;
		SteamApi.Init();

		// 0x7FFE2A0B5700
		var numericalFilterAddress = SteamApi.GetVirtualFunction(SteamApi.VirtualFunctionIndex.AddRequestLobbyListNumericalFilter);
		numericalFilterHook = Hook.Create<numericalFilter_Delegate>(numericalFilterAddress, OnNumericalFilter);

		startRequestHook = Hook.Create<startRequest_Delegate>(0x1421e2430, OnStartRequest);

		TeaLog.Info("Hooks: Initialization Done!");
	}

	private SearchTypes GetSearchType(nint netRequest)
	{
		var requestArguments = MemoryUtil.Read<int>(netRequest + 0x58);
		var searchKeyCount = MemoryUtil.Read<int>(requestArguments + 0x14);

		var searchType = SearchTypes.None;

		var searchKeyData = requestArguments + 0x1C;
		for (int i = 0; i < searchKeyCount; i++)
		{
			var keyId = MemoryUtil.Read<int>(searchKeyData - 0x4);
			var key = MemoryUtil.Read<int>(searchKeyData + 0x8);

			TeaLog.Info($"key {keyId}: {key}");

			if (keyId != Constants.SEARCH_KEY_SEARCH_TYPE_ID)
			{
				searchKeyData += 0x10;
				continue;
			}

			switch (key)
			{
				case Constants.SESSION_SEARCH_ID:

					searchType = SearchTypes.Session;
					break;

				case Constants.QUEST_SEARCH_ID:

					searchType = SearchTypes.Quest;
					break;
			}

			searchKeyData += 0x10;
		}

		return searchType;
	}

	public int OnStartRequest(nint netCore, nint netRequest)
	{
		// Phase Check
		var phase = MemoryUtil.Read<int>(netRequest + 0xE0);

		if (phase != 0) return startRequestHook!.Original(netCore, netRequest);

		TeaLog.Info($"startRequest\n");

		// Get Lobby Search Type

		var searchType = GetSearchType(netRequest);

		if(searchType == SearchTypes.None) return startRequestHook!.Original(netCore, netRequest);

		// Max Results

		ref var maxResultsRef = ref MemoryUtil.GetRef<int>(netRequest + 0x60);

		// Apply Stuff

		maxSearchResultLimit.Apply(searchType, ref maxResultsRef);
		regionLockFix.Apply(searchType);
		sessionPlayerCountFilter.ApplyMin(searchType).ApplyMax(searchType);

		return startRequestHook!.Original(netCore, netRequest);
	}

	private void OnNumericalFilter(nint steamInterface, nint keyAddress, int value, int comparison)
	{
		try
		{
			TeaLog.Info($"OnNumericalFilter");

			var key = MemoryUtil.ReadString(keyAddress);

			TeaLog.Info($"{key} ({GetSearchKeyName(key)}) {GetComparisonSign(comparison)} {value}");

			numericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
			numericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
		}
	}

	public void OnLoad()
	{
		Init();
	}

	public void OnUnload()
	{
		Log.Info("OnUnload");
	}

	public static string GetSearchKeyName(string key)
	{
		if (key == "SearchKey1") return "Player Type | ???";
		if (key == "SearchKey2") return "Quest Preference | Rewards Available";
		if (key == "SearchKey3") return "??? | Target";
		if (key == "SearchKey4") return "Language | Quest Rank (HR/MR)";
		if (key == "SearchKey5") return "Similar Hunter Rank | Language";
		if (key == "SearchKey6") return "Similar Master Rank | Quest Type";
		if (key == "SearchKey7") return "Master Rank |";
		if (key == "SearchKey8") return "Master Rank |";
		return "";
	}

	public static string GetComparisonSign(int comparison)
	{
		if (comparison == -2) return "<=";
		if (comparison == -1) return "<";
		if (comparison ==  0) return "==";
		if (comparison ==  1) return ">";
		if (comparison ==  2) return ">=";
		if (comparison ==  3) return "!=";

		return "";
	}

	public void OnImGuiRender()
	{
		try
		{
			if (ImGui.Button($"{Constants.MOD_NAME} v{Constants.VERSION}"))
			{
				CustomizationWindow.Instance.IsOpened = !CustomizationWindow.Instance.IsOpened;
			}
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
		}
	}

	public void OnImGuiFreeRender()
	{
		try
		{
			if (!AreHooksInitialized) InitHooks();
			CustomizationWindow.Instance.Render();
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
		}
	}
}

// Same Language:

// Matchmake ============== -1
// Japanese ===============  0
// English ================  1
// French =================  2
// Italian ================  5
// German =================  4
// Spanish ================  3
// Brazilian Portuguese === 21
// Polish ================= 11
// Russian ================ 10
// Korean =================  6
// Traditional Chinese ====  7
// Simplified Chinese =====  8
// Arabic ================= 22
// Latin-American Spanish = 23

// Player Type:

// Beginners === 0
// Experienced = 1
// Any ========= 2
// Matchmake === 3

// Rank Preference

// Similar Hunter Rank: SearchKey5 =  6
// Similar Master Rank: SearchKey6 = 10