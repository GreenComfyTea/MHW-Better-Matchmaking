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

		var searchKeyData = requestArguments + 0x1C;
		for(int i = 0; i < searchKeyCount; i++)
		{
			var keyId = MemoryUtil.Read<int>(searchKeyData - 0x4);
			var key = MemoryUtil.Read<int>(searchKeyData + 0x8);

			TeaLog.Info($"key {keyId}: {key}");

			if(keyId != Constants.SEARCH_KEY_SEARCH_TYPE_ID)
			{
				searchKeyData += 0x10;
				continue;
			}

			return key switch
			{
				(int) SearchTypes.Session => SearchTypes.Session,
				(int) SearchTypes.Quest => SearchTypes.Quest,
				_ => SearchTypes.None
			};
		}

		return SearchTypes.None;
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
				LanguageFilter_I.SkipNext = false;
				return StartRequestHook!.Original(netCore, netRequest);
			}

			TeaLog.Info("startRequest\n");

			// Get Lobby Search Type

			CurrentSearchType = GetSearchType(netRequest);

			if(CurrentSearchType == SearchTypes.None) return StartRequestHook!.Original(netCore, netRequest);

			// Max Results

			ref var maxResultsRef = ref MemoryUtil.GetRef<int>(netRequest + 0x60);

			// Apply Stuff

			MaxSearchResultLimit_I.Apply(CurrentSearchType, ref maxResultsRef);
			RegionLockFix_I.Apply(CurrentSearchType);
			SessionPlayerCountFilter_I.ApplyMin(CurrentSearchType).ApplyMax(CurrentSearchType);
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
			TeaLog.Info("OnNumericalFilter");

			var key = MemoryUtil.ReadString(keyAddress);

			TeaLog.Info($"{key} ({GetSearchKeyName(key)}) {GetComparisonSign(comparison)} {value}");

			skip = PlayerTypeFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = QuestPreferenceFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = LanguageFilter_I.ApplySameLanguage(ref key, ref value, ref comparison) || skip;
			skip = LanguageFilter_I.ApplyAnyLanguage(ref key, ref value, ref comparison) || skip;
		}
		catch(Exception exception)
		{
			DebugManager_I.Report("PlayerTypeLockBypass.OnNumericalFilter()", exception.ToString());
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
