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

	private LobbyDistanceFilter _distanceFilterEnum = LobbyDistanceFilter.WorldWide;
	[JsonIgnore]
	public LobbyDistanceFilter DistanceFilterEnum { get => _distanceFilterEnum; set => _distanceFilterEnum = value; }

	public RegionLockFixLobbyCustomization()
	{
		InstantiateSingletons();

		DistanceFilter = LocalizationManager_I.Default.ImGui.Worldwide;
	}

	public RegionLockFixLobbyCustomization Init()
	{
		var success = Enum.TryParse(DistanceFilter, true, out _distanceFilterEnum);

		return this;
	}

	public bool RenderImGui(string title)
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var distanceFilters = LocalizationManager_I.ImGui.DistanceFilterArray;

		if (ImGui.TreeNode(title))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int) DistanceFilterEnum;
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.DistanceFilter, ref selectedIndex, distanceFilters, distanceFilters.Length);
			if (tempChanged)
			{
				DistanceFilterEnum = (LobbyDistanceFilter) selectedIndex;
				DistanceFilter = LocalizationManager_I.Default.ImGui.DistanceFilterArray[selectedIndex];
			}

			changed = changed || tempChanged;

			ImGui.TreePop();
		}

		return changed;
	}
}
