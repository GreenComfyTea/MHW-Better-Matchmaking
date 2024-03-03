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
	public ConfigCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.Config))
		{
			changed = ImGui.Button(LocalizationManager_I.ImGui.ResetConfig) || changed;
			if(changed) ConfigManager_I.ResetConfig();

			ImGui.TreePop();
		}

		return changed;
	}
}
