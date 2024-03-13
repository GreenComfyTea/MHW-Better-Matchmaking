using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterOptionCustomization_FixedRegion : SingletonAccessor
{
	private bool _fixedRegionForest = true;
	public bool FixedRegionForest { get => _fixedRegionForest; set => _fixedRegionForest = value; }

	private bool _fixedRegionWildspire = true;
	public bool FixedRegionWildspire { get => _fixedRegionWildspire; set => _fixedRegionWildspire = value; }

	private bool _fixedRegionCoral = true;
	public bool FixedRegionCoral { get => _fixedRegionCoral; set => _fixedRegionCoral = value; }

	private bool _fixedRegionRotted = true;
	public bool FixedRegionRotted { get => _fixedRegionRotted; set => _fixedRegionRotted = value; }

	private bool _fixedRegionVolcanic = true;
	public bool FixedRegionVolcanic { get => _fixedRegionVolcanic; set => _fixedRegionVolcanic = value; }

	private bool _fixedRegionTundra = true;
	public bool FixedRegionTundra { get => _fixedRegionTundra; set => _fixedRegionTundra = value; }

	public ExpeditionObjectiveFilterOptionCustomization_FixedRegion()
	{
		InstantiateSingletons();
	}

	public ExpeditionObjectiveFilterOptionCustomization_FixedRegion SelectAll()
	{
		FixedRegionForest = true;
		FixedRegionWildspire = true;
		FixedRegionCoral = true;
		FixedRegionRotted = true;
		FixedRegionVolcanic = true;
		FixedRegionTundra = true;

		return this;
	}

	public ExpeditionObjectiveFilterOptionCustomization_FixedRegion DeselectAll()
	{
		FixedRegionForest = false;
		FixedRegionWildspire = false;
		FixedRegionCoral = false;
		FixedRegionRotted = false;
		FixedRegionVolcanic = false;
		FixedRegionTundra = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.FixedRegion))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FixedRegionForest, ref _fixedRegionForest) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FixedRegionWildspire, ref _fixedRegionWildspire) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FixedRegionCoral, ref _fixedRegionCoral) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FixedRegionRotted, ref _fixedRegionRotted) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FixedRegionVolcanic, ref _fixedRegionVolcanic) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FixedRegionTundra, ref _fixedRegionTundra) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
