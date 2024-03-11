using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class PlayerTypeFilterOptionCustomization : SingletonAccessor
{
    private bool _beginners = true;
    public bool Beginners { get => _beginners; set => _beginners = value; }

    private bool _experienced = true;
    public bool Experienced { get => _experienced; set => _experienced = value; }

    private bool _any = true;
    public bool Any { get => _any; set => _any = value; }

    public PlayerTypeFilterOptionCustomization()
    {
        InstantiateSingletons();
    }

    private PlayerTypeFilterOptionCustomization SelectAll()
    {
        Beginners = true;
        Experienced = true;
        Any = true;

        return this;
    }

    private PlayerTypeFilterOptionCustomization DeselectAll()
    {
        Beginners = false;
        Experienced = false;
        Any = false;

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

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Beginners, ref _beginners) || changed;
            ImGui.SameLine();
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Experienced, ref _experienced) || changed;
            ImGui.SameLine();
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Any, ref _any) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
