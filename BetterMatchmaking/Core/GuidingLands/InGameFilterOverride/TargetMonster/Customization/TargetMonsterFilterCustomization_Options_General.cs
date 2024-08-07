﻿using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;
internal class TargetMonsterFilterCustomization_Options_General : SingletonAccessor
{
	private bool _noPreference = true;
	public bool NoPreference { get => _noPreference; set => _noPreference = value; }

	public TargetMonsterFilterCustomization_Options_General()
	{
		InstantiateSingletons();
	}

	public TargetMonsterFilterCustomization_Options_General SelectAll()
	{
		NoPreference = true;

		return this;
	}

	public TargetMonsterFilterCustomization_Options_General DeselectAll()
	{
		NoPreference = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.General))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.NoPreference, ref _noPreference) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}