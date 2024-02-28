using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class SessionPlayerCountFilterMinCustomization : SingletonAccessor
{
	private bool enabled = true;
	public bool Enabled { get => enabled; set => enabled = value; }

	private int value = 1;
	public int Value { get => value; set => this.value = value; }

	[JsonIgnore]
	public int SliderMax { get; set; } = Constants.DEFAULT_SESSION_PLAYER_COUNT_MAX;

	public SessionPlayerCountFilterMinCustomization() { }

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Min))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Enabled, ref enabled) || changed;
			tempChanged = ImGui.SliderInt(localizationManager.ImGui.Value, ref value, 1, SliderMax);

			if (tempChanged)
			{
				changed = true;
				var max = sessionPlayerCountFilter.Customization.Max;

				max.SliderMin = Value;

				if (max.Value < Value)
				{
					max.Value = value;
				}
			}

			ImGui.TreePop();
		}

		return changed;
	}
}
