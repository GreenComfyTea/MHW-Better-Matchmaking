using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;
internal class CustomQuestRankFilterCustomization : SingletonAccessor
{
	private bool _enabled = false;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string ReplacementTarget { get; set; }

	[JsonIgnore]
	public QuestRanks ReplacementTargetEnum { get; set; } = QuestRanks.LowRank1;


	public CustomQuestRankFilterCustomization()
	{
		InstantiateSingletons();
		ReplacementTarget = LocalizationManager_I.Default.ImGui.LowRank1;
	}

	public CustomQuestRankFilterCustomization Init()
	{

		var stringIndex = Array.FindIndex(
			LocalizationManager.Instance.Default.ImGui.QuestRankReplacementTargets, arrayString => arrayString.Equals(ReplacementTarget)
		);

		ReplacementTargetEnum = (QuestRanks) StringIndexToEnum(stringIndex);

		TeaLog.Info("\n");

		//foreach(var value in Enum.GetValues<QuestRanks>())
		//{
		//	var stringIndex2 = EnumToStringIndex(value);

		//	TeaLog.Info($"{((int)value)} ({value}) -> {stringIndex2} ({LocalizationManager_I.Default.ImGui.QuestRanks[stringIndex2]})");
		//}

		for(int i = 0; i < LocalizationManager_I.Default.ImGui.QuestRankReplacementTargets.Length; i++)
		{
			var str = LocalizationManager_I.Default.ImGui.QuestRankReplacementTargets[i];

			var enumIndex = StringIndexToEnum(i);
			var enumV = (QuestRanks) enumIndex;

			TeaLog.Info($"{i} ({str}) -> {enumIndex} ({enumV})");
		}

		TeaLog.Info("\n");

		return this;
	}

	private int StringIndexToEnum(int stringIndex)
	{
		var highRank9Index = Array.FindIndex(
			LocalizationManager.Instance.Default.ImGui.QuestRankReplacementTargets, arrayString => arrayString.Equals(LocalizationManager.Instance.Default.ImGui.HighRank9)
		);

		if(stringIndex <= 2) return stringIndex + 20;
		if(stringIndex <= highRank9Index) return stringIndex - 2;
		
		return stringIndex - 1;
	}

	private int EnumToStringIndex(QuestRanks replacementTargetEnum)
	{
		var replacementTargetEnumValue = (int) replacementTargetEnum;

		if(replacementTargetEnumValue <= (int) QuestRanks.HighRank9) return replacementTargetEnumValue + 2;
		if(replacementTargetEnumValue <= (int) QuestRanks.MasterRank6) return replacementTargetEnumValue + 1;

		return replacementTargetEnumValue - 20;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var questRanks = LocalizationManager_I.ImGui.QuestRankReplacementTargets;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.CustomQuestRankFilter))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = EnumToStringIndex(ReplacementTargetEnum);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, questRanks, questRanks.Length);

			if(tempChanged)
			{
				ReplacementTargetEnum = (QuestRanks) StringIndexToEnum(selectedIndex);
				ReplacementTarget = LocalizationManager_I.Default.ImGui.QuestRankReplacementTargets[selectedIndex];
				TeaLog.Info(ReplacementTarget);
			}

			changed = changed || tempChanged;

			ImGui.TreePop();
		}

		return changed;
	}
}
