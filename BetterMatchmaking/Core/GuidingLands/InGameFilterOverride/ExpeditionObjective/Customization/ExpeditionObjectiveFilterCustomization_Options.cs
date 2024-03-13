using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterCustomization_Options : SingletonAccessor
{
	public ExpeditionObjectiveFilterCustomization_Options_General General { get; set; } = new();
	public ExpeditionObjectiveFilterCustomization_Options_FieldResearch FieldResearch { get; set; } = new();
	public ExpeditionObjectiveFilterCustomization_Options_Mining Mining { get; set; } = new();
	public ExpeditionObjectiveFilterCustomization_Options_BoneResearch BoneResearch { get; set; } = new();
	public ExpeditionObjectiveFilterCustomization_Options_FixedRegion FixedRegion { get; set; } = new();

	public ExpeditionObjectiveFilterCustomization_Options()
	{
		InstantiateSingletons();
	}

	private ExpeditionObjectiveFilterCustomization_Options SelectAll()
	{
		General.SelectAll();
		FieldResearch.SelectAll();
		Mining.SelectAll();
		BoneResearch.SelectAll();
		FixedRegion.SelectAll();

		return this;
	}

	private ExpeditionObjectiveFilterCustomization_Options DeselectAll()
	{
		General.DeselectAll();
		FieldResearch.DeselectAll();
		Mining.DeselectAll();
		BoneResearch.DeselectAll();
		FixedRegion.DeselectAll();

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

			changed = General.RenderImGui() || changed;
			changed = FieldResearch.RenderImGui() || changed;
			changed = Mining.RenderImGui() || changed;
			changed = BoneResearch.RenderImGui() || changed;
			changed = FixedRegion.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
