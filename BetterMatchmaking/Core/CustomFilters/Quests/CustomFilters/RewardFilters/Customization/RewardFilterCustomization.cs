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
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public RewardFilterOptionCustomization FilterOptions { get; set; } = new();

	public RewardFilterCustomization()
	{
		InstantiateSingletons();
	}

	public RewardFilterCustomization Init()
	{
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.Rewards))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			ImGui.Text($"{LocalizationManager_I.ImGui.ReplacementTarget}:");
			ImGui.SameLine();
			ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, LocalizationManager_I.ImGui.RewardsAvailable);

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
