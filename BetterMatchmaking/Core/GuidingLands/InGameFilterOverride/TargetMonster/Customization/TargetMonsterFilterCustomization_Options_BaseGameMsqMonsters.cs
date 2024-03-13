using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class TargetMonsterFilterCustomization_Options_BaseGameMsqMonsters : SingletonAccessor
{
	private bool _greatJagras = true;
	public bool GreatJagras { get => _greatJagras; set => _greatJagras = value; }

	private bool _kuluYaKu = true;
	public bool KuluYaKu { get => _kuluYaKu; set => _kuluYaKu = value; }

	private bool _pukeiPukei = true;
	public bool PukeiPukei { get => _pukeiPukei; set => _pukeiPukei = value; }

	private bool _barroth = true;
	public bool Barroth { get => _barroth; set => _barroth = value; }

	private bool _tobiKadachi = true;
	public bool TobiKadachi { get => _tobiKadachi; set => _tobiKadachi = value; }

	private bool _anjanath = true;
	public bool Anjanath { get => _anjanath; set => _anjanath = value; }

	private bool _rathian = true;
	public bool Rathian { get => _rathian; set => _rathian = value; }

	private bool _tzitziYaKu = true;
	public bool TzitziYaKu { get => _tzitziYaKu; set => _tzitziYaKu = value; }

	private bool _paolumu = true;
	public bool Paolumu { get => _paolumu; set => _paolumu = value; }

	private bool _greatGirros = true;
	public bool GreatGirros { get => _greatGirros; set => _greatGirros = value; }

	private bool _radobaan = true;
	public bool Radobaan { get => _radobaan; set => _radobaan = value; }

	private bool _legiana = true;
	public bool Legiana { get => _legiana; set => _legiana = value; }

	private bool _odogaron = true;
	public bool Odogaron { get => _odogaron; set => _odogaron = value; }

	private bool _rathalos = true;
	public bool Rathalos { get => _rathalos; set => _rathalos = value; }

	private bool _diablos = true;
	public bool Diablos { get => _diablos; set => _diablos = value; }

	private bool _kirin = true;
	public bool Kirin { get => _kirin; set => _kirin = value; }

	private bool _dodogama = true;
	public bool Dodogama { get => _dodogama; set => _dodogama = value; }

	private bool _pinkRathian = true;
	public bool PinkRathian { get => _pinkRathian; set => _pinkRathian = value; }

	private bool _lavasioth = true;
	public bool Lavasioth { get => _lavasioth; set => _lavasioth = value; }

	private bool _uragaan = true;
	public bool Uragaan { get => _uragaan; set => _uragaan = value; }

	private bool _azureRathalos = true;
	public bool AzureRathalos { get => _azureRathalos; set => _azureRathalos = value; }

	private bool _blackDiablos = true;
	public bool BlackDiablos { get => _blackDiablos; set => _blackDiablos = value; }

	private bool _teostra = true;
	public bool Teostra { get => _teostra; set => _teostra = value; }

	private bool _kushalaDaora = true;
	public bool KushalaDaora { get => _kushalaDaora; set => _kushalaDaora = value; }

	public TargetMonsterFilterCustomization_Options_BaseGameMsqMonsters()
	{
		InstantiateSingletons();
	}

	public TargetMonsterFilterCustomization_Options_BaseGameMsqMonsters SelectAll()
	{
		GreatJagras = true;
		KuluYaKu = true;
		PukeiPukei = true;
		Barroth = true;
		TobiKadachi = true;
		Anjanath = true;
		Rathian = true;
		TzitziYaKu = true;
		Paolumu = true;
		GreatGirros = true;
		Radobaan = true;
		Legiana = true;
		Odogaron = true;
		Rathalos = true;
		Diablos = true;
		Kirin = true;
		Dodogama = true;
		PinkRathian = true;
		Lavasioth = true;
		Uragaan = true;
		AzureRathalos = true;
		BlackDiablos = true;
		Teostra = true;
		KushalaDaora = true;

		return this;
	}

	public TargetMonsterFilterCustomization_Options_BaseGameMsqMonsters DeselectAll()
	{
		GreatJagras = false;
		KuluYaKu = false;
		PukeiPukei = false;
		Barroth = false;
		TobiKadachi = false;
		Anjanath = false;
		Rathian = false;
		TzitziYaKu = false;
		Paolumu = false;
		GreatGirros = false;
		Radobaan = false;
		Legiana = false;
		Odogaron = false;
		Rathalos = false;
		Diablos = false;
		Kirin = false;
		Dodogama = false;
		PinkRathian = false;
		Lavasioth = false;
		Uragaan = false;
		AzureRathalos = false;
		BlackDiablos = false;
		Teostra = false;
		KushalaDaora = false;

		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.BaseGameMSQMonsters))
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

			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.GreatJagras, ref _greatJagras) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.KuluYaKu, ref _kuluYaKu) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.PukeiPukei, ref _pukeiPukei) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Barroth, ref _barroth) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.TobiKadachi, ref _tobiKadachi) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Anjanath, ref _anjanath) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Rathian, ref _rathian) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.TzitziYaKu, ref _tzitziYaKu) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Paolumu, ref _paolumu) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.GreatGirros, ref _greatGirros) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Radobaan, ref _radobaan) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Legiana, ref _legiana) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Odogaron, ref _odogaron) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Rathalos, ref _rathalos) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Diablos, ref _diablos) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Kirin, ref _kirin) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Dodogama, ref _dodogama) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.PinkRathian, ref _pinkRathian) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Lavasioth, ref _lavasioth) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Uragaan, ref _uragaan) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.AzureRathalos, ref _azureRathalos) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BlackDiablos, ref _blackDiablos) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Teostra, ref _teostra) || changed;
			changed = ImGui.Checkbox(LocalizationManager_I.ImGui.KushalaDaora, ref _kushalaDaora) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}