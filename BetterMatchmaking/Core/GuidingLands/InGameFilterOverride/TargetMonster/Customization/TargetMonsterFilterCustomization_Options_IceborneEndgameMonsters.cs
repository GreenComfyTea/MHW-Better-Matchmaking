using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetMonsterFilterCustomization_Options_IceborneEndgameMonsters : SingletonAccessor
{
	private bool _savageDeviljho = true;
	public bool SavageDeviljho { get => _savageDeviljho; set => _savageDeviljho = value; }

	private bool _bruteTigrex = true;
	public bool BruteTigrex { get => _bruteTigrex; set => _bruteTigrex = value; }

	private bool _zinogre = true;
	public bool Zinogre { get => _zinogre; set => _zinogre = value; }

	private bool _yianGaruga = true;
	public bool YianGaruga { get => _yianGaruga; set => _yianGaruga = value; }

	private bool _scarredYianGaruga = true;
	public bool ScarredYianGaruga { get => _scarredYianGaruga; set => _scarredYianGaruga = value; }

	private bool _goldRathian = true;
	public bool GoldRathian { get => _goldRathian; set => _goldRathian = value; }

	private bool _silverRathalos = true;
	public bool SilverRathalos { get => _silverRathalos; set => _silverRathalos = value; }

	private bool _rajang = true;
	public bool Rajang { get => _rajang; set => _rajang = value; }

	private bool _stygianZinogre = true;
	public bool StygianZinogre { get => _stygianZinogre; set => _stygianZinogre = value; }

	public TargetMonsterFilterCustomization_Options_IceborneEndgameMonsters()
	{
		InstantiateSingletons();
	}

	public TargetMonsterFilterCustomization_Options_IceborneEndgameMonsters SelectAll()
	{
		SavageDeviljho = true;
		BruteTigrex = true;
		Zinogre = true;
		YianGaruga = true;
		ScarredYianGaruga = true;
		GoldRathian = true;
		SilverRathalos = true;
		Rajang = true;
		StygianZinogre = true;

		return this;
	}

	public TargetMonsterFilterCustomization_Options_IceborneEndgameMonsters DeselectAll()
	{
		SavageDeviljho = false;
		BruteTigrex = false;
		Zinogre = false;
		YianGaruga = false;
		ScarredYianGaruga = false;
		GoldRathian = false;
		SilverRathalos = false;
		Rajang = false;
		StygianZinogre = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.IceborneEndgameMonsters))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SavageDeviljho, ref _savageDeviljho) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BruteTigrex, ref _bruteTigrex) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Zinogre, ref _zinogre) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.YianGaruga, ref _yianGaruga) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.ScarredYianGaruga, ref _scarredYianGaruga) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.GoldRathian, ref _goldRathian) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SilverRathalos, ref _silverRathalos) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Rajang, ref _rajang) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.StygianZinogre, ref _stygianZinogre) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
