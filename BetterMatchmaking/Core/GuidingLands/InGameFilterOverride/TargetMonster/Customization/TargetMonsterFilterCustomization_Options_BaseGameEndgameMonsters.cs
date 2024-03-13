using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetMonsterFilterCustomization_Options_BaseGameEndgameMonsters : SingletonAccessor
{
	private bool _lunastra = true;
	public bool Lunastra { get => _lunastra; set => _lunastra = value; }

	public TargetMonsterFilterCustomization_Options_BaseGameEndgameMonsters()
	{
		InstantiateSingletons();
	}

	public TargetMonsterFilterCustomization_Options_BaseGameEndgameMonsters SelectAll()
	{
		Lunastra = true;

		return this;
	}

	public TargetMonsterFilterCustomization_Options_BaseGameEndgameMonsters DeselectAll()
	{
		Lunastra = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.BaseGameEndgameMonsters))
		{
			//if (ImGui.Button(LocalizationManager_I.ImGui.SelectAll))
			//{
			//    SelectAll();
			//    changed = true;
			//}

			//ImGui.SameLine();

			//if (ImGui.Button(LocalizationManager_I.ImGui.DeselectAll))
			//{
			//    DeselectAll();
			//    changed = true;
			//}

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Lunastra, ref _lunastra) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}