using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class ConfigCustomization : SingletonAccessor
{
	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Config))
		{
			changed = ImGui.Button(localizationManager.ImGui.ResetConfig) || changed;
			if(changed) configManager.ResetConfig();

			ImGui.TreePop();
		}

		return changed;
	}
}
