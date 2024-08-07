﻿using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class LanguageFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly LanguageFilter _singleton = new();

	public static LanguageFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static LanguageFilter() { }

	// Singleton Pattern End

	public LanguageFilterCustomization SessionCustomization { get; set; }
	public LanguageFilterCustomization QuestCustomization { get; set; }
	public LanguageFilterCustomization GuidingLandsCustomization { get; set; }

	private LanguageFilter() { }

	public LanguageFilter Init()
	{
		InstantiateSingletons();

		return this;
	}

	private LanguageFilter GetCustomization(out LanguageFilterCustomization customization, out string languageKey)
	{
		customization = null;
		languageKey = string.Empty;

		switch(Core_I.CurrentSearchType)
		{
			case SearchTypes.Session:

				customization = SessionCustomization;
				languageKey = Constants.SEARCH_KEY_SESSION_LANGUAGE;
				break;

			case SearchTypes.Quest:

				customization = QuestCustomization;
				languageKey = Constants.SEARCH_KEY_QUEST_LANGUAGE;

				break;

			case SearchTypes.GuidingLands:

				customization = GuidingLandsCustomization;
				languageKey = Constants.SEARCH_KEY_GUIDING_LANDS_LANGUAGE;

				break;
		}

		return this;
	}

	private LanguageFilter Apply(LanguageFilterCustomization customization, string languageKey)
	{
		var filterOptions = customization.FilterOptions;

		if (!filterOptions.Japanese)
		{
			TeaLog.Info("LanguageFilter: Skipping Japanese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Japanese, LobbyComparison.NotEqual);
		}

		if (!filterOptions.English)
		{
			TeaLog.Info("LanguageFilter: Skipping English...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.English, LobbyComparison.NotEqual);
		}

		if (!filterOptions.French)
		{
			TeaLog.Info("LanguageFilter: Skipping French...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.French, LobbyComparison.NotEqual);
		}

		if (!filterOptions.Italian)
		{
			TeaLog.Info("LanguageFilter: Skipping Italian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Italian, LobbyComparison.NotEqual);
		}

		if (!filterOptions.German)
		{
			TeaLog.Info("LanguageFilter: Skipping German...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.German, LobbyComparison.NotEqual);
		}

		if (!filterOptions.Spanish)
		{
			TeaLog.Info("LanguageFilter: Skipping Spanish...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Spanish, LobbyComparison.NotEqual);
		}

		if (!filterOptions.BrazilianPortuguese)
		{
			TeaLog.Info("LanguageFilter: Skipping Brazilian Portuguese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.BrazilianPortuguese, LobbyComparison.NotEqual);
		}

		if (!filterOptions.Polish)
		{
			TeaLog.Info("LanguageFilter: Skipping Polish...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Polish, LobbyComparison.NotEqual);
		}

		if (!filterOptions.Russian)
		{
			TeaLog.Info("LanguageFilter: Skipping Russian...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Russian, LobbyComparison.NotEqual);
		}

		if (!filterOptions.Korean)
		{
			TeaLog.Info("LanguageFilter: Skipping Korean...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Korean, LobbyComparison.NotEqual);
		}

		if (!filterOptions.TraditionalChinese)
		{
			TeaLog.Info("LanguageFilter: Skipping Traditional Chinese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.TraditionalChinese, LobbyComparison.NotEqual);
		}

		if (!filterOptions.SimplifiedChinese)
		{
			TeaLog.Info("LanguageFilter: Skipping Simplified Chinese...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.SimplifiedChinese, LobbyComparison.NotEqual);
		}

		if (!filterOptions.Arabic)
		{
			TeaLog.Info("LanguageFilter: Skipping Arabic...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.Arabic, LobbyComparison.NotEqual);
		}

		if (!filterOptions.LatinAmericanSpanish)
		{
			TeaLog.Info("LanguageFilter: Skipping Latin-American Spanish...");
			Matchmaking.AddRequestLobbyListNumericalFilter(languageKey, (int) Languages.LatinAmericanSpanish, LobbyComparison.NotEqual);
		}

		return this;
	}

	public bool ApplySameLanguage(ref string key, ref int value, ref int comparison)
	{
		if(Core_I.CurrentSearchType == SearchTypes.None) return false;

		LanguageFilterCustomization customization;
		string languageKey;

		GetCustomization(out customization, out languageKey);

		if(!customization.Enabled) return false;
		if(!key.Equals(languageKey)) return false;
		if (customization.ReplacementTargetEnum != LanguageSearchTypes.SameLanguage) return false;
		if (comparison != (int) LobbyComparison.Equal) return false;

		TeaLog.Info("LanguageFilter: Skipping Original Filter...");
		Apply(customization, languageKey);

		return true;
	}

	public LanguageFilter ApplyAnyLanguage()
	{

		if(!Core_I.IsLanguageAny) return this;

		LanguageFilterCustomization customization;
		string languageKey;

		GetCustomization(out customization, out languageKey);

		if (!customization.Enabled) return this;
		if (customization.ReplacementTargetEnum != LanguageSearchTypes.AnyLanguage) return this;

		Apply(customization, languageKey);

		return this;
	}
}
