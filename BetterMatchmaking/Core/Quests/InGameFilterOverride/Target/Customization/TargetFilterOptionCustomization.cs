using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetFilterOptionCustomization : SingletonAccessor
{
    public TargetFilterOptionCustomization_General General { get; set; } = new();
    public QuestPreferenceTargetFilterOptionCustomization_BaseGameMsqMonsters BaseGameMSQMonsters { get; set; } = new();
    public QuestPreferenceTargetFilterOptionCustomization_BaseGameEndgameMonsters BaseGameEndgameMonsters { get; set; } = new();
    public QuestPreferenceTargetFilterOptionCustomization_IceborneMSQMonsters IceborneMSQMonsters { get; set; } = new();
    public QuestPreferenceTargetFilterOptionCustomization_IceborneEndgameMonsters IceborneEndgameMonsters { get; set; } = new();

    public TargetFilterOptionCustomization()
    {
        InstantiateSingletons();
    }

    private TargetFilterOptionCustomization SelectAll()
    {
        General.SelectAll();
        BaseGameMSQMonsters.SelectAll();
        BaseGameEndgameMonsters.SelectAll();
        IceborneMSQMonsters.SelectAll();
        IceborneEndgameMonsters.SelectAll();

        return this;
    }

    private TargetFilterOptionCustomization DeselectAll()
    {
        General.DeselectAll();
        BaseGameMSQMonsters.DeselectAll();
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
            changed = BaseGameMSQMonsters.RenderImGui() || changed;
            changed = BaseGameEndgameMonsters.RenderImGui() || changed;
            changed = IceborneMSQMonsters.RenderImGui() || changed;
            changed = IceborneEndgameMonsters.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
