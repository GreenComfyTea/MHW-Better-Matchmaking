using ImGuiNET;
using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class LocalizationManager : IDisposable
{
	// Singleton Pattern
	private static readonly LocalizationManager _singleton = new();

	public static LocalizationManager Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static LocalizationManager() { }

	// Singleton Pattern End

	private LocalizationManager() { }

	public LocalizationWatcher LocalizationWatcherInstance { get; set; }

	public Dictionary<string, Localization> Localizations { get; set; }

	public LocalizationCustomization Customization { get; set; }

	public Localization Default { get; set; }
	public Localization Current { get; set; }

	public LocalizedStrings_LocalizationInfo LocalizationInfo { get; set; }
	public LocalizedStrings_ImGui ImGui { get; set; }

	public LocalizationManager Init()
	{
		TeaLog.Info("LocalizationManager: Initializing...");

		Customization = new();
		LocalizationWatcherInstance = new();
		Default = new();

		Default.Init();
		Default.Save();

		// Create folder hierarchy if it doesn't exist
		Directory.CreateDirectory(Constants.LOCALIZATIONS_PATH);

		SetCurrentLocalization(Default);
		LoadAllLocalizations();

		LocalizationWatcherInstance.Init();

		TeaLog.Info("LocalizationManager: Initialization Done!");

		return this;
	}

	public LocalizationManager SetCurrentLocalization(Localization localization)
	{
		Current = localization;
		Customization.SetCurrentLocalization(localization);

		LocalizationInfo = Current.LocalizationInfo;
		ImGui = Current.ImGui;

		return this;
	}

	public LocalizationManager SetCurrentLocalization(string isoName)
	{
		var localization = Localizations[isoName];
		SetCurrentLocalization(localization);

		return this;
	}

	public LocalizationManager LoadAllLocalizations()
	{
		TeaLog.Info("LocalizationManager: Loading All Localizations...");

		Localizations = new();
		var localizationNamesList = new List<string>();
		Localizations[Default.IsoName] = Default;

		foreach (var localizationFileNamePath in Directory.EnumerateFiles(Constants.LOCALIZATIONS_PATH, "*.json"))
		{
			var localization = LoadLocalization(localizationFileNamePath);
			if (localization == null) continue;

			localizationNamesList.Add(localization.IsoName);

			Customization.AddLocalization(localization);
		}

		TeaLog.Info("LocalizationManager: Loading All Localizations Done!");

		return this;
	}

	public Localization LoadLocalization(string localizationFileNamePath)
	{
		var isoName = Path.GetFileNameWithoutExtension(localizationFileNamePath);

		try
		{
			if (isoName.Equals(Default.IsoName)) return Default;

			TeaLog.Info($"Localization {isoName}: Loading...");

			var json = JsonManager.ReadFromFile(localizationFileNamePath);

			var localization = JsonSerializer.Deserialize<Localization>(json, JsonManager.JSON_SERIALIZER_OPTIONS_INSTANCE).Init(isoName);
			localization.Save();

			TeaLog.Info($"Localization {isoName}: Loading Done!");

			Localizations[isoName] = localization;

			return localization;
		}
		catch(Exception exception)
		{
			TeaLog.Info($"Localization {isoName}: Loading Failed!");
			TeaLog.Error(exception.ToString());
			return null;
		}
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}

	public void Dispose()
	{
		LocalizationWatcherInstance.Dispose();
	}
}
