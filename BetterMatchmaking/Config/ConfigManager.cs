using ImGuiNET;
using SharpPluginLoader.Core.Configuration;
using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal sealed class ConfigManager : SingletonAccessor, IDisposable
{
	// Singleton Pattern
	private static readonly ConfigManager _singleton = new();

	public static ConfigManager Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static ConfigManager() { }

	// Singleton Pattern End
	public ConfigWatcher ConfigWatcherInstance { get; set; }

	public ConfigCustomization Customization { get; set; }

	public Config Default { get; set; }
	public Config Current { get; set; }

	private ConfigManager() { }

	public ConfigManager Init()
	{
		TeaLog.Info("ConfigManager: Initializing...");

		InstantiateSingletons();

		// Create folder hierarchy if it doesn't exist
		Directory.CreateDirectory(Constants.PLUGIN_DATA_PATH);

		ConfigWatcherInstance = new();
		Customization = new();
		Default = new();

		Default.InitDefault();

		// If config file doesn't exist - use default one
		if(!File.Exists(Constants.DEFAULT_CONFIG_FILE_PATH_NAME))
		{
			TeaLog.Info("ConfigManager: Config Doesn't Exist, Using Default One.");

			SetCurrentConfig(Default);
			Current.Save();

			TeaLog.Info("ConfigManager: Initialization Done!");
			return this;
		}

		// If config file exists..

		// Load from file
		var config = LoadConfig();

		// If config file is incorrect - use default one
		if (config == null)
		{
			TeaLog.Info("Config: Loading Failed!");
			SetCurrentConfig(Default);

			TeaLog.Info("ConfigManager: Initialization Done!");
			return this;
		}

		// If config file is good - use it and save
		SetCurrentConfig(config);
		Current.Save();

		ConfigWatcherInstance.Init();

		TeaLog.Info("ConfigManager: Initialization Done!");
		return this;
	}

	public ConfigManager SetCurrentConfig(Config config)
	{
		Current = config;

		DebugManager_I.Customization = config.Debug;

		LocalizationManager_I.SetCurrentLocalization(config.Localization);

		var sessions = config.Sessions;
		var quests = config.Quests;
		var guidingLands = config.GuidingLands;

		var sessionInGameFilterOverride = sessions.InGameFilterOverride;
		var questInGameFilterOverride = quests.InGameFilterOverride;
		var guidingLandsInGameFilterOverride = guidingLands.InGameFilterOverride;



		SteamRegionLockFix_I.SessionCustomization = sessions.RegionLockFix;
		MaxSearchResultLimit_I.SessionCustomization = sessions.MaxSearchResultLimit;
		SessionPlayerCountFilter_I.Customization = sessions.PlayerCountFilter;

		PlayerTypeFilter_I.Customization = sessionInGameFilterOverride.PlayerType;
		QuestPreferenceFilter_I.Customization = sessionInGameFilterOverride.QuestPreference;
		LanguageFilter_I.SessionCustomization = sessionInGameFilterOverride.Language;



		SteamRegionLockFix_I.QuestCustomization = quests.RegionLockFix;
		MaxSearchResultLimit_I.QuestCustomization = quests.MaxSearchResultLimit;

		QuestTypeFilter_I.Customization = questInGameFilterOverride.QuestType;
		DifficultyFilter_I.Customization = questInGameFilterOverride.Difficulty;
		RewardFilter_I.Customization = questInGameFilterOverride.Rewards;
		LanguageFilter_I.QuestCustomization = questInGameFilterOverride.Language;
		TargetFilter_I.Customization = questInGameFilterOverride.Target;



		SteamRegionLockFix_I.GuidingLandsCustomization = guidingLands.RegionLockFix;
		MaxSearchResultLimit_I.GuidingLandsCustomization = guidingLands.MaxSearchResultLimit;

		ExpeditionObjectiveFilter_I.Customization = guidingLandsInGameFilterOverride.ExpeditionObjective;
		RegionLevelFilter_I.Customization = guidingLandsInGameFilterOverride.RegionLevel;
		LanguageFilter_I.GuidingLandsCustomization = guidingLandsInGameFilterOverride.Language;
		TargetMonsterFilter_I.Customization = guidingLandsInGameFilterOverride.TargetMonster;

		return this;
	}

	public static Config LoadConfig()
	{
		try
		{
			TeaLog.Info("Config: Loading...");

			var json = JsonManager.ReadFromFile(Constants.DEFAULT_CONFIG_FILE_PATH_NAME);
			var config = JsonSerializer.Deserialize<Config>(json, JsonManager.JSON_SERIALIZER_OPTIONS_INSTANCE).Init();

			TeaLog.Info("Config: Loading Done!");
			return config;
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
			return null;
		}
	}

	public ConfigManager ResetConfig()
	{
		var newConfig = Default.DeepCopy();

		SetCurrentConfig(newConfig);
		FontManager_I.RecreateFontCustomizations();
		FontManager_I.SetCurrentFont(LocalizationManager_I.Current);

		newConfig.Save();

		return this;
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}

	public void Dispose()
	{
		ConfigWatcherInstance.Dispose();
	}
}
