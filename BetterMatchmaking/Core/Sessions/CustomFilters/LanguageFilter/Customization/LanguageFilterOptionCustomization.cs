using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class LanguageFilterOptionCustomization : SingletonAccessor
{
	private bool _japanese = true;
	public bool Japanese { get => _japanese; set => _japanese = value; }

	private bool _english = true;
	public bool English { get => _english; set => _english = value; }

	private bool _french = true;
	public bool French { get => _french; set => _french = value; }

	private bool _italian = true;
	public bool Italian { get => _italian; set => _italian = value; }

	private bool _german = true;
	public bool German { get => _german; set => _german = value; }

	private bool _spanish = true;
	public bool Spanish { get => _spanish; set => _spanish = value; }

	private bool _brazilianPortuguese = true;
	public bool BrazilianPortuguese { get => _brazilianPortuguese; set => _brazilianPortuguese = value; }

	private bool _polish = true;
	public bool Polish { get => _polish; set => _polish = value; }

	private bool _russian = true;
	public bool Russian { get => _russian; set => _russian = value; }

	private bool _korean = true;
	public bool Korean { get => _korean; set => _korean = value; }

	private bool _traditionalChinese = true;
	public bool TraditionalChinese { get => _traditionalChinese; set => _traditionalChinese = value; }

	private bool _simplifiedChinese = true;
	public bool SimplifiedChinese { get => _simplifiedChinese; set => _simplifiedChinese = value; }

	private bool _arabic = true;
	public bool Arabic { get => _arabic; set => _arabic = value; }

	private bool _latinAmericanSpanish = true;
	public bool LatinAmericanSpanish { get => _latinAmericanSpanish; set => _latinAmericanSpanish = value; }

	public LanguageFilterOptionCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.FilterOptions))
		{
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Japanese, ref _japanese) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.English, ref _english) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.French, ref _french) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Italian, ref _italian) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.German, ref _german) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Spanish, ref _spanish) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BrazilianPortuguese, ref _brazilianPortuguese) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Polish, ref _polish) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Russian, ref _russian) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Korean, ref _korean) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.TraditionalChinese, ref _traditionalChinese) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SimplifiedChinese, ref _simplifiedChinese) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Arabic, ref _arabic) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.LatinAmericanSpanish, ref _latinAmericanSpanish) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
