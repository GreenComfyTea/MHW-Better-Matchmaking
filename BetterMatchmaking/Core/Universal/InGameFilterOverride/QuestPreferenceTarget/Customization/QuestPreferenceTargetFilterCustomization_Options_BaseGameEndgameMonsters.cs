using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BetterMatchmaking;
internal class QuestPreferenceTargetFilterCustomization_Options_BaseGameEndgameMonsters : SingletonAccessor
{
    private bool _kulveTaroth = true;
    public bool KulveTaroth { get => _kulveTaroth; set => _kulveTaroth = value; }

    private bool _deviljho = true;
    public bool Deviljho { get => _deviljho; set => _deviljho = value; }

    private bool _lunastra = true;
    public bool Lunastra { get => _lunastra; set => _lunastra = value; }

    private bool _behemoth = true;
    public bool Behemoth { get => _behemoth; set => _behemoth = value; }

    private bool _ancientLeshen = true;
    public bool AncientLeshen { get => _ancientLeshen; set => _ancientLeshen = value; }

    public QuestPreferenceTargetFilterCustomization_Options_BaseGameEndgameMonsters()
    {
        InstantiateSingletons();
    }

    public QuestPreferenceTargetFilterCustomization_Options_BaseGameEndgameMonsters SelectAll()
    {
        KulveTaroth = true;
        Deviljho = true;
        Lunastra = true;
        Behemoth = true;
        AncientLeshen = true;

        return this;
    }

    public QuestPreferenceTargetFilterCustomization_Options_BaseGameEndgameMonsters DeselectAll()
    {
        KulveTaroth = false;
        Deviljho = false;
        Lunastra = false;
        Behemoth = false;
        AncientLeshen = false;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.BaseGameEndgameMonsters))
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

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.KulveTaroth, ref _kulveTaroth) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Deviljho, ref _deviljho) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Lunastra, ref _lunastra) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Behemoth, ref _behemoth) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.AncientLeshen, ref _ancientLeshen) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}