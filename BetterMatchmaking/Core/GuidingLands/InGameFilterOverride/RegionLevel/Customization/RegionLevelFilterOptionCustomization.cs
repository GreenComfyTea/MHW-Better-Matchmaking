using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class RegionLevelFilterOptionCustomization : SingletonAccessor
{
	private bool _level1 = true;
	public bool Level1 { get => _level1; set => _level1 = value; }

	private bool _level2 = true;
	public bool Level2 { get => _level2; set => _level2 = value; }

	private bool _level3 = true;
	public bool Level3 { get => _level3; set => _level3 = value; }

	private bool _level4 = true;
	public bool Level4 { get => _level4; set => _level4 = value; }

	private bool _level5 = true;
	public bool Level5 { get => _level5; set => _level5 = value; }

	private bool _level6 = true;
	public bool Level6 { get => _level6; set => _level6 = value; }

	private bool _level7 = true;
	public bool Level7 { get => _level7; set => _level7 = value; }

	public RegionLevelFilterOptionCustomization()
	{
		InstantiateSingletons();
	}

	private RegionLevelFilterOptionCustomization SelectAll()
	{
		Level1 = true;
		Level2 = true;
		Level3 = true;
		Level4 = true;
		Level5 = true;
		Level6 = true;
		Level7 = true;

		return this;
	}

	private RegionLevelFilterOptionCustomization DeselectAll()
	{
		Level1 = false;
		Level2 = false;
		Level3 = false;
		Level4 = false;
		Level5 = false;
		Level6 = false;
		Level7 = false;

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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level1, ref _level1) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level2, ref _level2) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level3, ref _level3) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level4, ref _level4) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level5, ref _level5) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level6, ref _level6) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Level7, ref _level7) || changed;
			
			ImGui.TreePop();
		}

		return changed;
	}
}
