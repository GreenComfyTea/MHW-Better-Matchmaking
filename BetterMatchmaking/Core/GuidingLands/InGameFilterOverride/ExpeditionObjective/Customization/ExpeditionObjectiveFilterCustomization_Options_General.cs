using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterCustomization_Options_General : SingletonAccessor
{
	private bool _none = true;
	public bool None { get => _none; set => _none = value; }


	public ExpeditionObjectiveFilterCustomization_Options_General()
	{
		InstantiateSingletons();
	}

	public ExpeditionObjectiveFilterCustomization_Options_General SelectAll()
	{
		None = true;

		return this;
	}

	public ExpeditionObjectiveFilterCustomization_Options_General DeselectAll()
	{
		None = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.General))
		{
			//if(ImGui.Button(LocalizationManager_I.ImGui.SelectAll))
			//{
			//	SelectAll();
			//	changed = true;
			//}

			//ImGui.SameLine();

			//if(ImGui.Button(LocalizationManager_I.ImGui.DeselectAll))
			//{
			//	DeselectAll();
			//	changed = true;
			//}

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.None, ref _none) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
