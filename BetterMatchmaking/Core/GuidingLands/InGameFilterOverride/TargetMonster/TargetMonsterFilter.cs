using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class TargetMonsterFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly TargetMonsterFilter _singleton = new();

	public static TargetMonsterFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static TargetMonsterFilter() { }

	// Singleton Pattern End

	public TargetMonsterFilterCustomization Customization { get; set; }

	private TargetMonsterFilter() { }

	public TargetMonsterFilter Init()
	{
		InstantiateSingletons();
		return this;
	}

	private TargetMonsterFilter Apply()
	{
		var filterOptions = Customization.FilterOptions;
		var generalFilterOptions = filterOptions.General;
		var baseGameMSQMonsters = filterOptions.BaseGameMsqMonsters;
		var baseGameEndgameMonsters = filterOptions.BaseGameEndgameMonsters;
		var iceborneMsqMonsters = filterOptions.IceborneMSQMonsters;
		var iceborneEndgameMonsters = filterOptions.IceborneEndgameMonsters;

		if(!generalFilterOptions.None)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping None...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.None, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.GreatJagras)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Great Jagras...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.GreatJagras, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.KuluYaKu)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Kulu-Ya-Ku...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.KuluYaKu, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.PukeiPukei)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Pukei-Pukei...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.PukeiPukei, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Barroth)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Barroth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Barroth, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.TobiKadachi)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Tobi-Kadachi...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.TobiKadachi, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Anjanath)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Anjanath...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Anjanath, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Rathian)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Rathian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Rathian, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.TzitziYaKu)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Tzitzi-Ya-Ku...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.TzitziYaKu, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Paolumu)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Paolumu...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Paolumu, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.GreatGirros)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping GreatGirros...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.GreatGirros, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Radobaan)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Radobaan...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Radobaan, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Legiana)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Legiana...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Legiana, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Odogaron)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Odogaron...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Odogaron, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Rathalos)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Rathalos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Rathalos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Diablos)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Diablos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Diablos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Kirin)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Kirin...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Kirin, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Dodogama)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Dodogama...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Dodogama, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.PinkRathian)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Pink Rathian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.PinkRathian, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Lavasioth)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Lavasioth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Lavasioth, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Uragaan)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Uragaan...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Uragaan, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.AzureRathalos)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Azure Rathalos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.AzureRathalos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.BlackDiablos)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Black Diablos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.BlackDiablos, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.Teostra)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Teostra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Teostra, LobbyComparison.NotEqual);
		}

		if(!baseGameMSQMonsters.KushalaDaora)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Kushala Daora...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.KushalaDaora, LobbyComparison.NotEqual);
		}

		if(!baseGameEndgameMonsters.Lunastra)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Lunastra...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Lunastra, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Banbaro)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Banbaro...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Banbaro, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.ViperTobiKadachi)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Viper Tobi-Kadachi...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.ViperTobiKadachi, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.NightshadePaolumu)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Nightshade Paolumu...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.NightshadePaolumu, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.CoralPukeiPukei)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Coral Pukei-Pukei...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.CoralPukeiPukei, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Barioth)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Barioth...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Barioth, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Nargacuga)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Nargacuga...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Nargacuga, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Glavenus)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Glavenus...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Glavenus, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Tigrex)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Tigrex...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Tigrex, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Brachydios)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Brachydios...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Brachydios, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.ShriekingLegiana)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Shrieking Legiana...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.ShriekingLegiana, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.FulgurAnjanath)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Fulgur Anjanath...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.FulgurAnjanath, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.AcidicGlavenus)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Acidic Glavenus...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.AcidicGlavenus, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.EbonyOdogaron)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Ebony Odogaron...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.EbonyOdogaron, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Velkhana)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Velkhana...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Velkhana, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.SeethingBazelgeuse)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Seething Bazelgeuse...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.SeethingBazelgeuse, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.BlackveilVaalHazak)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Blackveil Vaal Hazak...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.BlackveilVaalHazak, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.Namielle)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Namielle...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Namielle, LobbyComparison.NotEqual);
		}

		if(!iceborneMsqMonsters.RuinerNergigante)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Ruiner Nergigante...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.RuinerNergigante, LobbyComparison.NotEqual);
		}



		if(!iceborneEndgameMonsters.SavageDeviljho)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Savage Deviljho...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.SavageDeviljho, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.BruteTigrex)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Brute Tigrex...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.BruteTigrex, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Zinogre)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Zinogre...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Zinogre, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.YianGaruga)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Yian Garuga...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.YianGaruga, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.ScarredYianGaruga)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Scarred Yian Garuga...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.ScarredYianGaruga, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.GoldRathian)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Gold Rathian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.GoldRathian, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.SilverRathalos)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Silver Rathalos...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.SilverRathalos, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.Rajang)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Rajang...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.Rajang, LobbyComparison.NotEqual);
		}

		if(!iceborneEndgameMonsters.StygianZinogre)
		{
			TeaLog.Info("TargetMonsterFilter: Skipping Stygian Zinogre...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER, (int) Targets.StygianZinogre, LobbyComparison.NotEqual);
		}

		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		if(!Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.GuidingLands) return false;
		if(Core_I.IsTargetMonsterNoPreference) return false;
		if(comparison != (int) LobbyComparison.Equal) return false;
		if(!key.Equals(Constants.SEARCH_KEY_GUIDING_LANDS_TARGET_MONSTER)) return false;
		if(value != (int) Customization.ReplacementTargetEnum) return false;

		TeaLog.Info("TargetMonsterFilter: Skipping Original Filter...");
		Apply();

		return true;
	}

	public TargetMonsterFilter ApplyNoPreference()
	{
		if(!Core_I.IsTargetMonsterNoPreference) return this;
		if(!Customization.Enabled) return this;
		if(Core_I.CurrentSearchType != SearchTypes.GuidingLands) return this;
		if(Customization.ReplacementTargetEnum != Targets.None) return this;

		Apply();

		return this;
	}
}
