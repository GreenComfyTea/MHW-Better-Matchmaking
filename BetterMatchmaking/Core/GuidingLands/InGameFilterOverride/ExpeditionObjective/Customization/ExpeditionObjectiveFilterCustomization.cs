using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public ExpeditionObjectiveFilterOptionCustomization FilterOptions { get; set; } = new();

	private ExpeditionObjectives _replacementTargetEnum = ExpeditionObjectives.FieldResearchForest;
	[JsonIgnore]
	public ExpeditionObjectives ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	public ExpeditionObjectiveFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.FieldResearchForest;
	}

	public ExpeditionObjectiveFilterCustomization Init()
	{
		var replacementTarget = ReplacementTarget;

		if(replacementTarget.Equals(LocalizationManager_I.Default.ImGui.NoPreference))
		{
			replacementTarget = LocalizationManager_I.Default.ImGui.None;
		}

		replacementTarget = replacementTarget.Replace(" ", "").Replace(":", "");
		var success = Enum.TryParse(replacementTarget, true, out _replacementTargetEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var expeditionObjectives = LocalizationManager_I.ImGui.ExpeditionObjectiveArray;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.ExpeditionObjective))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int) ReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, expeditionObjectives, expeditionObjectives.Length);

			if(tempChanged)
			{
				ReplacementTargetEnum = (ExpeditionObjectives) selectedIndex;
				ReplacementTarget = LocalizationManager_I.Default.ImGui.ExpeditionObjectiveArray[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
