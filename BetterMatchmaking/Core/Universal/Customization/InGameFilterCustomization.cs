using ImGuiNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class InGameFilterCustomization : SingletonAccessor
{
    public SessionCustomization Sessions { get; set; } = new();

    public QuestCustomization Quests { get; set; } = new();

    public InGameFilterCustomization()
    {
        InstantiateSingletons();
    }

    public bool RenderImGui()
    {
        var changed = false;
        if (ImGui.TreeNode(LocalizationManager_I.ImGui.InGameFilterOverride))
        {
            changed = Sessions.RenderImGui() || changed;
            changed = Quests.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
