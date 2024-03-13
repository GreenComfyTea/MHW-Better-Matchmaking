using ImGuiNET;
using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestInGameFilterOverrideCustomization : SingletonAccessor
{
	public QuestTypeFilterCustomization QuestType { get; set; } = new();

	public DifficultyFilterCustomization Difficulty { get; set; } = new();

	public RewardFilterCustomization Rewards { get; set; } = new();

	public LanguageFilterCustomization Language { get; set; } = new();

	public TargetFilterCustomization Target { get; set; } = new();

	public QuestInGameFilterOverrideCustomization()
	{
		InstantiateSingletons();
	}

	public QuestInGameFilterOverrideCustomization Init()
	{
		QuestType.Init();
		Difficulty.Init();
		Rewards.Init();
		Language.Init();
		Target.Init();

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		if(ImGui.TreeNode(LocalizationManager_I.ImGui.InGameFilterOverride))
		{
			changed = QuestType.RenderImGui() || changed;
			changed = Difficulty.RenderImGui() || changed;
			changed = Rewards.RenderImGui() || changed;
			changed = Language.RenderImGui() || changed;
			changed = Target.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
