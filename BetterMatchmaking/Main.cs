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
using System.Runtime.CompilerServices;

namespace BetterMatchmaking;

internal class BetterMatchmakingPlugin : SingletonAccessor, IPlugin
{
	public string Name => $"{Constants.MOD_NAME} v{Constants.VERSION}";

	public string Author => Constants.MOD_AUTHOR;

	private bool IsInitialized { get; set; } = false;

	public PluginData Initialize()
	{
		return new PluginData
		{
			ImGuiWrappedInTreeNode = false
		};
	}

	public BetterMatchmakingPlugin Init()
	{
		try
		{
			TeaLog.Info("Managers: Initializing...");

			InstantiateSingletons();

			LocalizationManagerInstance = LocalizationManager.Instance;
			ConfigManagerInstance = ConfigManager.Instance;
			RegionLockFixInstance = RegionLockFix.Instance;
			MaxSearchResultLimitInstance = MaxSearchResultLimit.Instance;
			SessionPlayerCountFilterInstance = SessionPlayerCountFilter.Instance;
			CoreInstance = Core.Instance;
			CustomizationWindowInstance = CustomizationWindow.Instance;

			LocalizationManagerInstance.Init();
			ConfigManagerInstance.Init();
			CustomizationWindowInstance.Init();

			IsInitialized = true;

			TeaLog.Info("Managers: Initialization Done!");
			return this;
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
			return this;
		}
	}

	public void OnLoad()
	{
		_ = Task.Run(Init);
	}

	public void OnUnload()
	{
		LocalizationManagerInstance.Dispose();
		ConfigManagerInstance.Dispose();
		CoreInstance.Dispose();
	}

	public void OnImGuiRender()
	{
		if (!IsInitialized) return;

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
		if (!IsInitialized) return;

		try
		{
			if (IsInitialized && !CoreInstance.AreHooksInitialized) CoreInstance.Init();
			CustomizationWindowInstance.Render();
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