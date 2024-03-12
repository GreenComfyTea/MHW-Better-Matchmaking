using ImGuiNET;
using SharpPluginLoader.Core;
using System.Diagnostics;

namespace BetterMatchmaking;

internal class BetterMatchmakingPlugin : SingletonAccessor, IPlugin
{
	public string Name => $"{Constants.MOD_NAME} v{Constants.VERSION}";

	public string Author => Constants.MOD_AUTHOR;

	private bool IsInitialized { get; set; } = false;
	private bool AreHooksInitialized { get; set; } = false;

	private bool IsImGuiRenderingEnabled { get; set; } = false;

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

			PlayerTypeFilter_I.Init();
			QuestPreferenceFilter_I.Init();
			LanguageFilter_I.Init();
			QuestPreferenceTargetFilter_I.Init();

			QuestTypeFilter_I.Init();
			DifficultyFilter_I.Init();
			RewardFilter_I.Init();
			TargetFilter_I.Init();

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
	}

	public int count = 0;

	public void OnImGuiRender()
	{
		if (!IsInitialized) return;

		IsImGuiRenderingEnabled = true;

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
		try
		{
			if(!IsInitialized) return;

			if(!AreHooksInitialized)
			{
				AreHooksInitialized = true;
				Task.Run(Core_I.Init);
			}

			if(!IsImGuiRenderingEnabled) return;

			CustomizationWindow_I.Render();
		}
		catch (Exception exception)
		{
			DebugManager_I.Report("BetterMatchmakingPlugin.OnImGuiFreeRender()", exception.ToString());
		}

		IsImGuiRenderingEnabled = false;
	}
}