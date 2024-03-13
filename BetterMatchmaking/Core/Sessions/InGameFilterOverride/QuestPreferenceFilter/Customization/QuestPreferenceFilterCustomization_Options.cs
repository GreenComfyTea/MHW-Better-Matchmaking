using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestPreferenceFilterCustomization_Options : SingletonAccessor
{
    public QuestPreferenceFilterCustomization_Options_General General { get; set; } = new();
    public QuestPreferenceTargetFilterCustomization_Options_BaseGameMsqMonsters BaseGameMSQMonsters { get; set; } = new();
    public QuestPreferenceTargetFilterCustomization_Options_BaseGameEndgameMonsters BaseGameEndgameMonsters { get; set; } = new();
    public QuestPreferenceTargetFilterCustomization_Options_IceborneMSQMonsters IceborneMSQMonsters { get; set; } = new();
    public QuestPreferenceTargetFilterCustomization_Options_IceborneEndgameMonsters IceborneEndgameMonsters { get; set; } = new();

    public QuestPreferenceFilterCustomization_Options()
    {
        InstantiateSingletons();
    }

    private QuestPreferenceFilterCustomization_Options SelectAll()
    {
        General.SelectAll();
        BaseGameMSQMonsters.SelectAll();
        BaseGameEndgameMonsters.SelectAll();
        IceborneMSQMonsters.SelectAll();
        IceborneEndgameMonsters.SelectAll();

        return this;
    }

    private QuestPreferenceFilterCustomization_Options DeselectAll()
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
