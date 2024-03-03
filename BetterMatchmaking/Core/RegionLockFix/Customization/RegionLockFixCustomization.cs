using ImGuiNET;
using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class RegionLockFixCustomization : SingletonAccessor
{
	public RegionLockFixLobbyCustomization Sessions { get; set; } = new();
	public RegionLockFixLobbyCustomization Quests { get; set; } = new();

	public RegionLockFixCustomization()
	{
		InstantiateSingletons();
	}

	public RegionLockFixCustomization Init()
	{

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(LocalizationManagerInstance.ImGui.RegionLockFix))
		{
			changed = Sessions.RenderImGui(LocalizationManagerInstance.ImGui.Sessions) || changed;
			changed = Quests.RenderImGui(LocalizationManagerInstance.ImGui.Quests) || changed;

			if (ImGui.TreeNode(LocalizationManagerInstance.ImGui.Explanation))
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
