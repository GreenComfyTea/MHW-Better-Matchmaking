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
	public string Name { get; set; } = "";

	public string Language { get; set; } = "en-us";

	public Config() { }

	public Config Init()
	{
		Name = Constants.DEFAULT_CONFIG;

		TeaLog.Info($"Default Config: Initializing...");
		TeaLog.Info($"Default Config: Done!");

		return this;
	}

	public Config Init(string name)
	{
		Name = name;

		TeaLog.Info($"Config {Name}: Initializing...");
		TeaLog.Info($"Config {Name}: Done!");

		return this;
	}

	public Config Save()
	{
		TeaLog.Info($"Config {Name}: Saving...");

		configManager.ConfigWatcherInstance.TemporarilyDisable(Name);
		JsonManager.SearializeToFile(Path.Combine(Constants.CONFIGS_PATH, $"{Name}.json"), this);
		
		TeaLog.Info($"Config {Name}: Done!");

		return this;
	}

	public Config DeepCopy()
	{
		var json = JsonManager.Serialize(this);
		return JsonSerializer.Deserialize<Config>(json, JsonManager.JsonSerializerOptionsInstance).Init(Name);
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
