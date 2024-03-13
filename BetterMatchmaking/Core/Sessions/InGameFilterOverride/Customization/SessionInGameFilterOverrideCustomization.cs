using ImGuiNET;
using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;
internal class SessionInGameFilterOverrideCustomization : SingletonAccessor
{
	public PlayerTypeFilterCustomization PlayerType { get; set; } = new();

	public QuestPreferenceFilterCustomization QuestPreference { get; set; } = new();

	public LanguageFilterCustomization Language { get; set; } = new();

	public SessionInGameFilterOverrideCustomization()
	{
		InstantiateSingletons();
	}

	public SessionInGameFilterOverrideCustomization Init()
	{
		PlayerType.Init();
		QuestPreference.Init();
		Language.Init();

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		if(ImGui.TreeNode(LocalizationManager_I.ImGui.InGameFilterOverride))
		{
			changed = PlayerType.RenderImGui() || changed;
			changed = QuestPreference.RenderImGui() || changed;
			changed = Language.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
