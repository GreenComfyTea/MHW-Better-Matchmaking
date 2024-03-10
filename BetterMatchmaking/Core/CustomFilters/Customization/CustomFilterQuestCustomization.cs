using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class CustomFilterQuestCustomization : SingletonAccessor
{
	public QuestTypeFilterCustomization QuestType { get; set; } = new();

	public DifficultyFilterCustomization Difficulty { get; set; } = new();

	public CustomFilterQuestCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;
		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Quests))
		{
			changed = QuestType.RenderImGui() || changed;
			changed = Difficulty.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
