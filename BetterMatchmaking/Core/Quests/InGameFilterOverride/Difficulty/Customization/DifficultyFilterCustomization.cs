using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class DifficultyFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	public DifficultyFilterCustomization_Options FilterOptions { get; set; } = new();

	[JsonIgnore]
	public Difficulties ReplacementTargetEnum { get; set; } = Difficulties.LowRank1;

	public DifficultyFilterCustomization()
	{
		InstantiateSingletons();
		ReplacementTarget = LocalizationManager_I.Default.ImGui._1;
	}

	public DifficultyFilterCustomization Init()
	{
		var stringIndex = Array.FindIndex(
			LocalizationManager.Instance.Default.ImGui.QuestRankReplacementTargets, arrayString => arrayString.Equals(ReplacementTarget)
		);

		ReplacementTargetEnum = (Difficulties) StringIndexToEnum(stringIndex);

		return this;
	}

	private static int StringIndexToEnum(int stringIndex)
	{
		var highRank9Index = Array.FindIndex(
			LocalizationManager.Instance.Default.ImGui.QuestRankReplacementTargets, arrayString => arrayString.Equals(LocalizationManager.Instance.Default.ImGui._9)
		);

		if (stringIndex <= 2) return stringIndex + 20;
		if (stringIndex <= highRank9Index) return stringIndex - 2;

		return stringIndex - 1;
	}

	private static int EnumToStringIndex(Difficulties replacementTargetEnum)
	{
		var replacementTargetEnumValue = (int) replacementTargetEnum;

		if (replacementTargetEnum <= Difficulties.HighRank9) return replacementTargetEnumValue + 2;
		if (replacementTargetEnum <= Difficulties.MasterRank6) return replacementTargetEnumValue + 1;

		return replacementTargetEnumValue - 20;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var styledQuestRanks = LocalizationManager_I.ImGui.StyledQuestRankReplacementTargets;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Difficulty))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = EnumToStringIndex(ReplacementTargetEnum);

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, styledQuestRanks, styledQuestRanks.Length);

			if (tempChanged)
			{
				ReplacementTargetEnum = (Difficulties) StringIndexToEnum(selectedIndex);
				ReplacementTarget = LocalizationManager_I.Default.ImGui.QuestRankReplacementTargets[selectedIndex];
				TeaLog.Info(ReplacementTarget);
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
