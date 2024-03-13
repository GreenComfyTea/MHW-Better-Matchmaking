using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestPreferenceTargetFilterOptionCustomization_IceborneMSQMonsters : SingletonAccessor
{
    private bool _beotodus = true;
    public bool Beotodus { get => _beotodus; set => _beotodus = value; }

    private bool _banbaro = true;
    public bool Banbaro { get => _banbaro; set => _banbaro = value; }

    private bool _viperTobiKadachi = true;
    public bool ViperTobiKadachi { get => _viperTobiKadachi; set => _viperTobiKadachi = value; }

    private bool _nightshadePaolumu = true;
    public bool NightshadePaolumu { get => _nightshadePaolumu; set => _nightshadePaolumu = value; }

    private bool _coralPukeiPukei = true;
    public bool CoralPukeiPukei { get => _coralPukeiPukei; set => _coralPukeiPukei = value; }

    private bool _barioth = true;
    public bool Barioth { get => _barioth; set => _barioth = value; }

    private bool _nargacuga = true;
    public bool Nargacuga { get => _nargacuga; set => _nargacuga = value; }

    private bool _glavenus = true;
    public bool Glavenus { get => _glavenus; set => _glavenus = value; }

    private bool _tigrex = true;
    public bool Tigrex { get => _tigrex; set => _tigrex = value; }

    private bool _brachydios = true;
    public bool Brachydios { get => _brachydios; set => _brachydios = value; }

    private bool _shriekingLegiana = true;
    public bool ShriekingLegiana { get => _shriekingLegiana; set => _shriekingLegiana = value; }

    private bool _fulgurAnjanath = true;
    public bool FulgurAnjanath { get => _fulgurAnjanath; set => _fulgurAnjanath = value; }

    private bool _acidicGlavenus = true;
    public bool AcidicGlavenus { get => _acidicGlavenus; set => _acidicGlavenus = value; }

    private bool _ebonyOdogaron = true;
    public bool EbonyOdogaron { get => _ebonyOdogaron; set => _ebonyOdogaron = value; }

    private bool _velkhana = true;
    public bool Velkhana { get => _velkhana; set => _velkhana = value; }

    private bool _seethingBazelgeuse = true;
    public bool SeethingBazelgeuse { get => _seethingBazelgeuse; set => _seethingBazelgeuse = value; }

    private bool _blackveilVaalHazak = true;
    public bool BlackveilVaalHazak { get => _blackveilVaalHazak; set => _blackveilVaalHazak = value; }

    private bool _namielle = true;
    public bool Namielle { get => _namielle; set => _namielle = value; }

    private bool _ruinerNergigante = true;
    public bool RuinerNergigante { get => _ruinerNergigante; set => _ruinerNergigante = value; }

    private bool _sharaIshvalda = true;
    public bool SharaIshvalda { get => _sharaIshvalda; set => _sharaIshvalda = value; }

    public QuestPreferenceTargetFilterOptionCustomization_IceborneMSQMonsters()
    {
        InstantiateSingletons();
    }

    public QuestPreferenceTargetFilterOptionCustomization_IceborneMSQMonsters SelectAll()
    {
        Beotodus = true;
        Banbaro = true;
        ViperTobiKadachi = true;
        NightshadePaolumu = true;
        CoralPukeiPukei = true;
        Barioth = true;
        Nargacuga = true;
        Glavenus = true;
        Tigrex = true;
        Brachydios = true;
        ShriekingLegiana = true;
        FulgurAnjanath = true;
        AcidicGlavenus = true;
        EbonyOdogaron = true;
        Velkhana = true;
        SeethingBazelgeuse = true;
        BlackveilVaalHazak = true;
        Namielle = true;
        RuinerNergigante = true;
        SharaIshvalda = true;

        return this;
    }

    public QuestPreferenceTargetFilterOptionCustomization_IceborneMSQMonsters DeselectAll()
    {
        Beotodus = false;
        Banbaro = false;
        ViperTobiKadachi = false;
        NightshadePaolumu = false;
        CoralPukeiPukei = false;
        Barioth = false;
        Nargacuga = false;
        Glavenus = false;
        Tigrex = false;
        Brachydios = false;
        ShriekingLegiana = false;
        FulgurAnjanath = false;
        AcidicGlavenus = false;
        EbonyOdogaron = false;
        Velkhana = false;
        SeethingBazelgeuse = false;
        BlackveilVaalHazak = false;
        Namielle = false;
        RuinerNergigante = false;
        SharaIshvalda = false;

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;

        if (ImGui.TreeNode(LocalizationManager_I.ImGui.IceborneMSQMonsters))
        {
            if (ImGui.Button(LocalizationManager_I.ImGui.SelectAll))
            {
                SelectAll();
                changed = true;
            }

            ImGui.SameLine();

            if (ImGui.Button(LocalizationManager_I.ImGui.DeselectAll))
            {
                DeselectAll();
                changed = true;
            }

            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Beotodus, ref _beotodus) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Banbaro, ref _banbaro) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.ViperTobiKadachi, ref _viperTobiKadachi) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.NightshadePaolumu, ref _nightshadePaolumu) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.CoralPukeiPukei, ref _coralPukeiPukei) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Barioth, ref _barioth) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Nargacuga, ref _nargacuga) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Glavenus, ref _glavenus) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Tigrex, ref _tigrex) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Brachydios, ref _brachydios) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.ShriekingLegiana, ref _shriekingLegiana) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.FulgurAnjanath, ref _fulgurAnjanath) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.AcidicGlavenus, ref _acidicGlavenus) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.EbonyOdogaron, ref _ebonyOdogaron) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Velkhana, ref _velkhana) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SeethingBazelgeuse, ref _seethingBazelgeuse) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.BlackveilVaalHazak, ref _blackveilVaalHazak) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.Namielle, ref _namielle) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.RuinerNergigante, ref _ruinerNergigante) || changed;
            changed = ImGui.Checkbox(LocalizationManager_I.ImGui.SharaIshvalda, ref _sharaIshvalda) || changed;

            ImGui.TreePop();
        }

        return changed;
    }
}