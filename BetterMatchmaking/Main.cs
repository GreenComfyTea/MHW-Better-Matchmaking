using SharpPluginLoader.Core;
using ImGuiNET;
using System.Diagnostics;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;
using SharpPluginLoader.Core.Actions;
using SharpPluginLoader.Core.Entities;
using Microsoft.VisualBasic;
using System.IO;

namespace BetterMatchmaking;

internal class BetterMatchmakingPlugin : IPlugin
{
	public string Name => "{Constants.MOD_NAME} v{Constants.VERSION}";

	public string Author => "Constants.MOD_AUTHOR";

	public PluginData Initialize()
	{
		return new PluginData
		{
			ImGuiWrappedInTreeNode = false

		};
	}

	public async Task Init()
	{
		try
		{
			TeaLog.Info("Plugin Loaded!");

			var localizationManager = LocalizationManager.Instance;
			var configManager = ConfigManager.Instance;

			localizationManager.Init();
			configManager.Init();
		}
		catch (Exception exception)
		{
			//TeaLog.Error(exception.ToString());
		}
	}

	public void OnLoad()
	{
		_ = Init();
	}

	public void OnImGuiRender()
	{
		var font = ImGui.GetFont();
		var oldScale = font.Scale;
		font.Scale *= 1.5f;

		try
		{
			//ImGui.PushFont(font);

			//if (ImGui.Button($"{Constants.MOD_NAME} v{Constants.VERSION}"))
			//{
			//	CustomizationWindow.Instance.IsOpened = !CustomizationWindow.Instance.IsOpened;
			//}

			font.Scale = oldScale;
			ImGui.PopFont();
		}
		catch (Exception exception)
		{
			//TeaLog.Error(exception.ToString());

			font.Scale = oldScale;
			ImGui.PopFont();
		}
	}

	public void OnImGuiFreeRender()
	{
		try
		{
			//CustomizationWindow.Instance.Render();
		}
		catch (Exception exception)
		{
			//TeaLog.Error(exception.ToString());
		}
	}

	public void OnLobbySearch(ref int maxResults)
	{
		//TeaLog.Info("OnLobbySearch");

		maxResults = 32;
		//TeaLog.Info("Set Max Result to 32");


		Matchmaking.AddRequestLobbyListDistanceFilter(LobbyDistanceFilter.WorldWide);
		//Matchmaking.AddRequestLobbyListFilterSlotsAvailable(15);
		//TeaLog.Info("Set Distance to WorldWide");
	}
}