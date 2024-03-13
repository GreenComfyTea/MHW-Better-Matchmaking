using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterCustomization_Options_Mining : SingletonAccessor
{
	private bool _miningForest = true;
	public bool MiningForest { get => _miningForest; set => _miningForest = value; }

	private bool _miningWildspire = true;
	public bool MiningWildspire { get => _miningWildspire; set => _miningWildspire = value; }

	private bool _miningCoral = true;
	public bool MiningCoral { get => _miningCoral; set => _miningCoral = value; }

	private bool _miningRotted = true;
	public bool MiningRotted { get => _miningRotted; set => _miningRotted = value; }

	private bool _miningVolcanic = true;
	public bool MiningVolcanic { get => _miningVolcanic; set => _miningVolcanic = value; }

	private bool _miningTundra = true;
	public bool MiningTundra { get => _miningTundra; set => _miningTundra = value; }

	public ExpeditionObjectiveFilterCustomization_Options_Mining()
	{
		InstantiateSingletons();
	}

	public ExpeditionObjectiveFilterCustomization_Options_Mining SelectAll()
	{
		MiningForest = true;
		MiningWildspire = true;
		MiningCoral = true;
		MiningRotted = true;
		MiningVolcanic = true;
		MiningTundra = true;

		return this;
	}

	public ExpeditionObjectiveFilterCustomization_Options_Mining DeselectAll()
	{
		MiningForest = false;
		MiningWildspire = false;
		MiningCoral = false;
		MiningRotted = false;
		MiningVolcanic = false;
		MiningTundra = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.Mining))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MiningForest, ref _miningForest) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MiningWildspire, ref _miningWildspire) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MiningCoral, ref _miningCoral) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MiningRotted, ref _miningRotted) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MiningVolcanic, ref _miningVolcanic) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MiningTundra, ref _miningTundra) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
