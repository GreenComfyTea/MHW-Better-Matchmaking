using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class MaxSearchResultLimitCustomization : SingletonAccessor
{
	public MaxSearchResultLimitLobbyCustomization Sessions { get; set; } = new();
	public MaxSearchResultLimitLobbyCustomization Quests { get; set; } = new();

	public MaxSearchResultLimitCustomization() { }

	public MaxSearchResultLimitCustomization Init()
	{
		Sessions.Init();
		Quests.Init(Constants.SEARCH_RESULT_LIMIT_MAX_QUESTS);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(localizationManager.ImGui.MaxSearchResultLimit))
		{
			changed = Sessions.RenderImGui(localizationManager.ImGui.Sessions) || changed;
			changed = Quests.RenderImGui(localizationManager.ImGui.Quests) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
