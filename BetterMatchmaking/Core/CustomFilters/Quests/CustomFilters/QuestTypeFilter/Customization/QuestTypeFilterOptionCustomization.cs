using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestTypeFilterOptionCustomization : SingletonAccessor
{
	private bool _optinalQuests = true;
	public bool OptionalQuests { get => _optinalQuests; set => _optinalQuests = value; }

	private bool _assignments = true;
	public bool Assignments { get => _assignments; set => _assignments = value; }

	private bool _investigations = true;
	public bool Investigations { get => _investigations; set => _investigations = value; }

	private bool _expeditions = true;
	public bool Expeditions { get => _expeditions; set => _expeditions = value; }

	private bool _eventQuests = true;
	public bool EventQuests { get => _eventQuests; set => _eventQuests = value; }

	private bool _specialInvestigations = true;
	public bool SpecialInvestigations { get => _specialInvestigations; set => _specialInvestigations = value; }

	public QuestTypeFilterOptionCustomization()
	{
		InstantiateSingletons();
	}

	private QuestTypeFilterOptionCustomization SelectAll()
	{
		OptionalQuests = true;
		Assignments = true;
		Investigations = true;
		Expeditions = true;
		EventQuests = true;
		SpecialInvestigations = true;

		return this;
	}

	private QuestTypeFilterOptionCustomization DeselectAll()
	{
		OptionalQuests = false;
		Assignments = false;
		Investigations = false;
		Expeditions = false;
		EventQuests = false;
		SpecialInvestigations = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.FilterOptions))
		{
			if(ImGui.Button(LocalizationManager_I.ImGui.SelectAll))
			{
				SelectAll();
				changed = true;
			}

			ImGui.SameLine();

			if(ImGui.Button(LocalizationManager_I.ImGui.DeselectAll))
			{
				DeselectAll();
				changed = true;
			}

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.OptionalQuests, ref _optinalQuests) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Assignments, ref _assignments) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Investigations, ref _investigations) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Expeditions, ref _expeditions) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.EventQuests, ref _eventQuests) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SpecialInvestigations, ref _specialInvestigations) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
