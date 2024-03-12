using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetFilterOptionCustomization_General : SingletonAccessor
{
    private bool _none = true;
    public bool None { get => _none; set => _none = value; }

    private bool _smallMonsters = true;
    public bool SmallMonsters { get => _smallMonsters; set => _smallMonsters = value; }

    public TargetFilterOptionCustomization_General()
    {
        InstantiateSingletons();
    }

    public TargetFilterOptionCustomization_General SelectAll()
    {
        None = true;
        SmallMonsters = true;

        return this;
    }

    public TargetFilterOptionCustomization_General DeselectAll()
    {
        None = false;
        SmallMonsters = false;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.General))
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

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.None, ref _none) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SmallMonsters, ref _smallMonsters) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
