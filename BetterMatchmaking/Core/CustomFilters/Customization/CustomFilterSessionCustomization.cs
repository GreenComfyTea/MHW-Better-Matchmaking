using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;

namespace BetterMatchmaking;

internal class CustomFilterSessionCustomization : SingletonAccessor
{
    public PlayerTypeFilterCustomization PlayerType { get; set; } = new();

    public QuestPreferenceFilterCustomization QuestPreference { get; set; } = new();

    public LanguageFilterCustomization Language { get; set; } = new();

    public CustomFilterSessionCustomization()
    {
        InstantiateSingletons();
    }

    public bool RenderImGui()
    {
        var changed = false;
        if (ImGui.TreeNode(LocalizationManager_I.ImGui.Sessions))
        {
            changed = PlayerType.RenderImGui() || changed;
            changed = QuestPreference.RenderImGui() || changed;
            changed = Language.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
