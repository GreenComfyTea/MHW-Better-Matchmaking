using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class ConfigWatcher : SingletonAccessor
{
    private Dictionary<string, DateTime> LastEventTimes = new();

    private FileSystemWatcher Watcher { get; }

    public ConfigWatcher()
    {
        TeaLog.Info("ConfigChangeWatcher: Initializing...");

        Watcher = new(Constants.CONFIGS_PATH);

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

        Watcher.Filter = "*.json";
        Watcher.EnableRaisingEvents = true;

        TeaLog.Info("ConfigChangeWatcher: Done!");
    }

    private void OnConfigFileChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed) return;

        TeaLog.Info($"ConfigChangeWatcher: Changed {e.Name}");

        UpdateConfig(e.FullPath, e.Name);
    }

    private void OnConfigFileCreated(object sender, FileSystemEventArgs e)
    {

        TeaLog.Info($"ConfigChangeWatcher: Created {e.Name}");

        UpdateConfig(e.FullPath, e.Name);
    }

    private void OnConfigFileDeleted(object sender, FileSystemEventArgs e)
    {
        TeaLog.Info($"ConfigChangeWatcher: Deleted {e.Name}");

        //configManager.DeleteConfig(e.Name);
    }

    private void OnConfigFileRenamed(object sender, RenamedEventArgs e)
    {
        TeaLog.Info($"ConfigChangeWatcher: Renamed {e.OldName} to {e.Name}");

        configManager.Configs.Remove(e.OldName);

        UpdateConfig(e.FullPath, e.Name);
    }

    private void OnConfigFileError(object sender, ErrorEventArgs e)
    {
        TeaLog.Info(e.GetException().ToString());
    }

    private void UpdateConfig(string filePathName, string fileName)
    {
        DateTime currentEventTime = DateTime.Now;
        DateTime lastEventTime;

        var contains = LastEventTimes.TryGetValue(fileName, out lastEventTime);

        if (contains && (currentEventTime - lastEventTime).TotalSeconds < 1) return;

        LastEventTimes[fileName] = currentEventTime;

        Timers.SetTimeout(() =>
        {
            var configName = configManager.LoadConfig(filePathName);
            configManager.Customization.AddConfig(configName);
            configManager.SetCurrentConfig(configManager.Configs[configName]);
        }, 250);
    }

    public void TemporarilyDisable(string configName)
    {
        LastEventTimes[$"{configName}.json"] = DateTime.Now;
    }

    public override string ToString()
    {
        return JsonManager.Serialize(this);
    }
}
