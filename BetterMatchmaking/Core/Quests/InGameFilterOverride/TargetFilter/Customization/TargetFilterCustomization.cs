using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetFilterCustomization : SingletonAccessor
{
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string TargetReplacementTarget { get; set; }

	public TargetFilterOptionCustomization FilterOptions { get; set; } = new();

	private QuestPreferences _targetReplacementTargetEnum = QuestPreferences.SmallMonsters;
	[JsonIgnore]
	public QuestPreferences TargetReplacementTargetEnum { get => _targetReplacementTargetEnum; set => _targetReplacementTargetEnum = value; }

	private int _replacementTargetSelectedIndex;
	[JsonIgnore]
	public int ReplacementTargetSelectedIndex { get => _replacementTargetSelectedIndex; set => _replacementTargetSelectedIndex = value; }

	public TargetFilterCustomization()
	{
		InstantiateSingletons();

		TargetReplacementTarget = LocalizationManager_I.Default.ImGui.SmallMonsters;
		ReplacementTargetSelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.TargetArray, TargetReplacementTarget);
	}

	public TargetFilterCustomization Init()
	{
		ReplacementTargetSelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.TargetArray, TargetReplacementTarget);
		UpdateEnumFromString();

		return this;
	}

	private TargetFilterCustomization UpdateEnumFromString()
	{
		var replacementTarget = TargetReplacementTarget.Replace(" ", "").Replace("-", "").Replace("'", "");
		var success = Enum.TryParse(replacementTarget, out _targetReplacementTargetEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		var targets = LocalizationManager_I.ImGui.TargetArray;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Target))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref _replacementTargetSelectedIndex, targets, targets.Length);

			if (tempChanged)
			{
				TargetReplacementTarget = LocalizationManager_I.Default.ImGui.TargetArray[ReplacementTargetSelectedIndex];
				UpdateEnumFromString();
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
