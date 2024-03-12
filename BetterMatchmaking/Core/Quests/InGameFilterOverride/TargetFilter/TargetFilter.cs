using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class TargetFilter : SingletonAccessor
{
    // Singleton Pattern
    private static readonly TargetFilter _singleton = new();

    public static TargetFilter Instance => _singleton;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static TargetFilter() { }

    // Singleton Pattern End

    public TargetFilterCustomization Customization { get; set; }

    private TargetFilter() { }

    public TargetFilter Init()
    {
        InstantiateSingletons();
        return this;
    }

    public bool Apply(ref string key, ref int value, ref int comparison)
    {
        var comparisonEnum = (LobbyComparison)comparison;

        if (!Customization.Enabled) return false;
        if (Core_I.CurrentSearchType != SearchTypes.Quest) return false;
        if (comparisonEnum == LobbyComparison.NotEqual) return false;
        if (!key.Equals(Constants.SEARCH_KEY_QUEST_TARGET)) return false;
        if (comparisonEnum == LobbyComparison.Equal && value != (int)Customization.TargetReplacementTargetEnum) return false;

        if (comparisonEnum == LobbyComparison.EqualToOrLessThan
        && value == (int)Targets.SilverRathalos
        && Customization.TargetReplacementTargetEnum != Targets.None)
        {
            return false;
        }

        TeaLog.Info("TargetFilter: Skipping Original Filter...");

        if (!Customization.FilterOptions.General.None)
        {
            TeaLog.Info("TargetFilter: Skipping None...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.None, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.General.SmallMonsters)
        {
            TeaLog.Info("TargetFilter: Skipping Small Monsters...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.SmallMonsters, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.GreatJagras)
        {
            TeaLog.Info("TargetFilter: Skipping Great Jagras...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.GreatJagras, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.KuluYaKu)
        {
            TeaLog.Info("TargetFilter: Skipping Kulu-Ya-Ku...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.KuluYaKu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.PukeiPukei)
        {
            TeaLog.Info("TargetFilter: Skipping Pukei-Pukei...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.PukeiPukei, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Barroth)
        {
            TeaLog.Info("TargetFilter: Skipping Barroth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Barroth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Jyuratodus)
        {
            TeaLog.Info("TargetFilter: Skipping Jyuratodus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Jyuratodus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.TobiKadachi)
        {
            TeaLog.Info("TargetFilter: Skipping Tobi-Kadachi...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.TobiKadachi, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Anjanath)
        {
            TeaLog.Info("TargetFilter: Skipping Anjanath...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Anjanath, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Rathian)
        {
            TeaLog.Info("TargetFilter: Skipping Rathian...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Rathian, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.TzitziYaKu)
        {
            TeaLog.Info("TargetFilter: Skipping Tzitzi-Ya-Ku...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.TzitziYaKu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Paolumu)
        {
            TeaLog.Info("TargetFilter: Skipping Paolumu...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Paolumu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.GreatGirros)
        {
            TeaLog.Info("TargetFilter: Skipping GreatGirros...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.GreatGirros, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Radobaan)
        {
            TeaLog.Info("TargetFilter: Skipping Radobaan...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Radobaan, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Legiana)
        {
            TeaLog.Info("TargetFilter: Skipping Legiana...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Legiana, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Odogaron)
        {
            TeaLog.Info("TargetFilter: Skipping Odogaron...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Odogaron, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Rathalos)
        {
            TeaLog.Info("TargetFilter: Skipping Rathalos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Rathalos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Diablos)
        {
            TeaLog.Info("TargetFilter: Skipping Diablos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Diablos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Kirin)
        {
            TeaLog.Info("TargetFilter: Skipping Kirin...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Kirin, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.ZorahMagdaros)
        {
            TeaLog.Info("TargetFilter: Skipping Zorah Magdaros...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.ZorahMagdaros, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Dodogama)
        {
            TeaLog.Info("TargetFilter: Skipping Dodogama...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Dodogama, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.PinkRathian)
        {
            TeaLog.Info("TargetFilter: Skipping Pink Rathian...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.PinkRathian, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Bazelgeuse)
        {
            TeaLog.Info("TargetFilter: Skipping Bazelgeuse...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Bazelgeuse, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Lavasioth)
        {
            TeaLog.Info("TargetFilter: Skipping Lavasioth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Lavasioth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Uragaan)
        {
            TeaLog.Info("TargetFilter: Skipping Uragaan...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Uragaan, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.AzureRathalos)
        {
            TeaLog.Info("TargetFilter: Skipping Azure Rathalos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.AzureRathalos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.BlackDiablos)
        {
            TeaLog.Info("TargetFilter: Skipping Black Diablos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.BlackDiablos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Nergigante)
        {
            TeaLog.Info("TargetFilter: Skipping Nergigante...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Nergigante, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Teostra)
        {
            TeaLog.Info("TargetFilter: Skipping Teostra...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Teostra, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.KushalaDaora)
        {
            TeaLog.Info("TargetFilter: Skipping Kushala Daora...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.KushalaDaora, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.VaalHazak)
        {
            TeaLog.Info("TargetFilter: Skipping Vaal Hazak...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.VaalHazak, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameMSQMonsters.Xenojiiva)
        {
            TeaLog.Info("TargetFilter: Skipping Xenojiiva...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Xenojiiva, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.KulveTaroth)
        {
            TeaLog.Info("TargetFilter: Skipping Kulve Taroth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.KulveTaroth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.Deviljho)
        {
            TeaLog.Info("TargetFilter: Skipping Deviljho...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Deviljho, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.Lunastra)
        {
            TeaLog.Info("TargetFilter: Skipping Lunastra...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Lunastra, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.Behemoth)
        {
            TeaLog.Info("TargetFilter: Skipping Behemoth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Behemoth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.BaseGameEndgameMonsters.AncientLeshen)
        {
            TeaLog.Info("TargetFilter: Skipping Ancient Leshen...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.AncientLeshen, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Beotodus)
        {
            TeaLog.Info("TargetFilter: Skipping Beotodus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Beotodus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Banbaro)
        {
            TeaLog.Info("TargetFilter: Skipping Banbaro...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Banbaro, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.ViperTobiKadachi)
        {
            TeaLog.Info("TargetFilter: Skipping Viper Tobi-Kadachi...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.ViperTobiKadachi, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.NightshadePaolumu)
        {
            TeaLog.Info("TargetFilter: Skipping Nightshade Paolumu...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.NightshadePaolumu, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.CoralPukeiPukei)
        {
            TeaLog.Info("TargetFilter: Skipping Coral Pukei-Pukei...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.CoralPukeiPukei, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Barioth)
        {
            TeaLog.Info("TargetFilter: Skipping Barioth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Barioth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Nargacuga)
        {
            TeaLog.Info("TargetFilter: Skipping Nargacuga...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Nargacuga, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Glavenus)
        {
            TeaLog.Info("TargetFilter: Skipping Glavenus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Glavenus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Tigrex)
        {
            TeaLog.Info("TargetFilter: Skipping Tigrex...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Tigrex, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Brachydios)
        {
            TeaLog.Info("TargetFilter: Skipping Brachydios...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Brachydios, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.ShriekingLegiana)
        {
            TeaLog.Info("TargetFilter: Skipping Shrieking Legiana...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.ShriekingLegiana, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.FulgurAnjanath)
        {
            TeaLog.Info("TargetFilter: Skipping Fulgur Anjanath...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.FulgurAnjanath, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.AcidicGlavenus)
        {
            TeaLog.Info("TargetFilter: Skipping Acidic Glavenus...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.AcidicGlavenus, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.EbonyOdogaron)
        {
            TeaLog.Info("TargetFilter: Skipping Ebony Odogaron...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.EbonyOdogaron, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Velkhana)
        {
            TeaLog.Info("TargetFilter: Skipping Velkhana...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Velkhana, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.SeethingBazelgeuse)
        {
            TeaLog.Info("TargetFilter: Skipping Seething Bazelgeuse...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.SeethingBazelgeuse, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.BlackveilVaalHazak)
        {
            TeaLog.Info("TargetFilter: Skipping Blackveil Vaal Hazak...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.BlackveilVaalHazak, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.Namielle)
        {
            TeaLog.Info("TargetFilter: Skipping Namielle...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Namielle, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.RuinerNergigante)
        {
            TeaLog.Info("TargetFilter: Skipping Ruiner Nergigante...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.RuinerNergigante, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneMSQMonsters.SharaIshvalda)
        {
            TeaLog.Info("TargetFilter: Skipping Shara Ishvalda...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.SharaIshvalda, LobbyComparison.NotEqual);
        }



        if (!Customization.FilterOptions.IceborneEndgameMonsters.SavageDeviljho)
        {
            TeaLog.Info("TargetFilter: Skipping Savage Deviljho...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.SavageDeviljho, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.BruteTigrex)
        {
            TeaLog.Info("TargetFilter: Skipping Brute Tigrex...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.BruteTigrex, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Zinogre)
        {
            TeaLog.Info("TargetFilter: Skipping Zinogre...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Zinogre, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.YianGaruga)
        {
            TeaLog.Info("TargetFilter: Skipping Yian Garuga...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.YianGaruga, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.ScarredYianGaruga)
        {
            TeaLog.Info("TargetFilter: Skipping Scarred Yian Garuga...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.ScarredYianGaruga, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.GoldRathian)
        {
            TeaLog.Info("TargetFilter: Skipping Gold Rathian...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.GoldRathian, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.SilverRathalos)
        {
            TeaLog.Info("TargetFilter: Skipping Silver Rathalos...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.SilverRathalos, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Rajang)
        {
            TeaLog.Info("TargetFilter: Skipping Rajang...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Rajang, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.StygianZinogre)
        {
            TeaLog.Info("TargetFilter: Skipping Stygian Zinogre...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.StygianZinogre, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.FuriousRajang)
        {
            TeaLog.Info("TargetFilter: Skipping Furious Rajang...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.FuriousRajang, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.RagingBrachydios)
        {
            TeaLog.Info("TargetFilter: Skipping Raging Brachydios...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.RagingBrachydios, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.FrostfangBarioth)
        {
            TeaLog.Info("TargetFilter: Skipping Frostfang Barioth...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.FrostfangBarioth, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Alatreon)
        {
            TeaLog.Info("TargetFilter: Skipping Alatreon...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Alatreon, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Safijiiva)
        {
            TeaLog.Info("TargetFilter: Skipping Safi'jiiva...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Safijiiva, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.IceborneEndgameMonsters.Fatalis)
        {
            TeaLog.Info("TargetFilter: Skipping Fatalis...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_QUEST_TARGET, (int)Targets.Fatalis, LobbyComparison.NotEqual);
        }

        return true;
    }
}
