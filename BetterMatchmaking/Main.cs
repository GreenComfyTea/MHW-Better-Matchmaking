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

	private bool IsInitialized { get; set; } = false;

	private Core CoreInstance { get; set; }

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
			TeaLog.Info("Managers: Initializing...");

			SteamApi.Init();

			var localizationManager = LocalizationManager.Instance;
			var configManager = ConfigManager.Instance;
			var regionLockFix = RegionLockFix.Instance;
			var maxSearchResultLimit = MaxSearchResultLimit.Instance;
			var sessionPlayerCountFilter = SessionPlayerCountFilter.Instance;
			CoreInstance = Core.Instance;

			localizationManager.Init();
			configManager.Init();

			IsInitialized = true;

			TeaLog.Info("Managers: Initialization Done!");
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
		}
	}

	public void OnLoad()
	{
		Task.Run(Init);
	}

	public void OnUnload()
	{
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