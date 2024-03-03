using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class RegionLockFixLobbyCustomization : SingletonAccessor
{
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string DistanceFilter { get; set; }

	[JsonIgnore]
	public LobbyDistanceFilter DistanceFilterEnum { get; set; } = LobbyDistanceFilter.WorldWide;

	public RegionLockFixLobbyCustomization()
	{
		InstantiateSingletons();
		DistanceFilter = LocalizationManager_I.Default.ImGui.Worldwide;
	}

	public RegionLockFixLobbyCustomization Init()
	{
	
		DistanceFilterEnum = (LobbyDistanceFilter)Array.FindIndex(
			LocalizationManager.Instance.Default.ImGui.DistanceFilters, arrayString => arrayString.Equals(DistanceFilter)
		);

		return this;
	}

	public bool RenderImGui(string title)
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		if (ImGui.TreeNode(title))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int)DistanceFilterEnum;
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.DistanceFilter, ref selectedIndex, LocalizationManager_I.ImGui.DistanceFilters, 4);
			if (tempChanged)
			{
				DistanceFilterEnum = (LobbyDistanceFilter)selectedIndex;
				DistanceFilter = LocalizationManager_I.Default.ImGui.DistanceFilters[selectedIndex];
			}

			changed = changed || tempChanged;

			ImGui.TreePop();
		}

		return changed;
	}
}
