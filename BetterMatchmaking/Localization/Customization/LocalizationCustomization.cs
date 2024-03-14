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
	public List<string> LocalizationIsoNamesList { get; set; } = new();

	private List<string> LocalizationNamesList { get; set; } = new();
	private string[] LocalizationNames { get; set; } = Array.Empty<string>();

	private int _selectedLocalizationIndex = 0;
	private int SelectedLocalizationIndex { get => _selectedLocalizationIndex; set => _selectedLocalizationIndex = value; }

	private Vector4 TranslatorColor { get; set; } = Constants.MOD_AUTHOR_COLOR;

	public LocalizationCustomization()
	{
		InstantiateSingletons();
	}

	public LocalizationCustomization SetCurrentLocalization(Localization localization)
	{
		var newSelectedLocalizationIndex = LocalizationIsoNamesList.IndexOf(localization.IsoName);

		if (newSelectedLocalizationIndex == -1) return this;

		SelectedLocalizationIndex = newSelectedLocalizationIndex;

		TranslatorColor =
			LocalizationManager_I.Current.LocalizationInfo.Translators.Equals(Constants.MOD_AUTHOR)
			? Constants.MOD_AUTHOR_COLOR
			: Constants.IMGUI_USERNAME_COLOR;

		return this;
	}

	public LocalizationCustomization DeleteLocalization(Localization localization)
	{
		if (LocalizationIsoNamesList.Remove(localization.IsoName))
		{
			LocalizationNamesList.Remove(localization.LocalizationInfo.Name);
			UpdateNamesList();
		}

		return this;
	}

	public LocalizationCustomization AddLocalization(Localization localization)
	{
		if (LocalizationIsoNamesList.Contains(localization.IsoName)) return this;

		LocalizationIsoNamesList.Add(localization.IsoName);
		LocalizationNamesList.Add(localization.LocalizationInfo.Name);

		UpdateNamesList();

		return this;
	}

	public LocalizationCustomization UpdateNamesList()
	{
		LocalizationNames = LocalizationNamesList.ToArray();

		return this;
	}

	public LocalizationCustomization OnLocalizationChanged(int selectedLocalizationIndex)
	{
		var currentLocalizationName = LocalizationIsoNamesList[selectedLocalizationIndex];
		ConfigManager_I.Current.Localization = currentLocalizationName;
		LocalizationManager_I.SetCurrentLocalization(currentLocalizationName);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Language))
		{
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.Language, ref _selectedLocalizationIndex, LocalizationNames, LocalizationNames.Length);
			if (tempChanged) OnLocalizationChanged(SelectedLocalizationIndex);
			changed = changed || tempChanged;

			ImGui.Text($"{LocalizationManager_I.ImGui.Translators}:");
			ImGui.SameLine();
			ImGui.TextColored(TranslatorColor, LocalizationManager_I.Current.LocalizationInfo.Translators);

			ImGui.TreePop();
		}

		return changed;
	}
}
