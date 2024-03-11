using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;

namespace BetterMatchmaking;

internal class SessionCustomization : SingletonAccessor
{
	public RegionLockFixCustomization RegionLockFix { get; set; } = new();

	public MaxSearchResultLimitCustomization MaxSearchResultLimit { get; set; } = new();

	public PlayerCountFilterCustomization PlayerCountFilter { get; set; } = new();

	public SessionInGameFilterOverride InGameFilterOverride { get; set; } = new();

	public SessionCustomization()
	{
		InstantiateSingletons();
	}

	public SessionCustomization Init()
	{
		RegionLockFix.Init();
		MaxSearchResultLimit.Init();
		PlayerCountFilter.Init();
		InGameFilterOverride.Init();

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Sessions))
		{
			changed = RegionLockFix.RenderImGui() || changed;
			changed = MaxSearchResultLimit.RenderImGui() || changed;
			changed = PlayerCountFilter.RenderImGui() || changed;
			changed = InGameFilterOverride.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
