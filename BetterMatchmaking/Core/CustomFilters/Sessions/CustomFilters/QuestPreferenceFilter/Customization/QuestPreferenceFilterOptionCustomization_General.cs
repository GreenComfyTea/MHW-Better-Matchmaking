using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestPreferenceFilterOptionCustomization_General : SingletonAccessor
{
	private bool _none = true;
	public bool None { get => _none; set => _none = value; }

	private bool _assignments = true;
	public bool Assignments { get => _assignments; set => _assignments = value; }

	private bool _optional = true;
	public bool Optional { get => _optional; set => _optional = value; }

	private bool _investigation = true;
	public bool Investigation { get => _investigation; set => _investigation = value; }

	private bool _theGuidingLandsExpedition = true;
	public bool TheGuidingLandsExpedition { get => _theGuidingLandsExpedition; set => _theGuidingLandsExpedition = value; }

	private bool _eventQuests = true;
	public bool EventQuests { get => _eventQuests; set => _eventQuests = value; }

	private bool _specialAssignments = true;
	public bool SpecialAssignments { get => _specialAssignments; set => _specialAssignments = value; }

	private bool _arena = true;
	public bool Arena { get => _arena; set => _arena = value; }

	private bool _expeditions = true;
	public bool Expeditions { get => _expeditions; set => _expeditions = value; }

	private bool _temperedMonsters = true;
	public bool TemperedMonsters { get => _temperedMonsters; set => _temperedMonsters = value; }

	private bool _smallMonsters = true;
	public bool SmallMonsters { get => _smallMonsters; set => _smallMonsters = value; }

	public QuestPreferenceFilterOptionCustomization_General()
	{
		InstantiateSingletons();
	}

	public QuestPreferenceFilterOptionCustomization_General SelectAll()
	{
		None = true;
		Assignments = true;
		Optional = true;
		Investigation = true;
		TheGuidingLandsExpedition = true;
		EventQuests = true;
		SpecialAssignments = true;
		Arena = true;
		Expeditions = true;
		TemperedMonsters = true;
		SmallMonsters = true;

		return this;
	}

	public QuestPreferenceFilterOptionCustomization_General DeselectAll()
	{
		None = false;
		Assignments = false;
		Optional = false;
		Investigation = false;
		TheGuidingLandsExpedition = false;
		EventQuests = false;
		SpecialAssignments = false;
		Arena = false;
		Expeditions = false;
		TemperedMonsters = false;
		SmallMonsters = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.General))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.None, ref _none) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Assignments, ref _assignments) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Optional, ref _optional) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Investigation, ref _investigation) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.TheGuidingLandsExpedition, ref _theGuidingLandsExpedition) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.EventQuests, ref _eventQuests) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SpecialAssignments, ref _specialAssignments) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Arena, ref _arena) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Expeditions, ref _expeditions) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.TemperedMonsters, ref _temperedMonsters) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SmallMonsters, ref _smallMonsters) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
