using SharpPluginLoader.Core.Entities;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class QuestPreferenceFilter : SingletonAccessor
{
    // Singleton Pattern
    private static readonly QuestPreferenceFilter _singleton = new();

    public static QuestPreferenceFilter Instance => _singleton;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static QuestPreferenceFilter() { }

    // Singleton Pattern End

    public QuestPreferenceFilterCustomization Customization { get; set; }

    private QuestPreferenceFilter() { }

    public QuestPreferenceFilter Init()
    {
        InstantiateSingletons();
        return this;
    }

    public bool Apply(ref string key, ref int value, ref int comparison)
    {
        var comparisonEnum = (LobbyComparison)comparison;

        if (!Customization.Enabled) return false;
        if (Core_I.CurrentSearchType != SearchTypes.Session) return false;
        if (comparisonEnum == LobbyComparison.NotEqual) return false;
        if (!key.Equals(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE)) return false;
        if (comparisonEnum == LobbyComparison.Equal && value != (int)Customization.QuestPreferenceReplacementTargetEnum) return false;

        if (comparisonEnum == LobbyComparison.EqualToOrLessThan
        && value == (int)Targets.SilverRathalos
        && Customization.QuestPreferenceReplacementTargetEnum != Targets.None)
        {
            return false;
        }

        TeaLog.Info("QuestPreferenceFilter: Skipping Original Filter...");

        if (!Customization.FilterOptions.General.None)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping None...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.None, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.Assignments)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Assignments...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Assignments, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.Optional)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Optional...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Optional, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.Investigation)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Investigation...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Investigation, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.TheGuidingLandsExpedition)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping The Guiding Lands Expedition...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.TheGuidingLandsExpedition, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.EventQuests)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Event Quests...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.EventQuests, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.SpecialAssignments)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Special Assignments...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.SpecialAssignments, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.Arena)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Arena...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Arena, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.Expeditions)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Expeditions...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Expeditions, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.TemperedMonsters)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Tempered Monsters...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.TemperedMonsters, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.SmallMonsters)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Small Monsters...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.SmallMonsters, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.GreatJagras)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Great Jagras...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.GreatJagras, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.KuluYaKu)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Kulu-Ya-Ku...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.KuluYaKu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.PukeiPukei)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Pukei-Pukei...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.PukeiPukei, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Barroth)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Barroth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Barroth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Jyuratodus)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Jyuratodus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Jyuratodus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.TobiKadachi)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Tobi-Kadachi...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.TobiKadachi, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Anjanath)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Anjanath...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Anjanath, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Rathian)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Rathian...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Rathian, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.TzitziYaKu)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Tzitzi-Ya-Ku...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.TzitziYaKu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Paolumu)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Paolumu...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Paolumu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.GreatGirros)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping GreatGirros...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.GreatGirros, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Radobaan)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Radobaan...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Radobaan, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Legiana)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Legiana...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Legiana, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Odogaron)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Odogaron...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Odogaron, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Rathalos)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Rathalos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Rathalos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Diablos)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Diablos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Diablos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Kirin)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Kirin...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Kirin, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.ZorahMagdaros)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Zorah Magdaros...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.ZorahMagdaros, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Dodogama)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Dodogama...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Dodogama, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.PinkRathian)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Pink Rathian...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.PinkRathian, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Bazelgeuse)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Bazelgeuse...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Bazelgeuse, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Lavasioth)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Lavasioth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Lavasioth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Uragaan)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Uragaan...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Uragaan, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.AzureRathalos)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Azure Rathalos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.AzureRathalos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.BlackDiablos)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Black Diablos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.BlackDiablos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Nergigante)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Nergigante...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Nergigante, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Teostra)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Teostra...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Teostra, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.KushalaDaora)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Kushala Daora...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.KushalaDaora, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.VaalHazak)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Vaal Hazak...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.VaalHazak, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Xenojiiva)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Xenojiiva...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Xenojiiva, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.KulveTaroth)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Kulve Taroth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.KulveTaroth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.Deviljho)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Deviljho...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Deviljho, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.Lunastra)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Lunastra...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Lunastra, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.Behemoth)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Behemoth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Behemoth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.AncientLeshen)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Ancient Leshen...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.AncientLeshen, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Beotodus)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Beotodus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Beotodus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Banbaro)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Banbaro...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Banbaro, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.ViperTobiKadachi)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Viper Tobi-Kadachi...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.ViperTobiKadachi, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.NightshadePaolumu)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Nightshade Paolumu...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.NightshadePaolumu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.CoralPukeiPukei)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Coral Pukei-Pukei...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.CoralPukeiPukei, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Barioth)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Barioth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Barioth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Nargacuga)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Nargacuga...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Nargacuga, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Glavenus)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Glavenus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Glavenus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Tigrex)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Tigrex...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Tigrex, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Brachydios)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Brachydios...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Brachydios, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.ShriekingLegiana)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Shrieking Legiana...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.ShriekingLegiana, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.FulgurAnjanath)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Fulgur Anjanath...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.FulgurAnjanath, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.AcidicGlavenus)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Acidic Glavenus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.AcidicGlavenus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.EbonyOdogaron)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Ebony Odogaron...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.EbonyOdogaron, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Velkhana)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Velkhana...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Velkhana, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.SeethingBazelgeuse)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Seething Bazelgeuse...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.SeethingBazelgeuse, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.BlackveilVaalHazak)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Blackveil Vaal Hazak...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.BlackveilVaalHazak, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Namielle)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Namielle...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Namielle, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.RuinerNergigante)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Ruiner Nergigante...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.RuinerNergigante, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.SharaIshvalda)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Shara Ishvalda...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.SharaIshvalda, LobbyComparison.NotEqual);
        }



        if (!Customization.FilterOptions.IceborneEndgameMonsters.SavageDeviljho)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Savage Deviljho...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.SavageDeviljho, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.BruteTigrex)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Brute Tigrex...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.BruteTigrex, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Zinogre)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Zinogre...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Zinogre, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.YianGaruga)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Yian Garuga...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.YianGaruga, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.ScarredYianGaruga)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Scarred Yian Garuga...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.ScarredYianGaruga, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.GoldRathian)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Gold Rathian...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.GoldRathian, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.SilverRathalos)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Silver Rathalos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.SilverRathalos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Rajang)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Rajang...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Rajang, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.StygianZinogre)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Stygian Zinogre...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.StygianZinogre, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.FuriousRajang)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Furious Rajang...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.FuriousRajang, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.RagingBrachydios)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Raging Brachydios...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.RagingBrachydios, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.FrostfangBarioth)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Frostfang Barioth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.FrostfangBarioth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Alatreon)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Alatreon...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Alatreon, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Safijiiva)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Safi'jiiva...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Safijiiva, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Fatalis)
        {
            TeaLog.Info("QuestPreferenceFilter: Skipping Fatalis...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE, (int)Targets.Fatalis, LobbyComparison.NotEqual);
        }

        return true;
    }
}
