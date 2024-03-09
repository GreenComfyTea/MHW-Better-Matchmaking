using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class CustomFilterQuestCustomization : SingletonAccessor
{
	public CustomFilterQuestCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;
		if(ImGui.TreeNode(LocalizationManager_I.ImGui.Quests))
		{

			ImGui.TreePop();
		}

		return changed;
	}
}
