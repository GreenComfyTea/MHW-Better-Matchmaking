using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class DifficultyFilterCustomization_Options_LowRank : SingletonAccessor
{
    private bool _lowRank1 = true;
    public bool LowRank1 { get => _lowRank1; set => _lowRank1 = value; }

    private bool _lowRank2 = true;
    public bool LowRank2 { get => _lowRank2; set => _lowRank2 = value; }

    private bool _lowRank3 = true;
    public bool LowRank3 { get => _lowRank3; set => _lowRank3 = value; }

    private bool _lowRank4 = true;
    public bool LowRank4 { get => _lowRank4; set => _lowRank4 = value; }

    private bool _lowRank5 = true;
    public bool LowRank5 { get => _lowRank5; set => _lowRank5 = value; }

    private bool _highRank6 = true;
    public bool HighRank6 { get => _highRank6; set => _highRank6 = value; }

    public DifficultyFilterCustomization_Options_LowRank()
    {
        InstantiateSingletons();
    }

    public DifficultyFilterCustomization_Options_LowRank SelectAll()
    {
        LowRank1 = true;
        LowRank2 = true;
        LowRank3 = true;
        LowRank4 = true;
        LowRank5 = true;

        return this;
    }

    public DifficultyFilterCustomization_Options_LowRank DeselectAll()
    {
        LowRank1 = false;
        LowRank2 = false;
        LowRank3 = false;
        LowRank4 = false;
        LowRank5 = false;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.LowRank))
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

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.LowRank1Star, ref _lowRank1) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.LowRank2Star, ref _lowRank2) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.LowRank3Star, ref _lowRank3) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.LowRank4Star, ref _lowRank4) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.LowRank5Star, ref _lowRank5) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
