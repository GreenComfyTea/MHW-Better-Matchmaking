using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class DifficultyFilterOptionCustomization_MasterRank : SingletonAccessor
{
	private bool _masterRank1 = true;
	public bool MasterRank1 { get => _masterRank1; set => _masterRank1 = value; }

	private bool _masterRank2 = true;
	public bool MasterRank2 { get => _masterRank2; set => _masterRank2 = value; }

	private bool _masterRank3 = true;
	public bool MasterRank3 { get => _masterRank3; set => _masterRank3 = value; }

	private bool _masterRank4 = true;
	public bool MasterRank4 { get => _masterRank4; set => _masterRank4 = value; }

	private bool _masterRank5 = true;
	public bool MasterRank5 { get => _masterRank5; set => _masterRank5 = value; }

	private bool _masterRank6 = true;
	public bool MasterRank6 { get => _masterRank6; set => _masterRank6 = value; }

	public DifficultyFilterOptionCustomization_MasterRank()
	{
		InstantiateSingletons();
	}

	public DifficultyFilterOptionCustomization_MasterRank SelectAll()
	{
		MasterRank1 = true;
		MasterRank2 = true;
		MasterRank3 = true;
		MasterRank4 = true;
		MasterRank5 = true;
		MasterRank6 = true;

		return this;
	}

	public DifficultyFilterOptionCustomization_MasterRank DeselectAll()
	{
		MasterRank1 = false;
		MasterRank2 = false;
		MasterRank3 = false;
		MasterRank4 = false;
		MasterRank5 = false;
		MasterRank6 = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.MasterRank))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MasterRank1, ref _masterRank1) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MasterRank2, ref _masterRank2) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MasterRank3, ref _masterRank3) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MasterRank4, ref _masterRank4) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MasterRank5, ref _masterRank5) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.MasterRank6, ref _masterRank6) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
