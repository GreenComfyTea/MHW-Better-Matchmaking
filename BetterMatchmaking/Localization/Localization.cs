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
		public string IsoName { get; set; } = "";
		[JsonIgnore]
		public bool IsDefault { get; set; } = false;

		public LocalizedStrings_LocalizationInfo LocalizationInfo { get; set; } = new();
		public LocalizedStrings_ImGui ImGui { get; set; } = new();

		public Localization() {
			InstantiateSingletons();
		}

		public Localization Init()
		{
			IsoName = Constants.DEFAULT_LOCALIZATION;

			TeaLog.Info($"Localization {IsoName}: Initializing...");

			IsDefault = true;
			ImGui.Init();

			TeaLog.Info($"Localization {IsoName}: Done!");

			return this;
		}

		public Localization Init(string name)
		{
			TeaLog.Info($"Localization {name}: Initializing...");

			InstantiateSingletons();
			IsoName = name;
			IsDefault = false;
			ImGui.Init();

			TeaLog.Info($"Localization {name}: Done!");

			return this;
		}

		public Localization Save()
		{
			TeaLog.Info($"Localization {IsoName}: Saving...");

			LocalizationManager_I.LocalizationWatcherInstance.TemporarilyDisable(IsoName);
			JsonManager.SearializeToFile(Path.Combine(Constants.LOCALIZATIONS_PATH, $"{IsoName}.json"), this);

			return this;
		}

		public override string ToString()
		{
			return JsonManager.Serialize(this);
		}
	}
}
