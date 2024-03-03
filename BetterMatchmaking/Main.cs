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
	private bool AreHooksInitialized { get; set; } = false;

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

			LocalizationManager_I.Init();
			ConfigManager_I.Init();
			CustomizationWindow_I.Init();

			IsInitialized = true;

			TeaLog.Info("Managers: Initialization Done!");
			return this;
		}
		catch (Exception exception)
		{
			DebugManager_I.Report("BetterMatchmakingPlugin.Init()", exception.ToString());
			return this;
		}
	}

	public void OnLoad()
	{
		_ = Task.Run(Init);
	}

	public void OnUnload()
	{
		LocalizationManager_I.Dispose();
		ConfigManager_I.Dispose();
		Core_I.Dispose();
		PlayerTypeFilterBypass_I.Dispose();
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
			DebugManager_I.Report("BetterMatchmakingPlugin.OnImGuiRender()", exception.ToString());
		}
	}

	public void OnImGuiFreeRender()
	{
		if (!IsInitialized) return;

		try
		{
			if(!AreHooksInitialized)
			{
				AreHooksInitialized = true;

				Task.Run(() =>
				{
					Core_I.Init();
					PlayerTypeFilterBypass_I.Init();
				});
			}

			CustomizationWindow_I.Render();
		}
		catch (Exception exception)
		{
			DebugManager_I.Report("BetterMatchmakingPlugin.OnImGuiFreeRender()", exception.ToString());
		}
	}
}