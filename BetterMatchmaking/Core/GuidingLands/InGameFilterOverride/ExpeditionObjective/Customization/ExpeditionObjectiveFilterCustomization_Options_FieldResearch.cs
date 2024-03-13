using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterCustomization_Options_FieldResearch : SingletonAccessor
{
	private bool _fieldResearchForest = true;
	public bool FieldResearchForest { get => _fieldResearchForest; set => _fieldResearchForest = value; }

	private bool _fieldResearchWildspire = true;
	public bool FieldResearchWildspire { get => _fieldResearchWildspire; set => _fieldResearchWildspire = value; }

	private bool _fieldResearchCoral = true;
	public bool FieldResearchCoral { get => _fieldResearchCoral; set => _fieldResearchCoral = value; }

	private bool _fieldResearchRotted = true;
	public bool FieldResearchRotted { get => _fieldResearchRotted; set => _fieldResearchRotted = value; }

	private bool _fieldResearchVolcanic = true;
	public bool FieldResearchVolcanic { get => _fieldResearchVolcanic; set => _fieldResearchVolcanic = value; }

	private bool _fieldResearchTundra = true;
	public bool FieldResearchTundra { get => _fieldResearchTundra; set => _fieldResearchTundra = value; }

	public ExpeditionObjectiveFilterCustomization_Options_FieldResearch()
	{
		InstantiateSingletons();
	}

	public ExpeditionObjectiveFilterCustomization_Options_FieldResearch SelectAll()
	{
		FieldResearchForest = true;
		FieldResearchWildspire = true;
		FieldResearchCoral = true;
		FieldResearchRotted = true;
		FieldResearchVolcanic = true;
		FieldResearchTundra = true;

		return this;
	}

	public ExpeditionObjectiveFilterCustomization_Options_FieldResearch DeselectAll()
	{
		FieldResearchForest = false;
		FieldResearchWildspire = false;
		FieldResearchCoral = false;
		FieldResearchRotted = false;
		FieldResearchVolcanic = false;
		FieldResearchTundra = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.FieldResearch))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FieldResearchForest, ref _fieldResearchForest) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FieldResearchWildspire, ref _fieldResearchWildspire) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FieldResearchCoral, ref _fieldResearchCoral) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FieldResearchRotted, ref _fieldResearchRotted) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FieldResearchVolcanic, ref _fieldResearchVolcanic) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FieldResearchTundra, ref _fieldResearchTundra) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
