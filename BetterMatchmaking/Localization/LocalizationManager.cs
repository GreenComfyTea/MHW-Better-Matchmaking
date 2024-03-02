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
		Customization.SetCurrentLocalization(localization.Name);

		LocalizationInfo = Current.LocalizationInfo;
		ImGui = Current.ImGui;

		return this;
	}

	public LocalizationManager SetCurrentLocalization(string localizationName)
	{
		var localization = Localizations[localizationName];
		SetCurrentLocalization(localization);

		return this;
	}

	public LocalizationManager LoadAllLocalizations()
	{
		TeaLog.Info("LocalizationManager: Loading All Localizations...");

		Localizations = new();
		var localizationNamesList = new List<string>();
		Localizations[Default.Name] = Default;

		foreach (var localalizationFileNamePath in Directory.EnumerateFiles(Constants.LOCALIZATIONS_PATH, "*.json"))
		{
			var localizationName = LoadLocalization(localalizationFileNamePath);
			if (localizationName == null) continue;

			localizationNamesList.Add(localizationName);
		}

		Customization.LocalizationNamesList = localizationNamesList;
		Customization.UpdateNamesList();

		TeaLog.Info("LocalizationManager: Loading All Localizations Done!");

		return this;
	}

	public string LoadLocalization(string localizationFileNamePath)
	{
		var localizationName = Path.GetFileNameWithoutExtension(localizationFileNamePath);

		try
		{
			if (localizationName.Equals(Default.Name)) return localizationName;

			TeaLog.Info($"Localization {localizationName}: Loading...");

			var json = JsonManager.ReadFromFile(localizationFileNamePath);

			var localization = JsonSerializer.Deserialize<Localization>(json, JsonManager.JSON_SERIALIZER_OPTIONS_INSTANCE).Init(localizationName);

			TeaLog.Info($"Localization {localizationName}: Loading Done!");

			Localizations[localizationName] = localization;

			return localizationName;
		}
		catch(Exception exception)
		{
			TeaLog.Info($"Localization {localizationName}: Loading Failed!");
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
