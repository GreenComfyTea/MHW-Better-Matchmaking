using ImGuiNET;
using SharpPluginLoader.Core.Configuration;
using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
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

	public SelectedConfigWatcher SelectedConfigWatcherInstance { get; set; }

	public Dictionary<string, Config> Configs { get; set; } = new();

	public ConfigCustomization Customization { get; set; }

	public Config Default { get; set; }
	public Config Current { get; set; }

	public SelectedConfigClass SelectedConfigInstance { get; set; }

	private ConfigManager()
	{
		
	}

	public ConfigManager Init()
	{
		TeaLog.Info("ConfigManager: Initializing...");

		Customization = new();
		ConfigWatcherInstance = new();
		SelectedConfigWatcherInstance = new();

		Default = new Config().Init();
		//SetCurrentConfig(Default);

		LoadAllConfigs();
		LoadSelectedConfig();

		TeaLog.Info("ConfigManager: Done!");

		return this;
	}

	public ConfigManager SetCurrentConfig(Config config)
	{
		Current = config;
		SelectedConfigInstance.SelectedConfig = config.Name;
		SelectedConfigInstance.Save();
		Customization.SetCurrentConfig(config.Name);
		localizationManager.SetCurrentLocalization(Current.Language);

		return this;
	}

	public ConfigManager LoadAllConfigs()
	{
		TeaLog.Info("ConfigManager: Loading All Configs...");

		Configs = new();
		var configNamesList = new List<string>();
		var isDefaultConfigFromFile = false;

		foreach (var localizationFileNamePath in Directory.EnumerateFiles(Constants.CONFIGS_PATH, "*.json"))
		{
			var configName = LoadConfig(localizationFileNamePath);
			if (configName == null) continue;

			configNamesList.Add(configName);

			if(configName.Equals(Constants.DEFAULT_CONFIG))
			{
				isDefaultConfigFromFile = true;
			}
		}

		if (!isDefaultConfigFromFile)
		{
			NewConfig(Constants.DEFAULT_CONFIG);
		}

		Customization.ConfigNamesList = configNamesList;
		Customization.UpdateNamesList();

		return this;
	}

	public string LoadConfig(string configFileNamePath)
	{
		try
		{
			var configName = Path.GetFileNameWithoutExtension(configFileNamePath);

			TeaLog.Info($"Config {configName}: Loading...");

			var json = JsonManager.ReadFromFile(configFileNamePath);

			Configs[configName] = JsonSerializer.Deserialize<Config>(json, JsonManager.JsonSerializerOptionsInstance).Init(configName).Save();

			return configName;
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
			return null;
		}
	}

	public ConfigManager LoadSelectedConfig()
	{
		try
		{
			TeaLog.Info($"Selected Config File: Loading...");

			var json = JsonManager.ReadFromFile(Constants.SELECTED_CONFIG_FILE_PATH_NAME);

			var selectedConfig = JsonSerializer.Deserialize<SelectedConfigClass>(json, JsonManager.JsonSerializerOptionsInstance);

			Config config;
			
			var success = Configs.TryGetValue(selectedConfig.SelectedConfig, out config);

			if (!success) {
				SelectedConfigInstance.Save();
				TeaLog.Info($"Selected Config File: Done!");
				return this;
			}

			TeaLog.Info($"Selected Config File: Done!");

			SelectedConfigInstance = selectedConfig;
			SetCurrentConfig(config);

			return this;
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
			return this;
		}
	}

	private ConfigManager DuplicateConfig(string name, Config configToDuplicate)
	{
		if (name.Length == 0 || Customization.ConfigNamesList.Contains(name))
		{
			return this;
		}

		var newConfig = configToDuplicate.DeepCopy();
		newConfig.Name = name;
		newConfig.Save();

		Configs[name] = newConfig;
		Customization.AddConfig(name);

		SetCurrentConfig(newConfig);
		return this;
	}

	public ConfigManager NewConfig(string name)
	{
		DuplicateConfig(name.Trim(), Default);
		return this;
	}

	public ConfigManager DuplicateConfig(string name)
	{
		DuplicateConfig(name.Trim(), Current);
		return this;
	}

	public ConfigManager ResetConfig()
	{
		var newConfig = Default.DeepCopy();
		newConfig.Name = Current.Name;
		newConfig.Save();

		Configs[newConfig.Name] = newConfig;
		Current = newConfig;

		return this;
	}

	public ConfigManager DeleteConfig(string fileName)
	{
		var configName = Path.GetFileNameWithoutExtension(fileName);

		Config configToDelete;

		var contains = Configs.TryGetValue(fileName, out configToDelete);

		if (!contains) return this;

		Configs.Remove(configName);
		Customization.DeleteConfig(configName);

		return this;
	}

	public ConfigManager DeleteConfig()
	{
		var configToDelete = Current;

		Configs.Remove(configToDelete.Name);
		Customization.DeleteConfig(configToDelete.Name);

		Config defaultConfiguredConfig;
		var success = Configs.TryGetValue(Default.Name, out defaultConfiguredConfig);

		if(!success)
		{
			DuplicateConfig(Default.Name, Default);
			return this;
		}

		SetCurrentConfig(defaultConfiguredConfig);

		File.Delete($"{Constants.CONFIGS_PATH}{configToDelete.Name}.json");

		return this;
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
