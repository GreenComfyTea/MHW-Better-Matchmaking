using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class QuestPreferenceTargetFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly QuestPreferenceTargetFilter _singleton = new();

	public static QuestPreferenceTargetFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static QuestPreferenceTargetFilter() { }

	// Singleton Pattern End

	private QuestPreferenceTargetFilter() { }

	public QuestPreferenceTargetFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	public QuestPreferenceTargetFilter Apply(
		string searchKey,
		QuestPreferenceTargetFilterOptionCustomization_BaseGameMsqMonsters baseGameMSQMonsters,
		QuestPreferenceTargetFilterOptionCustomization_BaseGameEndgameMonsters baseGameEndgameMonsters,
		QuestPreferenceTargetFilterOptionCustomization_IceborneMSQMonsters iceborneMSQMonsters,
		QuestPreferenceTargetFilterOptionCustomization_IceborneEndgameMonsters iceborneEndgameMonsters
	)
	{

		if(!baseGameMSQMonsters.GreatJagras)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Great Jagras...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.GreatJagras, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.KuluYaKu)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Kulu-Ya-Ku...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.KuluYaKu, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.PukeiPukei)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Pukei-Pukei...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.PukeiPukei, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Barroth)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Barroth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Barroth, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Jyuratodus)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Jyuratodus...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Jyuratodus, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.TobiKadachi)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Tobi-Kadachi...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.TobiKadachi, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Anjanath)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Anjanath...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Anjanath, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Rathian)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Rathian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Rathian, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.TzitziYaKu)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Tzitzi-Ya-Ku...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.TzitziYaKu, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Paolumu)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Paolumu...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Paolumu, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.GreatGirros)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping GreatGirros...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.GreatGirros, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Radobaan)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Radobaan...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Radobaan, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Legiana)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Legiana...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Legiana, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Odogaron)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Odogaron...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Odogaron, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Rathalos)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Rathalos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Rathalos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Diablos)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Diablos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Diablos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Kirin)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Kirin...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Kirin, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.ZorahMagdaros)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Zorah Magdaros...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.ZorahMagdaros, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Dodogama)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Dodogama...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Dodogama, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.PinkRathian)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Pink Rathian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.PinkRathian, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Bazelgeuse)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Bazelgeuse...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Bazelgeuse, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Lavasioth)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Lavasioth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Lavasioth, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Uragaan)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Uragaan...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Uragaan, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.AzureRathalos)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Azure Rathalos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.AzureRathalos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.BlackDiablos)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Black Diablos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.BlackDiablos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Nergigante)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Nergigante...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Nergigante, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Teostra)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Teostra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Teostra, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.KushalaDaora)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Kushala Daora...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.KushalaDaora, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.VaalHazak)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Vaal Hazak...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.VaalHazak, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Xenojiiva)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Xenojiiva...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Xenojiiva, LobbyComparison.NotEqual);
		}

		if(!baseGameEndgameMonsters.KulveTaroth)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Kulve Taroth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.KulveTaroth, LobbyComparison.NotEqual);
		}

		if(!baseGameEndgameMonsters.Deviljho)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Deviljho...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Deviljho, LobbyComparison.NotEqual);
		}

		if(!baseGameEndgameMonsters.Lunastra)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Lunastra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Lunastra, LobbyComparison.NotEqual);
		}

		if(!baseGameEndgameMonsters.Behemoth)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Behemoth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Behemoth, LobbyComparison.NotEqual);
		}

		if(!baseGameEndgameMonsters.AncientLeshen)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Ancient Leshen...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.AncientLeshen, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Beotodus)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Beotodus...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Beotodus, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Banbaro)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Banbaro...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Banbaro, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.ViperTobiKadachi)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Viper Tobi-Kadachi...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.ViperTobiKadachi, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.NightshadePaolumu)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Nightshade Paolumu...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.NightshadePaolumu, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.CoralPukeiPukei)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Coral Pukei-Pukei...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.CoralPukeiPukei, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Barioth)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Barioth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Barioth, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Nargacuga)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Nargacuga...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Nargacuga, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Glavenus)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Glavenus...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Glavenus, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Tigrex)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Tigrex...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Tigrex, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Brachydios)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Brachydios...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Brachydios, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.ShriekingLegiana)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Shrieking Legiana...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.ShriekingLegiana, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.FulgurAnjanath)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Fulgur Anjanath...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.FulgurAnjanath, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.AcidicGlavenus)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Acidic Glavenus...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.AcidicGlavenus, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.EbonyOdogaron)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Ebony Odogaron...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.EbonyOdogaron, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Velkhana)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Velkhana...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Velkhana, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.SeethingBazelgeuse)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Seething Bazelgeuse...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.SeethingBazelgeuse, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.BlackveilVaalHazak)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Blackveil Vaal Hazak...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.BlackveilVaalHazak, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.Namielle)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Namielle...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Namielle, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.RuinerNergigante)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Ruiner Nergigante...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.RuinerNergigante, LobbyComparison.NotEqual);
		}

		if(!iceborneMSQMonsters.SharaIshvalda)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Shara Ishvalda...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.SharaIshvalda, LobbyComparison.NotEqual);
		}



		if(!iceborneEndgameMonsters.SavageDeviljho)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Savage Deviljho...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.SavageDeviljho, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.BruteTigrex)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Brute Tigrex...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.BruteTigrex, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Zinogre)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Zinogre...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Zinogre, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.YianGaruga)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Yian Garuga...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.YianGaruga, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.ScarredYianGaruga)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Scarred Yian Garuga...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.ScarredYianGaruga, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.GoldRathian)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Gold Rathian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.GoldRathian, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.SilverRathalos)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Silver Rathalos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.SilverRathalos, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Rajang)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Rajang...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Rajang, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.StygianZinogre)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Stygian Zinogre...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.StygianZinogre, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.FuriousRajang)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Furious Rajang...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.FuriousRajang, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.RagingBrachydios)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Raging Brachydios...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.RagingBrachydios, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.FrostfangBarioth)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Frostfang Barioth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.FrostfangBarioth, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Safijiiva)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Safi'jiiva...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Safijiiva, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Alatreon)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Alatreon...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Alatreon, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Fatalis)
		{
			TeaLog.Info("QuestPreferenceTargetFilter: Skipping Fatalis...");
			Matchmaking.AddRequestLobbyListNumericalFilter(searchKey, (int) Targets.Fatalis, LobbyComparison.NotEqual);
		}

		return this;
	}
}
