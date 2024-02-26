using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class SelectedConfigWatcher : SingletonAccessor
{
    private DateTime LastEventTime = DateTime.Now;

    private FileSystemWatcher Watcher { get; }

    public SelectedConfigWatcher()
    {
        TeaLog.Info("SelectedConfigChangeWatcher: Initializing...");

        Watcher = new(Constants.PLUGIN_DATA_PATH);

        Watcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.FileName
                             | NotifyFilters.LastWrite
                             | NotifyFilters.Security
                             | NotifyFilters.Size;

        Watcher.Changed += OnSelectedConfigFileChanged;
        Watcher.Created += OnSelectedConfigFileCreated;
        Watcher.Deleted += OnSelectedConfigFileDeleted;
        Watcher.Renamed += OnSelectedConfigFileRenamed;
        Watcher.Error += OnSelectedConfigFileError;

        Watcher.Filter = "*.json";
        Watcher.EnableRaisingEvents = true;

        TeaLog.Info("SelectedConfigChangeWatcher: Done!");
    }

    private void OnSelectedConfigFileChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed) return;

        TeaLog.Info($"SelectedConfigChangeWatcher: Changed {e.Name}");

        UpdateConfig(e.Name);
    }

    private void OnSelectedConfigFileCreated(object sender, FileSystemEventArgs e)
    {

        TeaLog.Info($"SelectedConfigChangeWatcher: Created {e.Name}");

        if (!e.Name.Equals(Constants.SELECTED_CONFIG_WITH_EXTENSION)) return;
        configManager.SelectedConfigInstance.Save();
    }

    private void OnSelectedConfigFileDeleted(object sender, FileSystemEventArgs e)
    {
        TeaLog.Info($"SelectedConfigChangeWatcher: Deleted {e.Name}");

        if (!e.Name.Equals(Constants.SELECTED_CONFIG_WITH_EXTENSION)) return;
        configManager.SelectedConfigInstance.Save();
    }

    private void OnSelectedConfigFileRenamed(object sender, RenamedEventArgs e)
    {
        TeaLog.Info($"SelectedConfigChangeWatcher: Renamed {e.OldName} to {e.Name}");

        if (!e.OldName.Equals(Constants.SELECTED_CONFIG_WITH_EXTENSION)) return;
        configManager.SelectedConfigInstance.Save();
    }

    private void OnSelectedConfigFileError(object sender, ErrorEventArgs e)
    {
        TeaLog.Info(e.GetException().ToString());
    }

    private void UpdateConfig(string fileName)
    {
        if (!fileName.Equals(Constants.SELECTED_CONFIG_WITH_EXTENSION)) return;

        DateTime currentEventTime = DateTime.Now;
        if ((currentEventTime - LastEventTime).TotalSeconds < 1) return;

        LastEventTime = currentEventTime;

        Timers.SetTimeout(() => configManager.LoadSelectedConfig(), 250);
    }

    public void TemporarilyDisable()
    {
        LastEventTime = DateTime.Now;
    }

    public override string ToString()
    {
        return JsonManager.Serialize(this);
    }
}
