using ImGuiNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class CustomFilterCustomization : SingletonAccessor
{
	public CustomFilterSessionCustomization Sessions { get; set; } = new();

	public CustomFilterQuestCustomization Quests { get; set; } = new();

	public CustomFilterCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;
		if(ImGui.TreeNode(LocalizationManager_I.ImGui.CustomFilters))
		{

			changed = Sessions.RenderImGui() || changed;
			changed = Quests.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
