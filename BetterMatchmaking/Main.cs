using ImGuiNET;
using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Rendering;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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

			LocalizationManager_I.Init();
			ConfigManager_I.Init();
			CustomizationWindow_I.Init();

			UniversalTargetFilter_I.Init();

			PlayerTypeFilter_I.Init();
			QuestPreferenceFilter_I.Init();
			LanguageFilter_I.Init();


			QuestTypeFilter_I.Init();
			DifficultyFilter_I.Init();
			RewardFilter_I.Init();
			TargetFilter_I.Init();

			ExpeditionObjectiveFilter_I.Init();
			RegionLevelFilter_I.Init();
			TargetMonsterFilter_I.Init();
			Core_I.Init();

			FontManager_I.Init();

			ConfigManager_I.Current.Save();

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
		try
		{
			if(!IsInitialized) return;
			if(!Renderer.MenuShown) return;

			CustomizationWindow_I.Render();
		}
		catch (Exception exception)
		{
			DebugManager_I.Report("BetterMatchmakingPlugin.OnImGuiFreeRender()", exception.ToString());
		}
	}
}