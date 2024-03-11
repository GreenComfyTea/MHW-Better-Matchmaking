using ImGuiNET;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class PlayerTypeFilterCustomization : SingletonAccessor
{
	private bool _enabled = true;
	public bool Enabled { get => _enabled; set => _enabled = value; }

	public string PlayerTypeReplacementTarget { get; set; }

	public PlayerTypeFilterOptionCustomization FilterOptions { get; set; } = new();

	private PlayerTypes _playerTypeReplacementTargetEnum = PlayerTypes.Any;
	[JsonIgnore]
	public PlayerTypes PlayerTypeReplacementTargetEnum { get => _playerTypeReplacementTargetEnum; set => _playerTypeReplacementTargetEnum = value; }

	public PlayerTypeFilterCustomization()
	{
		InstantiateSingletons();

		PlayerTypeReplacementTarget = LocalizationManager_I.Default.ImGui.Any;
	}

	public PlayerTypeFilterCustomization Init()
	{
		var success = Enum.TryParse(PlayerTypeReplacementTarget, true, out _playerTypeReplacementTargetEnum);
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		var playerTypes = LocalizationManager_I.ImGui.PlayerTypeArray;

		if (ImGui.TreeNode(LocalizationManager_I.ImGui.PlayerType))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Enabled, ref _enabled) || changed;

			selectedIndex = (int)PlayerTypeReplacementTargetEnum;

			ImGui.SetNextItemWidth(CustomizationWindow_I.ComboBoxWidth);
			tempChanged = ImGui.Combo(LocalizationManager_I.ImGui.ReplacementTarget, ref selectedIndex, playerTypes, playerTypes.Length);

			if (tempChanged)
			{
				PlayerTypeReplacementTargetEnum = (PlayerTypes)selectedIndex;
				PlayerTypeReplacementTarget = LocalizationManager_I.Default.ImGui.PlayerTypeArray[selectedIndex];
			}

			changed = changed || tempChanged;

			changed = FilterOptions.RenderImGui() || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
