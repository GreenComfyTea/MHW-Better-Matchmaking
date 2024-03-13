using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterOptionCustomization_BoneResearch : SingletonAccessor
{
	private bool _boneResearchForest = true;
	public bool BoneResearchForest { get => _boneResearchForest; set => _boneResearchForest = value; }

	private bool _boneResearchWildspire = true;
	public bool BoneResearchWildspire { get => _boneResearchWildspire; set => _boneResearchWildspire = value; }

	private bool _boneResearchCoral = true;
	public bool BoneResearchCoral { get => _boneResearchCoral; set => _boneResearchCoral = value; }

	private bool _boneResearchRotted = true;
	public bool BoneResearchRotted { get => _boneResearchRotted; set => _boneResearchRotted = value; }

	private bool _boneResearchVolcanic = true;
	public bool BoneResearchVolcanic { get => _boneResearchVolcanic; set => _boneResearchVolcanic = value; }

	private bool _boneResearchTundra = true;
	public bool BoneResearchTundra { get => _boneResearchTundra; set => _boneResearchTundra = value; }

	public ExpeditionObjectiveFilterOptionCustomization_BoneResearch()
	{
		InstantiateSingletons();
	}

	public ExpeditionObjectiveFilterOptionCustomization_BoneResearch SelectAll()
	{
		BoneResearchForest = true;
		BoneResearchWildspire = true;
		BoneResearchCoral = true;
		BoneResearchRotted = true;
		BoneResearchVolcanic = true;
		BoneResearchTundra = true;

		return this;
	}

	public ExpeditionObjectiveFilterOptionCustomization_BoneResearch DeselectAll()
	{
		BoneResearchForest = false;
		BoneResearchWildspire = false;
		BoneResearchCoral = false;
		BoneResearchRotted = false;
		BoneResearchVolcanic = false;
		BoneResearchTundra = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.BoneResearch))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BoneResearchForest, ref _boneResearchForest) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BoneResearchWildspire, ref _boneResearchWildspire) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BoneResearchCoral, ref _boneResearchCoral) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BoneResearchRotted, ref _boneResearchRotted) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BoneResearchVolcanic, ref _boneResearchVolcanic) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BoneResearchTundra, ref _boneResearchTundra) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
