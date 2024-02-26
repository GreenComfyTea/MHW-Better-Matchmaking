using SharpPluginLoader.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class SelectedConfigClass : SingletonAccessor
{
	public string SelectedConfig { get; set; } = "default";

	public SelectedConfigClass Save()
	{
		TeaLog.Info($"Selected Config File: Saving...");
		configManager.SelectedConfigWatcherInstance.TemporarilyDisable();
		JsonManager.SearializeToFile(Constants.SELECTED_CONFIG_FILE_PATH_NAME, this);

		return this;
	}
}
