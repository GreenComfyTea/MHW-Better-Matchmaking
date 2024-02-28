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

internal class ConfigManager : SingletonAccessor
{
	// Singleton Pattern
	private static readonly ConfigManager singleton = new();

	public static ConfigManager Instance { get { return singleton; } }

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

		ConfigWatcherInstance = new();
		Customization = new();
		Default = new();

		Default.InitDefault();

		// Create folder hierarchy if it doesn't exist
		Directory.CreateDirectory(Constants.PLUGIN_DATA_PATH);

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
			TeaLog.Info($"Config: Loading Failed!");
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

	public void SetCurrentConfig(Config config)
	{
		Current = config;

		regionLockFix.Customization = config.RegionLockFix;
		maxSearchResultLimit.Customization = config.MaxSearchResultLimit;
		sessionPlayerCountFilter.Customization = config.SessionPlayerCountFilter;
	}

	public Config LoadConfig()
	{
		try
		{
			TeaLog.Info($"Config: Loading...");

			var json = JsonManager.ReadFromFile(Constants.DEFAULT_CONFIG_FILE_PATH_NAME);

			var config = JsonSerializer.Deserialize<Config>(json, JsonManager.JsonSerializerOptionsInstance).Init();

			TeaLog.Info($"Config: Loading Done!");
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
		newConfig.Save();

		return this;
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
