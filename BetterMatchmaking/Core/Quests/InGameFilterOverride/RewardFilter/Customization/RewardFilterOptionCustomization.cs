using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class RewardFilterOptionCustomization : SingletonAccessor
{
    private bool _noRewards = true;
    public bool NoRewards { get => _noRewards; set => _noRewards = value; }

    private bool _rewardsAvailable = true;
    public bool RewardsAvailable { get => _rewardsAvailable; set => _rewardsAvailable = value; }

    public RewardFilterOptionCustomization()
    {
        InstantiateSingletons();
    }

    private RewardFilterOptionCustomization SelectAll()
    {
        NoRewards = true;
        RewardsAvailable = true;

        return this;
    }

    private RewardFilterOptionCustomization DeselectAll()
    {
        NoRewards = false;
        RewardsAvailable = false;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.FilterOptions))
        {
            if (ImGui.Button(LocalizationManager_I.ImGui.SelectAll))
            {
                SelectAll();
                changed = true;
            }

            ImGui.SameLine();

            if (ImGui.Button(LocalizationManager_I.ImGui.DeselectAll))
            {
                DeselectAll();
                changed = true;
            }

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.NoRewards, ref _noRewards) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.RewardsAvailable, ref _rewardsAvailable) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
