using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class LocalizationCustomization : SingletonAccessor
{
	public List<string> LocalizationNamesList { get; set; } = new();

	private string[] LocalizationNames { get; set; } = Array.Empty<string>();

	private int _selectedLocalizationIndex = 0;
	private int SelectedLocalizationIndex { get => _selectedLocalizationIndex; set => _selectedLocalizationIndex = value; }

	public LocalizationCustomization()
	{
		InstantiateSingletons();
	}

	public LocalizationCustomization SetCurrentLocalization(string name)
	{
		var newSelectedLocalizationIndex = Array.IndexOf(LocalizationNames, name);

		if (newSelectedLocalizationIndex == -1) return this;

		SelectedLocalizationIndex = newSelectedLocalizationIndex;

		return this;
	}

	public LocalizationCustomization DeleteLocalization(string name)
	{
		if (LocalizationNamesList.Remove(name))
		{
			UpdateNamesList();
		}

		return this;
	}

	public LocalizationCustomization AddLocalization(string name)
	{
		if (LocalizationNamesList.Contains(name)) return this;

		LocalizationNamesList.Add(name);
		UpdateNamesList();

		return this;
	}

	public LocalizationCustomization UpdateNamesList()
	{
		LocalizationNamesList.Sort((left, right) =>
		{
			if (left.Equals(Constants.DEFAULT_LOCALIZATION)) return -1;

			return left.CompareTo(right);
		});

		LocalizationNames = LocalizationNamesList.ToArray();

		return this;
	}

	public LocalizationCustomization OnLocalizationChanged(int selectedLocalizationIndex)
	{
		var currentLocalizationName = LocalizationNames[selectedLocalizationIndex];
		ConfigManagerInstance.Current.Localization = currentLocalizationName;
		LocalizationManagerInstance.SetCurrentLocalization(currentLocalizationName);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(LocalizationManagerInstance.ImGui.Language))
		{
			tempChanged = ImGui.Combo(LocalizationManagerInstance.ImGui.Language, ref _selectedLocalizationIndex, LocalizationNames, LocalizationNames.Length);
			if (tempChanged) OnLocalizationChanged(SelectedLocalizationIndex);
			changed = changed || tempChanged;

			ImGui.Text(LocalizationManagerInstance.ImGui.Translators);
			ImGui.SameLine();
			ImGui.TextColored(Constants.IMGUI_USERNAME_COLOR, LocalizationManagerInstance.Current.LocalizationInfo.Translators);

			ImGui.TreePop();
		}

		return changed;
	}
}
