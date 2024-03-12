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

	public string RewardTypeReplacementTarget { get; set; }

	public RewardFilterOptionCustomization FilterOptions { get; set; } = new();

	private RewardTypes _rewardTypeReplacementTargetEnum = RewardTypes.NoPreference;
	[JsonIgnore]
	public RewardTypes RewardTypeReplacementTargetEnum { get => _rewardTypeReplacementTargetEnum; set => _rewardTypeReplacementTargetEnum = value; }

	public RewardFilterCustomization()
	{
		InstantiateSingletons();

		RewardTypeReplacementTarget = LocalizationManager_I.Default.ImGui.NoPreference;
	}

	public RewardFilterCustomization Init()
	{
		var replacementTarget = RewardTypeReplacementTarget.Replace(" ", "");
		var success = Enum.TryParse(replacementTarget, true, out _rewardTypeReplacementTargetEnum);

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

			selectedIndex = (int)RewardTypeReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, rewardReplacementTargets, rewardReplacementTargets.Length);

			if (tempChanged)
			{
				RewardTypeReplacementTargetEnum = (RewardTypes)selectedIndex;
				RewardTypeReplacementTarget = LocalizationManager_I.Default.ImGui.RewardReplacementTargets[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
