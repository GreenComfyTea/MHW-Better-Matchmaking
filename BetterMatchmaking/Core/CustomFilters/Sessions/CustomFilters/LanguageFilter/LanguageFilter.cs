using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class LanguageFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly LanguageFilter _singleton = new();

	public static LanguageFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static LanguageFilter() { }

	// Singleton Pattern End

	public bool SkipNext { get; set; } = true;

	public LanguageFilterCustomization Customization { get; set; }

	private LanguageFilter() { }

	public LanguageFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	private LanguageFilter Apply()
	{
		if(!Customization.FilterOptions.Japanese)
		{
			TeaLog.Info($"LanguageFilter: Skipping Japanese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Japanese, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.English)
		{
			TeaLog.Info($"LanguageFilter: Skipping English...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.English, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.French)
		{
			TeaLog.Info($"LanguageFilter: Skipping French...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.French, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Italian)
		{
			TeaLog.Info($"LanguageFilter: Skipping Italian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Italian, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.German)
		{
			TeaLog.Info($"LanguageFilter: Skipping German...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.German, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Spanish)
		{
			TeaLog.Info($"LanguageFilter: Skipping Spanish...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Spanish, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.BrazilianPortuguese)
		{
			TeaLog.Info($"LanguageFilter: Skipping Brazilian Portuguese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.BrazilianPortuguese, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Polish)
		{
			TeaLog.Info($"LanguageFilter: Skipping Polish...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Polish, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Russian)
		{
			TeaLog.Info($"LanguageFilter: Skipping Russian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Russian, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Korean)
		{
			TeaLog.Info($"LanguageFilter: Skipping Korean...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Korean, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.TraditionalChinese)
		{
			TeaLog.Info($"LanguageFilter: Skipping Traditional Chinese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.TraditionalChinese, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.SimplifiedChinese)
		{
			TeaLog.Info($"LanguageFilter: Skipping Simplified Chinese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.SimplifiedChinese, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.Arabic)
		{
			TeaLog.Info($"LanguageFilter: Skipping Arabic...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.Arabic, LobbyComparison.NotEqual);
		}

		if(!Customization.FilterOptions.LatinAmericanSpanish)
		{
			TeaLog.Info($"LanguageFilter: Skipping Latin-American Spanish...");
			Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_LANGUAGE, (int) Languages.LatinAmericanSpanish, LobbyComparison.NotEqual);
		}

		return this;
	}

	public bool ApplySameLanguage(ref string key, ref int value, ref int comparison)
	{
		if(SkipNext) return false;
		if(!Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.Session) return false;
		if(!key.Equals(Constants.SEARCH_KEY_SESSION_LANGUAGE)) return false;

		SkipNext = true;

		if(Customization.LanguageReplacementTargetEnum != LanguageSearchTypes.SameLanguage) return false;
		if((LobbyComparison) comparison != LobbyComparison.Equal) return false;

		TeaLog.Info($"LanguageFilter: Same Language -> Skipping Original Call...");
		SkipNext = true;
		Apply();

		return true;
	}

	public bool ApplyAnyLanguage(ref string key, ref int value, ref int comparison)
	{
		if(SkipNext) return false;
		if(!Customization.Enabled) return false;
		if(Customization.LanguageReplacementTargetEnum != LanguageSearchTypes.AnyLanguage) return false;
		if(Core_I.CurrentSearchType != SearchTypes.Session) return false;
		if(!key.Equals(Constants.SEARCH_KEY_SESSION_QUEST_PREFERENCE)) return false;

		TeaLog.Info($"LanguageFilter: Any Language -> Skipping Original Call...");
		SkipNext = true;
		Apply();

		return true;
	}

}
