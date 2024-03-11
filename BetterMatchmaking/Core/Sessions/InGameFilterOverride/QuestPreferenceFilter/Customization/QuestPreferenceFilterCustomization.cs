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
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string QuestPreferenceReplacementTarget { get; set; }

	public QuestPreferenceFilterOptionCustomization FilterOptions { get; set; } = new();

	private QuestPreferences _questPreferenceReplacementTargetEnum = QuestPreferences.SmallMonsters;
	[JsonIgnore]
	public QuestPreferences QuestPreferenceReplacementTargetEnum { get => _questPreferenceReplacementTargetEnum; set => _questPreferenceReplacementTargetEnum = value; }

	private int _replacementTargetSelectedIndex;
	[JsonIgnore]
	public int ReplacementTargetSelectedIndex { get => _replacementTargetSelectedIndex; set => _replacementTargetSelectedIndex = value; }

	public QuestPreferenceFilterCustomization()
	{
		InstantiateSingletons();

		QuestPreferenceReplacementTarget = LocalizationManager_I.Default.ImGui.SmallMonsters;
		ReplacementTargetSelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.QuestPreferenceArray, QuestPreferenceReplacementTarget);
	}

	public QuestPreferenceFilterCustomization Init()
	{
		ReplacementTargetSelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.QuestPreferenceArray, QuestPreferenceReplacementTarget);
		UpdateEnumFromString();

		return this;
	}

	private QuestPreferenceFilterCustomization UpdateEnumFromString()
	{
		var replacementTarget = QuestPreferenceReplacementTarget.Replace(" ", "").Replace("-", "").Replace("'", "");
		var success = Enum.TryParse(replacementTarget, out _questPreferenceReplacementTargetEnum);

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
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref _replacementTargetSelectedIndex, questPreferences, questPreferences.Length);

			if (tempChanged)
			{
				QuestPreferenceReplacementTarget = LocalizationManager_I.Default.ImGui.QuestPreferenceArray[ReplacementTargetSelectedIndex];
				UpdateEnumFromString();
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
