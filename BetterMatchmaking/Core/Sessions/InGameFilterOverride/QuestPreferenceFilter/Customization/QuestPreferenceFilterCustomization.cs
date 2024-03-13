using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestPreferenceFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public QuestPreferenceFilterCustomization_Options FilterOptions { get; set; } = new();

	private Targets _replacementTargetEnum = Targets.SmallMonsters;
	[JsonIgnore]
	public Targets ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	private int _selectedIndex;
	[JsonIgnore]
	public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

	public QuestPreferenceFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.SmallMonsters;
		SelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.QuestPreferenceArray, ReplacementTarget);
	}

	public QuestPreferenceFilterCustomization Init()
	{
		SelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.QuestPreferenceArray, ReplacementTarget);
		UpdateEnumFromString();

		return this;
	}

	private QuestPreferenceFilterCustomization UpdateEnumFromString()
	{
		var replacementTarget = ReplacementTarget.Replace(" ", "").Replace("-", "").Replace("'", "");
		var success = Enum.TryParse(replacementTarget, out _replacementTargetEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		var questPreferences = LocalizationManager_I.ImGui.QuestPreferenceArray;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.QuestPreference))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref _selectedIndex, questPreferences, questPreferences.Length);

			if (tempChanged)
			{
				ReplacementTarget = LocalizationManager_I.Default.ImGui.QuestPreferenceArray[SelectedIndex];
				UpdateEnumFromString();
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
