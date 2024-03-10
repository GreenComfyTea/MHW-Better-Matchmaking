using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestTypeFilterCustomization : SingletonAccessor
{
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string QuestTypeReplacementTarget { get; set; }

	public QuestTypeFilterOptionCustomization FilterOptions { get; set; } = new();

	private QuestTypes _questTypeReplacementTargetEnum = QuestTypes.Expeditions;
	[JsonIgnore]
	public QuestTypes QuestTypeReplacementTargetEnum { get => _questTypeReplacementTargetEnum; set => _questTypeReplacementTargetEnum = value; }

	public QuestTypeFilterCustomization()
	{
		InstantiateSingletons();

		QuestTypeReplacementTarget = LocalizationManager_I.Default.ImGui.Expeditions;

	}

	public QuestTypeFilterCustomization Init()
	{
		var replacementTarget = QuestTypeReplacementTarget.Replace(" ", "");
		var success = Enum.TryParse(replacementTarget, true, out _questTypeReplacementTargetEnum);

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var questTypes = LocalizationManager_I.ImGui.QuestTypeArray;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.QuestType))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int) QuestTypeReplacementTargetEnum - 1;
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, questTypes, questTypes.Length);

			if(tempChanged)
			{
				QuestTypeReplacementTargetEnum = (QuestTypes) (selectedIndex + 1);
				QuestTypeReplacementTarget = LocalizationManager_I.Default.ImGui.QuestTypeArray[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
