using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class PlayerCountFilterCustomization : SingletonAccessor
{
    public PlayerCountFilterMinCustomization Min { get; set; } = new();

    public PlayerCountFilterMaxCustomization Max { get; set; } = new();

    public PlayerCountFilterCustomization()
    {
        InstantiateSingletons();
    }

    public PlayerCountFilterCustomization Init()
    {
        if (Max.Value < Min.Value)
        {
            Max.Value = Min.Value;
        }

        Min.SliderMax = Max.Value;
        Max.SliderMin = Min.Value;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.PlayerCountFilter))
        {
            changed = Min.RenderImGui() || changed;
            changed = Max.RenderImGui() || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}
