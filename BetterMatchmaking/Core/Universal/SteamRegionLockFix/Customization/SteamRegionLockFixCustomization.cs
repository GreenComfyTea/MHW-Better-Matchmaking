using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class SteamRegionLockFixCustomization : SingletonAccessor
{
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string DistanceFilter { get; set; }

	private LobbyDistanceFilter _distanceFilterEnum = LobbyDistanceFilter.WorldWide;
	[JsonIgnore]
	public LobbyDistanceFilter DistanceFilterEnum { get => _distanceFilterEnum; set => _distanceFilterEnum = value; }

	public SteamRegionLockFixCustomization()
	{
		InstantiateSingletons();

		DistanceFilter = LocalizationManager_I.Default.ImGui.Worldwide;
	}

	public SteamRegionLockFixCustomization Init()
	{
		var success = Enum.TryParse(DistanceFilter, true, out _distanceFilterEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var distanceFilters = LocalizationManager_I.ImGui.DistanceFilterArray;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.SteamRegionLockFix))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int) DistanceFilterEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.DistanceFilter, ref selectedIndex, distanceFilters, distanceFilters.Length);

			if (tempChanged)
			{
				DistanceFilterEnum = (LobbyDistanceFilter) selectedIndex;
				DistanceFilter = LocalizationManager_I.Default.ImGui.DistanceFilterArray[selectedIndex];
			}

			changed = changed || tempChanged;

			if(ImGui.TreeNode(LocalizationManager_I.ImGui.Explanation))
			{
				ImGui.TextColored(Constants.IMGUI_RED_COLOR, "Close");
				ImGui.SameLine();
				ImGui.Text("-");
				ImGui.SameLine();
				ImGui.Text("Only sessions in the same immediate region will be returned.");

				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "Default");
				ImGui.SameLine();
				ImGui.Text("-");
				ImGui.SameLine();
				ImGui.Text("Only sessions in the same region or nearby regions will be returned.");

				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "Far");
				ImGui.SameLine();
				ImGui.Text("-");
				ImGui.SameLine();
				ImGui.Text("Will return sessions about half-way around the globe.");

				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Worldwide");
				ImGui.SameLine();
				ImGui.Text("-");
				ImGui.SameLine();
				ImGui.Text("No filtering, will match sessions as far as India to NY");
				ImGui.Text("(not recommended, expect multiple seconds of latency between the clients).");

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return changed;
	}
}
