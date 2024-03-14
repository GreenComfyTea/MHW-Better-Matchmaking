using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class LocalizationWatcher : SingletonAccessor, IDisposable
{
	private Dictionary<string, DateTime> _lastEventTimes = new();

	private FileSystemWatcher Watcher { get; }

	public LocalizationWatcher()
	{
		InstantiateSingletons();
		Watcher = new(Constants.LOCALIZATIONS_PATH);
	}

	public LocalizationWatcher Init()
	{
		TeaLog.Info("LocalizationChangeWatcher: Initializing...");

		Watcher.NotifyFilter = NotifyFilters.Attributes
							 | NotifyFilters.CreationTime
							 | NotifyFilters.FileName
							 | NotifyFilters.LastWrite
							 | NotifyFilters.Security
							 | NotifyFilters.Size;

		Watcher.Changed += OnLocalizationFileChanged;
		Watcher.Created += OnLocalizationFileCreated;
		Watcher.Deleted += OnLocalizationFileDeleted;
		Watcher.Renamed += OnLocalizationFileRenamed;
		Watcher.Error += OnLocalizationFileError;

		Watcher.Filter = "*.json";
		Watcher.EnableRaisingEvents = true;

		TeaLog.Info("LocalizationChangeWatcher: Initialization Done!");

		return this;
	}

	private void OnLocalizationFileChanged(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType != WatcherChangeTypes.Changed) return;

		TeaLog.Info($"LocalizationChangeWatcher: Changed {e.Name}.");

		UpdateLocalization(e.FullPath, e.Name);
	}

	private void OnLocalizationFileCreated(object sender, FileSystemEventArgs e)
	{
		TeaLog.Info($"LocalizationChangeWatcher: Created {e.Name}.");

		UpdateLocalization(e.FullPath, e.Name);
	}

	private void OnLocalizationFileDeleted(object sender, FileSystemEventArgs e)
	{
		TeaLog.Info($"LocalizationChangeWatcher: Deleted {e.Name}.");

		if(e.Name.Equals(Constants.DEFAULT_LOCALIZATION)) LocalizationManager_I.Default.Save();
	}

	private void OnLocalizationFileRenamed(object sender, RenamedEventArgs e)
	{
		TeaLog.Info($"LocalizationChangeWatcher: Renamed {e.OldName} to {e.Name}.");

		LocalizationManager_I.Localizations.Remove(e.OldName);

		UpdateLocalization(e.FullPath, e.Name);
	}

	private void OnLocalizationFileError(object sender, ErrorEventArgs e)
	{
		TeaLog.Info(e.GetException().ToString());
	}

	private LocalizationWatcher UpdateLocalization(string filePathName, string fileName)
	{
		DateTime currentEventTime = DateTime.Now;
		DateTime lastEventTime;

		var contains = _lastEventTimes.TryGetValue(fileName, out lastEventTime);

		if (contains && (currentEventTime - lastEventTime).TotalSeconds < 1)
		{
			TeaLog.Info("LocalizationChangeWatcher: Skipping...");
			return this;
		}

		_lastEventTimes[fileName] = currentEventTime;

		Timers.SetTimeout(() =>
		{
			var localization = LocalizationManager_I.LoadLocalization(filePathName);
			LocalizationManager_I.Customization.AddLocalization(localization);
			LocalizationManager_I.SetCurrentLocalization(LocalizationManager_I.Current.IsoName);
		}, 250);

		return this;
	}

	public LocalizationWatcher TemporarilyDisable(string isoName)
	{
		TeaLog.Info($"LocalizationChangeWatcher: Localization { isoName}: Temporarily Disabling...");
		_lastEventTimes[$"{isoName}.json"] = DateTime.Now;

		return this;
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
	public void Dispose()
	{
		TeaLog.Info("LocalizationChangeWatcher: Disposing...");
		Watcher.Dispose();
	}
}
