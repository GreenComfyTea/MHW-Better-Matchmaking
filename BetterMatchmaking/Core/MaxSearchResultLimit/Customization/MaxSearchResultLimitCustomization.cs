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

	public MaxSearchResultLimitCustomization()
	{
		InstantiateSingletons();
	}

	public MaxSearchResultLimitCustomization Init()
	{
		InstantiateSingletons();

		Sessions.Init();
		Quests.Init(Constants.SEARCH_RESULT_LIMIT_MAX_QUESTS);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(LocalizationManagerInstance.ImGui.MaxSearchResultLimit))
		{
			changed = Sessions.RenderImGui(LocalizationManagerInstance.ImGui.Sessions) || changed;
			changed = Quests.RenderImGui(LocalizationManagerInstance.ImGui.Quests) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
