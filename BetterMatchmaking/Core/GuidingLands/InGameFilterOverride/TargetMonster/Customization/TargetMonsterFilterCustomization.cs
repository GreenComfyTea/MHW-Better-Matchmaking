using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetMonsterFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public TargetMonsterFilterCustomization_Options FilterOptions { get; set; } = new();

	private Targets _replacementTargetEnum = Targets.GreatJagras;
	[JsonIgnore]
	public Targets ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	private int _selectedIndex;
	[JsonIgnore]
	public int SelectedIndex { get => _selectedIndex; set => _selectedIndex = value; }

	public TargetMonsterFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.GreatJagras;
		SelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.TargetMonsterArray, ReplacementTarget);
	}

	public TargetMonsterFilterCustomization Init()
	{
		SelectedIndex = Array.IndexOf(LocalizationManager_I.Default.ImGui.TargetMonsterArray, ReplacementTarget);
		UpdateEnumFromString();

		return this;
	}

	private TargetMonsterFilterCustomization UpdateEnumFromString()
	{
		var replacementTarget = ReplacementTarget.Replace(" ", "").Replace("-", "").Replace("'", "");
		var success = Enum.TryParse(replacementTarget, out _replacementTargetEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		var targets = LocalizationManager_I.ImGui.TargetMonsterArray;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.TargetMonster))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref _selectedIndex, targets, targets.Length);

			if(tempChanged)
			{
				ReplacementTarget = LocalizationManager_I.Default.ImGui.TargetMonsterArray[SelectedIndex];
				UpdateEnumFromString();
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
