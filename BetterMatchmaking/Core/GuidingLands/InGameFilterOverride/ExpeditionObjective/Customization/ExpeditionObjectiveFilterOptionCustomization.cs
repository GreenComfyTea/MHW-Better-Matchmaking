using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class ExpeditionObjectiveFilterOptionCustomization : SingletonAccessor
{
	public ExpeditionObjectiveFilterOptionCustomization_General General { get; set; } = new();
	public ExpeditionObjectiveFilterOptionCustomization_FieldResearch FieldResearch { get; set; } = new();
	public ExpeditionObjectiveFilterOptionCustomization_Mining Mining { get; set; } = new();
	public ExpeditionObjectiveFilterOptionCustomization_BoneResearch BoneResearch { get; set; } = new();
	public ExpeditionObjectiveFilterOptionCustomization_FixedRegion FixedRegion { get; set; } = new();

	public ExpeditionObjectiveFilterOptionCustomization()
	{
		InstantiateSingletons();
	}

	private ExpeditionObjectiveFilterOptionCustomization SelectAll()
	{
		General.SelectAll();
		FieldResearch.SelectAll();
		Mining.SelectAll();
		BoneResearch.SelectAll();
		FixedRegion.SelectAll();

		return this;
	}

	private ExpeditionObjectiveFilterOptionCustomization DeselectAll()
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
