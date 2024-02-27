using ImGuiNET;
using SharpPluginLoader.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

internal class Config : SingletonAccessor
{
	[JsonIgnore]
	public string Name { get; set; } = Constants.DEFAULT_CONFIG;

	public string Localization { get; set; } = "en-us";

	public RegionLockFixCustomization RegionLockFix { get; set; } = new();
	public MaxSearchResultLimitCustomization MaxSearchResultLimit { get; set; } = new();

	public Config() { }

	public Config InitDefault()
	{
		TeaLog.Info("Default Config: Initializing...");
		TeaLog.Info("Default Config: Initialization Done!");

		return this;
	}

	public Config Init()
	{
		TeaLog.Info("Config: Initializing...");

		RegionLockFix.Init();
		MaxSearchResultLimit.Init();

		TeaLog.Info("Config: Initialization Done!");

		return this;
	}

	public Config Save()
	{
		TeaLog.Info("Config: Saving...");

		configManager.ConfigWatcherInstance.TemporarilyDisable();
		JsonManager.SearializeToFile(Constants.DEFAULT_CONFIG_FILE_PATH_NAME, this);

		TeaLog.Info("Config: Saving Done!");
		return this;
	}

	public Config DeepCopy()
	{
		var json = JsonManager.Serialize(this);
		return JsonSerializer.Deserialize<Config>(json, JsonManager.JsonSerializerOptionsInstance).Init();
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
