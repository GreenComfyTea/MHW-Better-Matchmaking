using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class LanguageFilterCustomization : SingletonAccessor
{
    private bool _enabled = true;
    public bool Enabled { get => _enabled; set => _enabled = value; }

    public string LanguageReplacementTarget { get; set; }

    public LanguageFilterOptionCustomization FilterOptions { get; set; } = new();

    private LanguageSearchTypes _languageReplacementTargetEnum = LanguageSearchTypes.SameLanguage;
    [JsonIgnore]
    public LanguageSearchTypes LanguageReplacementTargetEnum { get => _languageReplacementTargetEnum; set => _languageReplacementTargetEnum = value; }

    public LanguageFilterCustomization()
    {
        InstantiateSingletons();

        LanguageReplacementTarget = LocalizationManager_I.Default.ImGui.SameLanguage;
    }

    public LanguageFilterCustomization Init()
    {
        var replacementTarget = LanguageReplacementTarget.Replace(" ", "");
        var success = Enum.TryParse(replacementTarget, true, out _languageReplacementTargetEnum);
        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;
        var tempChanged = false;
        var selectedIndex = 0;

        var languageSearchTypes = LocalizationManager_I.ImGui.LanguageSearchTypeArray;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.Language))
        {
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

            selectedIndex = (int)LanguageReplacementTargetEnum;
            tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, languageSearchTypes, languageSearchTypes.Length);

            if (tempChanged)
            {
                LanguageReplacementTargetEnum = (LanguageSearchTypes)selectedIndex;
                LanguageReplacementTarget = LocalizationManager_I.Default.ImGui.LanguageSearchTypeArray[selectedIndex];
            }

            changed = changed || tempChanged;

            changed = FilterOptions.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
