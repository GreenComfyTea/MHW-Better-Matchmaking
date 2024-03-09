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

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.MaxSearchResultLimit))
        {
            changed = Sessions.RenderImGui(LocalizationManager_I.ImGui.Sessions) || changed;
            changed = Quests.RenderImGui(LocalizationManager_I.ImGui.Quests) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
