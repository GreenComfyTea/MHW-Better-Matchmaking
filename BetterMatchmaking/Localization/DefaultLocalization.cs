using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.UI.Text;

namespace BetterMatchmaking;

public interface LocalizedStrings { }

internal class LocalizedStrings_LocalizationInfo : LocalizedStrings
{
	public string Translators { get; set; } = "GreenComfyTea";
}

internal class LocalizedStrings_UI : LocalizedStrings
{
	public string UI { get; set; } = "UI";
}

internal class LocalizedStrings_ImGui : LocalizedStrings
{
	// Mod Info
	public string MadeBy { get; set; } = "Made by:";
	public string NexusMods { get; set; } = "Nexus Mods";
	public string GitHubRepo { get; set; } = "GitHub Repo";
	public string Twitch { get; set; } = "Twitch";
	public string Twitter { get; set; } = "Twitter";
	public string ArtStation { get; set; } = "ArtStation";
	public string DonationMessage1 { get; set; } = "If you like the mod, please consider making a small donation!";
	public string DonationMessage2 { get; set; } = "It would help me maintain existing mods and create new ones in the future!";
	public string Donate { get; set; } = "Donate";
	public string PayPal { get; set; } = "PayPal";
	public string BuyMeATea { get; set; } = "Buy Me a Tea";

	// Config
	public string Config { get; set; } = "Config";

	public string ConfigName { get; set; } = "Config Name";

	public string New { get; set; } = "New";
	public string Duplicate { get; set; } = "Duplicate";
	public string Reset { get; set; } = "Reset";
	public string ResetConfig { get; set; } = "Reset Config";

	public string Delete { get; set; } = "Delete";

	// Language

	public string Language { get; set; } = "Language";

	public string Translators { get; set; } = "Translators:";

	// Region Fix
	public string Enabled { get; set; } = "Enabled";
	public string RegionLockFix { get; set; } = "Region Lock Fix";

	public string DistanceFilter { get; set; } = "Distance Filter";
	public string Close { get; set; } = "Close";
	public string Default { get; set; } = "Default";
	public string Far { get; set; } = "Far";
	public string Worldwide { get; set; } = "Worldwide";

	// Max Search Result Limit
	public string MaxSearchResultLimit { get; set; } = "Max Search Result Limit";
	public string Value { get; set; } = "Value";

	// Session Player Count Filter
	public string SessionPlayerCountFilter { get; set; } = "Session Player Count Filter";
	public string Min { get; set; } = "Min";
	public string Max { get; set; } = "Max";

	[JsonIgnore]
	public string[] DistanceFilters { get; set; } = Array.Empty<string>();


	public LocalizedStrings_ImGui()
	{
		DistanceFilters = [Close, Default, Far, Worldwide];
	}
}