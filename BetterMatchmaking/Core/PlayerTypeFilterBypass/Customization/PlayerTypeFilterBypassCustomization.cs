using ImGuiNET;
using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Windows.Globalization;

namespace BetterMatchmaking;
internal class PlayerTypeFilterBypassCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public PlayerTypeFilterBypassCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.PlayerTypeFilterBypass))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			if(ImGui.TreeNode(LocalizationManager_I.ImGui.Explanation))
			{
				ImGui.Text("\"Any\" Player Type search filter returns sessions with Player Type set");
				ImGui.Text("to \"Any\", ignoring\"Beginners\" and \"Experienced\" sessions.");
				ImGui.Text("The bypass makes \"Any\" search return all sessions, regardless of the");
				ImGui.Text("session's Player Type setting.");

				ImGui.NewLine();
				ImGui.Text("Vanilla:");
				ImGui.NewLine();

				// Beginners Search

				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Success");

				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Failure");

				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Failure");

				ImGui.NewLine();

				// Experienced Search

				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Failure");

				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Success");

				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Failure");

				ImGui.NewLine();

				// Any Search

				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "Failure");

				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "Failure");

				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Success");

				ImGui.NewLine();
				ImGui.Text("With Player Type Filter Bypass:");
				ImGui.NewLine();


				// Any Search

				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_YELLOW_COLOR, "\"Beginners\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Success");

				ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, "\"Any\"");
				ImGui.SameLine();
				ImGui.Text("Search");
				ImGui.SameLine();
				ImGui.Text("+");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_PURPLE_COLOR, "\"Experienced\"");
				ImGui.SameLine();
				ImGui.Text("Lobby");
				ImGui.SameLine();
				ImGui.Text("=");
				ImGui.SameLine();
				ImGui.TextColored(Constants.IMGUI_GREEN_COLOR, "Success");

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return changed;
	}
}
