using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class RegionLevelFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public RegionLevelFilterOptionCustomization FilterOptions { get; set; } = new();

	private RegionLevels _replacementTargetEnum = RegionLevels.Level1;
	[JsonIgnore]
	public RegionLevels ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	public RegionLevelFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.Level1;
	}

	public RegionLevelFilterCustomization Init()
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

		var regionLevels = LocalizationManager_I.ImGui.RegionLevelArray;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.RegionLevel))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int) ReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, regionLevels, regionLevels.Length);

			if(tempChanged)
			{
				ReplacementTargetEnum = (RegionLevels) selectedIndex;
				ReplacementTarget = LocalizationManager_I.Default.ImGui.RegionLevelArray[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
