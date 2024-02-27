using SharpPluginLoader.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class ConfigWatcher : SingletonAccessor
{
	private DateTime LastEventTime = new();

	private FileSystemWatcher Watcher { get; }

	public ConfigWatcher()
	{
		Watcher = new(Constants.PLUGIN_DATA_PATH);
	}

	public ConfigWatcher Init()
	{
		TeaLog.Info("ConfigChangeWatcher: Initializing...");

		Watcher.NotifyFilter = NotifyFilters.Attributes
							 | NotifyFilters.CreationTime
							 | NotifyFilters.FileName
							 | NotifyFilters.LastWrite
							 | NotifyFilters.Security
							 | NotifyFilters.Size;

		Watcher.Changed += OnConfigFileChanged;
		Watcher.Created += OnConfigFileCreated;
		Watcher.Deleted += OnConfigFileDeleted;
		Watcher.Renamed += OnConfigFileRenamed;
		Watcher.Error += OnConfigFileError;

		Watcher.Filter = $"{Constants.DEFAULT_CONFIG}.json";
		Watcher.EnableRaisingEvents = true;

		TeaLog.Info("ConfigChangeWatcher: Initialization Done!");
		return this;
	}

	private void OnConfigFileChanged(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType != WatcherChangeTypes.Changed) return;

		TeaLog.Info($"ConfigChangeWatcher: Changed {e.Name}.");

		UpdateConfig();
	}

	private void OnConfigFileCreated(object sender, FileSystemEventArgs e)
	{
		TeaLog.Info($"ConfigChangeWatcher: Created {e.Name}.");

		UpdateConfig();
	}

	private void OnConfigFileDeleted(object sender, FileSystemEventArgs e)
	{
		TeaLog.Info($"ConfigChangeWatcher: Deleted {e.Name}.");

		// Save current config if the config file was deleted
		configManager.Current.Save();
	}

	private void OnConfigFileRenamed(object sender, RenamedEventArgs e)
	{
		TeaLog.Info($"ConfigChangeWatcher: Renamed {e.OldName} to {e.Name}.");

		// Save current config if the config file was renamed
		configManager.Current.Save();
	}

	private void OnConfigFileError(object sender, ErrorEventArgs e)
	{
		TeaLog.Info(e.GetException().ToString());
	}

	private void UpdateConfig()
	{
		DateTime currentEventTime = DateTime.Now;

		if ((currentEventTime - LastEventTime).TotalSeconds < 1)
		{
			TeaLog.Info($"ConfigChangeWatcher: Skipping...");
			return;
		}

		LastEventTime = currentEventTime;

		Timers.SetTimeout(() =>
		{
			// Load from file
			var config = configManager.LoadConfig();

			// If config file is incorrect - do nothing
			if (config == null)
			{
				return;
			}

			// If config file is good - use it and save
			configManager.SetCurrentConfig(config);
			config.Save();
		}, 250);
	}

	public void TemporarilyDisable()
	{
		TeaLog.Info($"ConfigChangeWatcher: Temporarily Disabling...");
		LastEventTime = DateTime.Now;
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
