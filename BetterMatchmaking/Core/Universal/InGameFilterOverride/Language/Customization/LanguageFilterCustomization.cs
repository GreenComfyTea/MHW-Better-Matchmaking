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
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public LanguageFilterCustomization_Options FilterOptions { get; set; } = new();

	private LanguageSearchTypes _replacementTargetEnum = LanguageSearchTypes.SameLanguage;
	[JsonIgnore]
	public LanguageSearchTypes ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	public LanguageFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.SameLanguage;
	}

	public LanguageFilterCustomization Init()
	{
		var replacementTarget = ReplacementTarget.Replace(" ", "");
		var success = Enum.TryParse(replacementTarget, true, out _replacementTargetEnum);
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

			selectedIndex = (int)ReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, languageSearchTypes, languageSearchTypes.Length);

			if (tempChanged)
			{
				ReplacementTargetEnum = (LanguageSearchTypes)selectedIndex;
				ReplacementTarget = LocalizationManager_I.Default.ImGui.LanguageSearchTypeArray[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
