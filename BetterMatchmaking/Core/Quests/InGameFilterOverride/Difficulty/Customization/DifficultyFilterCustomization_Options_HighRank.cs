using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class DifficultyFilterCustomization_Options_HighRank : SingletonAccessor
{
    private bool _highRank6 = true;
    public bool HighRank6 { get => _highRank6; set => _highRank6 = value; }

    private bool _highRank7 = true;
    public bool HighRank7 { get => _highRank7; set => _highRank7 = value; }

    private bool _highRank8 = true;
    public bool HighRank8 { get => _highRank8; set => _highRank8 = value; }

    private bool _highRank9 = true;
    public bool HighRank9 { get => _highRank9; set => _highRank9 = value; }

    public DifficultyFilterCustomization_Options_HighRank()
    {
        InstantiateSingletons();
    }

    public DifficultyFilterCustomization_Options_HighRank SelectAll()
    {
        HighRank6 = true;
        HighRank7 = true;
        HighRank8 = true;
        HighRank9 = true;

        return this;
    }

    public DifficultyFilterCustomization_Options_HighRank DeselectAll()
    {
        HighRank6 = false;
        HighRank7 = false;
        HighRank8 = false;
        HighRank9 = false;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.HighRank))
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

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.HighRank6, ref _highRank6) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.HighRank7, ref _highRank7) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.HighRank8, ref _highRank8) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.HighRank9, ref _highRank9) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
