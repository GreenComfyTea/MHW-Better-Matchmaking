﻿using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestTypeFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public QuestTypeFilterCustomization_Options FilterOptions { get; set; } = new();

	private QuestTypes _replacementTargetEnum = QuestTypes.Expeditions;
	[JsonIgnore]
	public QuestTypes ReplacementTargetEnum { get => _replacementTargetEnum; set => _replacementTargetEnum = value; }

	public QuestTypeFilterCustomization()
	{
		InstantiateSingletons();

		ReplacementTarget = LocalizationManager_I.Default.ImGui.Expeditions;
	}

	public QuestTypeFilterCustomization Init()
	{
		var replacementTarget = ReplacementTarget.Replace(" ", "");
		var success = Enum.TryParse(replacementTarget, true, out _replacementTargetEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var questTypes = LocalizationManager_I.ImGui.QuestTypeArray;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.QuestType))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int) ReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, questTypes, questTypes.Length);

			if (tempChanged)
			{
				ReplacementTargetEnum = (QuestTypes) (selectedIndex);
				ReplacementTarget = LocalizationManager_I.Default.ImGui.QuestTypeArray[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
