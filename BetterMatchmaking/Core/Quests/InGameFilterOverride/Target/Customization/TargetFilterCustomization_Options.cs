using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetFilterCustomization_Options : SingletonAccessor
{
    public TargetFilterCustomization_Options_General General { get; set; } = new();
    public UniversalTargetFilterCustomization_Options_BaseGameMsqMonsters BaseGameMsqMonsters { get; set; } = new();
    public UniversalTargetFilterCustomization_Options_BaseGameEndgameMonsters BaseGameEndgameMonsters { get; set; } = new();
    public UniversalTargetFilterCustomization_Options_IceborneMsqMonsters IceborneMSQMonsters { get; set; } = new();
    public UniversalTargetFilterCustomization_Options_IceborneEndgameMonsters IceborneEndgameMonsters { get; set; } = new();

    public TargetFilterCustomization_Options()
    {
        InstantiateSingletons();
    }

    private TargetFilterCustomization_Options SelectAll()
    {
        General.SelectAll();
        BaseGameMsqMonsters.SelectAll();
        BaseGameEndgameMonsters.SelectAll();
        IceborneMSQMonsters.SelectAll();
        IceborneEndgameMonsters.SelectAll();

        return this;
    }

    private TargetFilterCustomization_Options DeselectAll()
    {
        General.DeselectAll();
        BaseGameMsqMonsters.DeselectAll();
        BaseGameEndgameMonsters.DeselectAll();
        IceborneMSQMonsters.DeselectAll();
        IceborneEndgameMonsters.DeselectAll();

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

            changed = General.RenderImGui() || changed;
            changed = BaseGameMsqMonsters.RenderImGui() || changed;
            changed = BaseGameEndgameMonsters.RenderImGui() || changed;
            changed = IceborneMSQMonsters.RenderImGui() || changed;
            changed = IceborneEndgameMonsters.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
