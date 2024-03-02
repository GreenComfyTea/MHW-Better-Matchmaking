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

		if (ImGui.TreeNode(LocalizationManagerInstance.ImGui.Config))
		{
			changed = ImGui.Button(LocalizationManagerInstance.ImGui.ResetConfig) || changed;
			if(changed) ConfigManagerInstance.ResetConfig();

			ImGui.TreePop();
		}

		return changed;
	}
}
