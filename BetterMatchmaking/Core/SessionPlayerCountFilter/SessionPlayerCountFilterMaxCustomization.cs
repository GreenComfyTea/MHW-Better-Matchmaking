using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class SessionPlayerCountFilterMaxCustomization : SingletonAccessor
{
	private bool enabled = true;
	public bool Enabled { get => enabled; set => enabled = value; }

	private int value = 15;
	public int Value { get => value; set => this.value = value; }

	[JsonIgnore]
	public int SliderMin { get; set; } = Constants.DEFAULT_SESSION_PLAYER_COUNT_MIN;

	public SessionPlayerCountFilterMaxCustomization() { }

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Max))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Enabled, ref enabled) || changed;
			tempChanged = ImGui.SliderInt(localizationManager.ImGui.Value, ref value, SliderMin, 15);

			if (tempChanged)
			{
				changed = true;
				var min = sessionPlayerCountFilter.Customization.Min;

				min.SliderMax = Value;

				if (min.Value > Value)
				{
					min.Value = value;
				}
			}

			ImGui.TreePop();

		}

		return changed;
	}
}