using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class ConfigCustomization : SingletonAccessor
{
    public List<string> ConfigNamesList { get; set; } = new();

    public string[] ConfigNames { get; set; } = Array.Empty<string>();

    private string configNameInput = string.Empty;
    public string ConfigNameInput { get => configNameInput; set => configNameInput = value; }

    private int selectedConfigIndex = 0;
    public int SelectedConfigIndex { get => selectedConfigIndex; set => selectedConfigIndex = value; }

    public void SetCurrentConfig(string name)
    {
        var newSelectedConfigIndex = Array.IndexOf(ConfigNames, name);

        if (newSelectedConfigIndex == -1) return;

        SelectedConfigIndex = newSelectedConfigIndex;
    }

    public void DeleteConfig(string name)
    {
        if (ConfigNamesList.Remove(name))
        {
            UpdateNamesList();
            return;
        }
    }

    public void AddConfig(string name)
    {
        if (ConfigNamesList.Contains(name)) return;

        ConfigNamesList.Add(name);
        UpdateNamesList();
    }

    public void UpdateNamesList()
    {
        ConfigNamesList.Sort((left, right) =>
        {
            if (left.Equals(Constants.DEFAULT_CONFIG)) return -1;

            return left.CompareTo(right);
        });

        ConfigNames = ConfigNamesList.ToArray();
    }

    public void RenderImGui()
    {
        var changed = false;
        var tempChanged = false;

        if (ImGui.TreeNode(localizationManager.ImGui.Config))
        {
            tempChanged = ImGui.Combo(localizationManager.ImGui.Config, ref selectedConfigIndex, ConfigNames, ConfigNames.Length);
            if (tempChanged)
            {
                _ = configManager.SetCurrentConfig(configManager.Configs[ConfigNames[SelectedConfigIndex]]);
            }
            changed = changed || tempChanged;

            changed = ImGui.InputText(localizationManager.ImGui.ConfigName, ref configNameInput, 64) || changed;

            if (ImGui.Button(localizationManager.ImGui.New))
            {
                _ = configManager.NewConfig(ConfigNameInput);
            }

            ImGui.SameLine();

            if (ImGui.Button(localizationManager.ImGui.Duplicate))
            {
                _ = configManager.DuplicateConfig(ConfigNameInput);
            }

            ImGui.SameLine();

            if (ImGui.Button(localizationManager.ImGui.Reset))
            {
                _ = configManager.ResetConfig();
            }

            ImGui.SameLine();

            if (ImGui.Button(localizationManager.ImGui.Delete))
            {
                _ = configManager.DeleteConfig();
            }

            ImGui.TreePop();
        }
    }
}
