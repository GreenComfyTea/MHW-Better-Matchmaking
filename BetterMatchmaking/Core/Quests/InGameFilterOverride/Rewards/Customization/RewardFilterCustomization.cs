using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace BetterMatchmaking;

internal class RewardFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public RewardFilterCustomization_Options FilterOptions { get; set; } = new();

	private RewardTypes _replacementTargetEnum = RewardTypes.NoPreference;
	[JsonIgnore]
	public RewardTypes ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	public RewardFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.NoPreference;
	}

	public RewardFilterCustomization Init()
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

		var rewardReplacementTargets = LocalizationManager_I.ImGui.RewardReplacementTargets;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Rewards))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int)ReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, rewardReplacementTargets, rewardReplacementTargets.Length);

			if (tempChanged)
			{
				ReplacementTargetEnum = (RewardTypes)selectedIndex;
				ReplacementTarget = LocalizationManager_I.Default.ImGui.RewardReplacementTargets[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
