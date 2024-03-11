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
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	private int _value = Constants.SEARCH_RESULT_LIMIT_MAX;
	public int Value { get => _value; set => _value = value; }

	public MaxSearchResultLimitCustomization()
	{
		InstantiateSingletons();
	}

	public MaxSearchResultLimitCustomization Init()
	{
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.MaxSearchResultLimit))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;
			changed = ImGui.SliderInt(LocalizationManager_I.ImGui.Value, ref _value, 1, Constants.SEARCH_RESULT_LIMIT_MAX) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
