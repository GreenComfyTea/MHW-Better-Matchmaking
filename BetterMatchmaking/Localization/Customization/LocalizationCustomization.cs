using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class LocalizationCustomization : SingletonAccessor
{
	public List<string> LocalizationNamesList { get; set; } = new();

	public string[] LocalizationNames { get; set; } = Array.Empty<string>();

	private int selectedLocalizationIndex = 0;
	public int SelectedLocalizationIndex { get => selectedLocalizationIndex; set => selectedLocalizationIndex = value; }

	public void SetCurrentLocalization(string name)
	{
		var newSelectedLocalizationIndex = Array.IndexOf(LocalizationNames, name);

		if (newSelectedLocalizationIndex == -1) return;

		SelectedLocalizationIndex = newSelectedLocalizationIndex;
	}

	public void DeleteLocalization(string name)
	{
		if (LocalizationNamesList.Remove(name))
		{
			UpdateNamesList();
			return;
		}
	}

	public void AddLocalization(string name)
	{
		if (LocalizationNamesList.Contains(name)) return;

		LocalizationNamesList.Add(name);
		UpdateNamesList();
	}

	public void UpdateNamesList()
	{
		LocalizationNamesList.Sort((left, right) =>
		{
			if (left.Equals(Constants.DEFAULT_LOCALIZATION)) return -1;

			return left.CompareTo(right);
		});

		LocalizationNames = LocalizationNamesList.ToArray();
	}

	public async Task OnLocalizationChanged(int selectedLocalizationIndex)
	{
		var currentLocalizationName = LocalizationNames[selectedLocalizationIndex];
		configManager.Current.Language = currentLocalizationName;
		localizationManager.SetCurrentLocalization(currentLocalizationName);
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Language))
		{
			tempChanged = ImGui.Combo(localizationManager.ImGui.Language, ref selectedLocalizationIndex, LocalizationNames, LocalizationNames.Length);
			if (tempChanged) _ = OnLocalizationChanged(SelectedLocalizationIndex);
			changed = changed || tempChanged;

			ImGui.Text(localizationManager.ImGui.Translators);
			ImGui.SameLine();
			ImGui.TextColored(Constants.IMGUI_USERNAME_COLOR, localizationManager.Current.LocalizationInfo.Translators);

			ImGui.TreePop();
		}

		return changed;
	}
}
