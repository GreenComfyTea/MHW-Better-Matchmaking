using SharpPluginLoader.Core.Memory;
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

	public bool AreHooksInitialized { get; set; } = false;

	public delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	public Hook<numericalFilter_Delegate> NumericalFilterHook { get; set; }

	public delegate int startRequest_Delegate(nint netCore, nint netRequest);
	public static Hook<startRequest_Delegate> StartRequestHook { get; set; }

	private Core() { }

	public Core Init()
	{
		AreHooksInitialized = true;

		Task.Run(() =>
		{
			TeaLog.Info("Core: Initializing Hooks...");

			InstantiateSingletons();

			SteamApi.Init();

			// 0x7FFE2A0B5700
			var numericalFilterAddress = SteamApi.GetVirtualFunction(SteamApi.VirtualFunctionIndex.AddRequestLobbyListNumericalFilter);
			NumericalFilterHook = Hook.Create<numericalFilter_Delegate>(numericalFilterAddress, OnNumericalFilter);

			StartRequestHook = Hook.Create<startRequest_Delegate>(0x1421e2430, OnStartRequest);

			TeaLog.Info("Core: Hook Initialization Done!");
		});

		return this;
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
		if (comparison == 0) return "==";
		if (comparison == 1) return ">";
		if (comparison == 2) return ">=";
		if (comparison == 3) return "!=";

		return "";
	}

	private static SearchTypes GetSearchType(nint netRequest)
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

		if (phase != 0) return StartRequestHook!.Original(netCore, netRequest);

		TeaLog.Info("startRequest\n");

		// Get Lobby Search Type

		var searchType = GetSearchType(netRequest);

		if (searchType == SearchTypes.None) return StartRequestHook!.Original(netCore, netRequest);

		// Max Results

		ref var maxResultsRef = ref MemoryUtil.GetRef<int>(netRequest + 0x60);

		// Apply Stuff

		MaxSearchResultLimitInstance.Apply(searchType, ref maxResultsRef);
		RegionLockFixInstance.Apply(searchType);
		SessionPlayerCountFilterInstance.ApplyMin(searchType).ApplyMax(searchType);

		return StartRequestHook!.Original(netCore, netRequest);
	}

	private void OnNumericalFilter(nint steamInterface, nint keyAddress, int value, int comparison)
	{
		try
		{
			TeaLog.Info("OnNumericalFilter");

			var key = MemoryUtil.ReadString(keyAddress);

			TeaLog.Info($"{key} ({GetSearchKeyName(key)}) {GetComparisonSign(comparison)} {value}");

			NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
			NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
		}
	}

	public void Dispose()
	{
		TeaLog.Info("Core: Disposing Hooks...");
		NumericalFilterHook?.Dispose();
		StartRequestHook?.Dispose();
	}
}
