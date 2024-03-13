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
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public TargetFilterCustomization_Options FilterOptions { get; set; } = new();

	private Targets _replacementTargetEnum = Targets.SmallMonsters;
	[JsonIgnore]
	public Targets ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	private int _selectedIndex;
	[JsonIgnore]
	public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

	public TargetFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.SmallMonsters;
		SelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.TargetArray, ReplacementTarget);
	}

	public TargetFilterCustomization Init()
	{
		SelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.TargetArray, ReplacementTarget);
		UpdateEnumFromString();

		return this;
	}

	private TargetFilterCustomization UpdateEnumFromString()
	{
		var replacementTarget = ReplacementTarget.Replace(" ", "").Replace("-", "").Replace("'", "");
		var success = Enum.TryParse(replacementTarget, out _replacementTargetEnum);

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
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref _selectedIndex, targets, targets.Length);

			if (tempChanged)
			{
				ReplacementTarget = LocalizationManager_I.Default.ImGui.TargetArray[SelectedIndex];
				UpdateEnumFromString();
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
