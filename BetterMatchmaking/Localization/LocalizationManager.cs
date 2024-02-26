using ImGuiNET;
using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class LocalizationManager
{
	// Singleton Pattern
	private static readonly LocalizationManager singleton = new();

	public static LocalizationManager Instance { get { return singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static LocalizationManager() { }

	// Singleton Pattern End

	private LocalizationManager() { }

	public LocalizationWatcher LocalizationWatcherInstance { get; set; }

	public Dictionary<string, Localization> Localizations { get; set; } = new();

	public LocalizationCustomization Customization { get; set; }

	public Localization Default { get; set; }
	public Localization Current { get; set; }

	public LocalizedStrings_LocalizationInfo LocalizationInfo { get; set; }
	public LocalizedStrings_UI UI { get; set; }
	public LocalizedStrings_ImGui ImGui { get; set; }

	public LocalizationManager Init()
	{
		TeaLog.Info("LocalizationManager: Initializing...");

		Customization = new();
		LocalizationWatcherInstance = new();

		Default = new Localization();
		Default.Init();

		SetCurrentLocalization(Default);
		LoadAllLocalizations();

		TeaLog.Info("LocalizationManager: Done!");

		return this;
	}

	public LocalizationManager SetCurrentLocalization(Localization localization)
	{
		Current = localization;

		LocalizationInfo = localization.LocalizationInfo;
		UI = localization.UI;
		ImGui = localization.ImGui;

		Customization.SetCurrentLocalization(localization.Name);

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

		return this;
	}

	public string LoadLocalization(string localizationFileNamePath)
	{
		try
		{
			var localizationName = Path.GetFileNameWithoutExtension(localizationFileNamePath);

			if (localizationName.Equals(Default.Name)) return localizationName;

			TeaLog.Info($"Localization {localizationName}: Loading...");

			var json = JsonManager.ReadFromFile(localizationFileNamePath);

			var localization = JsonSerializer.Deserialize<Localization>(json, JsonManager.JsonSerializerOptionsInstance).Init(localizationName);

			Localizations[localizationName] = localization;

			return localizationName;
		}
		catch(Exception exception)
		{
			TeaLog.Error(exception.ToString());
			return null;
		}
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
