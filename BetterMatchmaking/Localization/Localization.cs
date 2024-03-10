using ImGuiNET;
using SharpPluginLoader.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking
{
	internal class Localization : SingletonAccessor
	{
		[JsonIgnore]
		public string Name { get; set; } = "";

		[JsonIgnore]
		public bool IsDefault { get; set; } = false;

		public LocalizedStrings_LocalizationInfo LocalizationInfo { get; set; } = new();

		public LocalizedStrings_UI UI { get; set; } = new();

		public LocalizedStrings_ImGui ImGui { get; set; } = new();

		public Localization() {
			InstantiateSingletons();
		}

		public Localization Init()
		{
			Name = Constants.DEFAULT_LOCALIZATION;

			TeaLog.Info($"Localization {Name}: Initializing...");

			IsDefault = true;

			TeaLog.Info($"Localization {Name}: Done!");

			return this;
		}

		public Localization Init(string name)
		{
			TeaLog.Info($"Localization {name}: Initializing...");

			InstantiateSingletons();
			Name = name;
			IsDefault = false;

			TeaLog.Info($"Localization {name}: Done!");

			return this;
		}

		public Localization Save()
		{
			TeaLog.Info($"Localization {Name}: Saving...");

			LocalizationManager_I.LocalizationWatcherInstance.TemporarilyDisable(Name);
			JsonManager.SearializeToFile(Path.Combine(Constants.LOCALIZATIONS_PATH, $"{Name}.json"), this);

			return this;
		}

		public override string ToString()
		{
			return JsonManager.Serialize(this);
		}
	}
}
