using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class GuidingLandsCustomization : SingletonAccessor
{
	public RegionLockFixCustomization RegionLockFix { get; set; } = new();
	public MaxSearchResultLimitCustomization MaxSearchResultLimit { get; set; } = new();
	public GuidingLandsInGameFilterOverrideCustomization InGameFilterOverride { get; set; } = new();

	public GuidingLandsCustomization()
	{
		InstantiateSingletons();
	}

	public GuidingLandsCustomization Init()
	{
		RegionLockFix.Init();
		MaxSearchResultLimit.Init();
		InGameFilterOverride.Init();

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		if(ImGui.TreeNode(LocalizationManager_I.ImGui.GuidingLands))
		{
			changed = RegionLockFix.RenderImGui() || changed;
			changed = MaxSearchResultLimit.RenderImGui() || changed;
			changed = InGameFilterOverride.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
