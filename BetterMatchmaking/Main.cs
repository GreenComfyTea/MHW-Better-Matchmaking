using SharpPluginLoader.Core;
using ImGuiNET;
using System.Diagnostics;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using SharpPluginLoader.Core.Actions;
using SharpPluginLoader.Core.Entities;
using Microsoft.VisualBasic;
using System.IO;
using static SharpPluginLoader.Core.Components.CollisionComponent;
using System.Text.RegularExpressions;
using System;

namespace BetterMatchmaking;

internal class BetterMatchmakingPlugin : IPlugin
{
	public string Name => $"{Constants.MOD_NAME} v{Constants.VERSION}";

	public string Author => $"{Constants.MOD_AUTHOR}";

	private LocalizationManager localizationManager;
	private ConfigManager configManager;
	private RegionLockFix regionLockFix;
	private MaxSearchResultLimit maxSearchResultLimit;
	private SessionPlayerCountFilter sessionPlayerCountFilter;

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

	public void OnLoad()
	{
		Init();
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
			CustomizationWindow.Instance.Render();
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
		}
	}

	public void OnLobbySearch(ref int maxResults)
	{
		regionLockFix.Apply();
		maxSearchResultLimit.Apply(ref maxResults);
		sessionPlayerCountFilter.ApplyMin().ApplyMax();

		// (*(code*)(*steamApi_2)->vft->AddRequestLobbyListResultNumericalFilter(*steamApi_2, s_SlotPublicOpen_1434fd928, minOpenSlotsCount, 2);


	}
}