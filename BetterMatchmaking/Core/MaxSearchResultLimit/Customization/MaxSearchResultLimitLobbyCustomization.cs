using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class MaxSearchResultLimitLobbyCustomization : SingletonAccessor
{
	private bool enabled = true;
	public bool Enabled { get => enabled; set => enabled = value; }

	private int value = Constants.SEARCH_RESULT_LIMIT_MAX_SESSIONS;
	public int Value { get => value; set => this.value = value; }

	public int Max { get; set; } = Constants.SEARCH_RESULT_LIMIT_MAX_SESSIONS;

	public MaxSearchResultLimitLobbyCustomization() { }

	public MaxSearchResultLimitLobbyCustomization Init(int max = Constants.SEARCH_RESULT_LIMIT_MAX_SESSIONS)
	{
		Max = max;
		return this;
	}

	public bool RenderImGui(string title)
	{
		var changed = false;

		if (ImGui.TreeNode(title))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Enabled, ref enabled) || changed;
			changed = ImGui.SliderInt(localizationManager.ImGui.Value, ref value, 1, Max) || changed;

			ImGui.TreePop();

		}

		return changed;
	}
}
