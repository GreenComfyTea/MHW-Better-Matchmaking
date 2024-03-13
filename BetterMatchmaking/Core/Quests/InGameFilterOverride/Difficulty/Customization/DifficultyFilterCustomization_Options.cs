using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class DifficultyFilterCustomization_Options : SingletonAccessor
{
    public DifficultyFilterCustomization_Options_LowRank LowRank { get; set; } = new();
    public DifficultyFilterCustomization_Options_HighRank HighRank { get; set; } = new();
    public DifficultyFilterCustomization_Options_MasterRank MasterRank { get; set; } = new();

    public DifficultyFilterCustomization_Options()
    {
        InstantiateSingletons();
    }

    private DifficultyFilterCustomization_Options SelectAll()
    {
        LowRank.SelectAll();
        HighRank.SelectAll();
        MasterRank.SelectAll();

        return this;
    }

    private DifficultyFilterCustomization_Options DeselectAll()
    {
        LowRank.DeselectAll();
        HighRank.DeselectAll();
        MasterRank.DeselectAll();

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.FilterOptions))
        {
            if (ImGui.Button(LocalizationManager_I.ImGui.SelectAll))
            {
                SelectAll();
                changed = true;
            }

            ImGui.SameLine();

            if (ImGui.Button(LocalizationManager_I.ImGui.DeselectAll))
            {
                DeselectAll();
                changed = true;
            }

            changed = LowRank.RenderImGui() || changed;
            changed = HighRank.RenderImGui() || changed;
            changed = MasterRank.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
