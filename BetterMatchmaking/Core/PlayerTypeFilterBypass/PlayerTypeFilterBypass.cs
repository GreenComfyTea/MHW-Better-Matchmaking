using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BetterMatchmaking.Core;

namespace BetterMatchmaking;

internal class PlayerTypeFilterBypass : SingletonAccessor, IDisposable
{
	// Singleton Pattern
	private static readonly PlayerTypeFilterBypass _singleton = new();

	public static PlayerTypeFilterBypass Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static PlayerTypeFilterBypass() { }

	// Singleton Pattern End

	private delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	private Hook<numericalFilter_Delegate> NumericalFilterHook { get; set; }

	public PlayerTypeFilterBypassCustomization Customization { get; set; }

	private PlayerTypeFilterBypass() { }

	public PlayerTypeFilterBypass Init()
	{
		Task.Run(() =>
		{
			TeaLog.Info("PlayerTypeLockBypass: Initializing Hooks...");

			InstantiateSingletons();

			// 0x7FFE2A0B5700
			var numericalFilterAddress = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListNumericalFilter);
			NumericalFilterHook = Hook.Create<numericalFilter_Delegate>(numericalFilterAddress, OnNumericalFilter);

			TeaLog.Info("PlayerTypeLockBypass: Hook Initialization Done!");
		});

		return this;
	}

	private void OnNumericalFilter(nint steamInterface, nint keyAddress, int value, int comparison)
	{
		try
		{
			TeaLog.Info("OnNumericalFilter");

			//TeaLog.Info((keyAddress == null).ToString());

			var key = MemoryUtil.ReadString(keyAddress);

			TeaLog.Info($"{key} ({GetSearchKeyName(key)}) {GetComparisonSign(comparison)} {value}");

			if(key.Equals(Constants.SEARCH_KEY_SESSION_PLAYER_TYPE) && value == (int) PlayerTypes.Any && Customization.Enabled)
			{
				TeaLog.Info($"PlayerTypeLockBypass: Bypassing...");
				return;
			}
		}
		catch(Exception exception)
		{
			DebugManager_I.Report("PlayerTypeLockBypass.OnNumericalFilter()", exception.ToString());
		}

		NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
	}

	public void Dispose()
	{
		if(NumericalFilterHook == null) return;

		TeaLog.Info("PlayerTypeLockBypass: Disposing Hooks...");
		NumericalFilterHook.Dispose();
	}
}
