using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class GuidingLandsInGameFilterOverrideCustomization : SingletonAccessor
{
	public ExpeditionObjectiveFilterCustomization ExpeditionObjective { get; set; } = new();
	public RegionLevelFilterCustomization RegionLevel { get; set; } = new();
	public LanguageFilterCustomization Language { get; set; } = new();
	public TargetMonsterFilterCustomization TargetMonster { get; set; } = new();

	public GuidingLandsInGameFilterOverrideCustomization()
	{
		InstantiateSingletons();
	}

	public GuidingLandsInGameFilterOverrideCustomization Init()
	{
		ExpeditionObjective.Init();
		RegionLevel.Init();
		Language.Init();
		TargetMonster.Init();

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		if(ImGui.TreeNode(LocalizationManager_I.ImGui.InGameFilterOverride))
		{
			changed = ExpeditionObjective.RenderImGui() || changed;
			changed = RegionLevel.RenderImGui() || changed;
			changed = Language.RenderImGui() || changed;
			changed = TargetMonster.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
