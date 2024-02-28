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

internal class MaxSearchResultLimitCustomization : SingletonAccessor
{
	private bool enabled = true;
	public bool Enabled { get => enabled; set => enabled = value; }

	private int value = 32;
	public int Value { get => value; set => this.value = value; }

	public MaxSearchResultLimitCustomization() { }

	public MaxSearchResultLimitCustomization Init()
	{
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(localizationManager.ImGui.MaxSearchResultLimit))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Enabled, ref enabled) || changed;
			changed = ImGui.SliderInt(localizationManager.ImGui.Value, ref value, 1, 32) || changed;

			ImGui.TreePop();

		}

		return changed;
	}
}
