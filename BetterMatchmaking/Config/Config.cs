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

	public Dictionary<string, FontCustomization> Fonts { get; set; } = new();

	public DebugCustomization Debug { get; set; } = new();
	public SessionCustomization Sessions { get; set; } = new();
	public QuestCustomization Quests { get; set; } = new();
	public GuidingLandsCustomization GuidingLands { get; set; } = new();

	public Config()
	{
		InstantiateSingletons();
	}

	public Config InitDefault()
	{
		TeaLog.Info("Default Config: Initializing...");

		TeaLog.Info("Default Config: Initialization Done!");

		return this;
	}

	public Config Init()
	{
		TeaLog.Info("Config: Initializing...");

		Debug.Init();
		Sessions.Init();
		Quests.Init();
		GuidingLands.Init();

		TeaLog.Info("Config: Initialization Done!");

		return this;
	}

	public Config Save()
	{
		TeaLog.Info("Config: Saving...");

		ConfigManager_I.ConfigWatcherInstance.TemporarilyDisable();
		JsonManager.SearializeToFile(Constants.DEFAULT_CONFIG_FILE_PATH_NAME, this);

		TeaLog.Info("Config: Saving Done!");
		return this;
	}

	public Config DeepCopy()
	{
		var json = JsonManager.Serialize(this);
		return JsonSerializer.Deserialize<Config>(json, JsonManager.JSON_SERIALIZER_OPTIONS_INSTANCE).Init();
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
