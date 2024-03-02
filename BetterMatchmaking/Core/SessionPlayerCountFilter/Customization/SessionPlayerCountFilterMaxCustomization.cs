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
    private bool _enabled = true;
    public bool Enabled { get => _enabled; set => _enabled = value; }

    private int _value = 15;
    public int Value { get => _value; set => _value = value; }

    [JsonIgnore]
    public int SliderMin { get; set; } = Constants.DEFAULT_SESSION_PLAYER_COUNT_MIN;

    public SessionPlayerCountFilterMaxCustomization() { }

    public bool RenderImGui()
    {
        var changed = false;
        var tempChanged = false;

        if (ImGui.TreeNode(localizationManager.ImGui.Max))
        {
            changed = ImGui.Checkbox(localizationManager.ImGui.Enabled, ref _enabled) || changed;
            tempChanged = ImGui.SliderInt(localizationManager.ImGui.Value, ref _value, SliderMin, 15);

            if (tempChanged)
            {
                changed = true;
                var min = sessionPlayerCountFilter.Customization.Min;

                min.SliderMax = Value;

                if (min.Value > Value)
                {
                    min.Value = _value;
                }
            }

            ImGui.TreePop();

        }

        return changed;
    }
}