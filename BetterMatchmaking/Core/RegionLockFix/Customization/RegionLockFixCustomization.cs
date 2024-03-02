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

internal class RegionLockFixCustomization : SingletonAccessor
{
    public RegionLockFixLobbyCustomization Sessions { get; set; } = new();
    public RegionLockFixLobbyCustomization Quests { get; set; } = new();

    public RegionLockFixCustomization() { }

    public RegionLockFixCustomization Init()
    {
        Sessions.Init();
        Quests.Init();

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(localizationManager.ImGui.RegionLockFix))
        {
            changed = Sessions.RenderImGui(localizationManager.ImGui.Sessions) || changed;
            changed = Quests.RenderImGui(localizationManager.ImGui.Quests) || changed;

            ImGui.TreePop();

        }

        return changed;
    }
}
