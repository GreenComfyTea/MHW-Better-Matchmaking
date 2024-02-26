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
	public string Delete { get; set; } = "Delete";

	// Language

	public string Language { get; set; } = "Language";

	public string Translators { get; set; } = "Translators:";

	// Bar
	public string Visible { get; set; } = "Visible";
	public string Settings { get; set; } = "Settings";

	public string FillDirection { get; set; } = "Fill Direction";
	public string LeftToRight { get; set; } = "Left to Right";
	public string RightToLeft { get; set; } = "Right to Left";
	public string TopToBottom { get; set; } = "Top to Bottom";
	public string BottomToTop { get; set; } = "Bottom to Top";

	public string Offset { get; set; } = "Offset";
	public string X { get; set; } = "X";
	public string Y { get; set; } = "Y";

	public string Size { get; set; } = "Size";
	public string Width { get; set; } = "Width";
	public string Height { get; set; } = "Height";

	public string Outline { get; set; } = "Outline";
	public string Thickness { get; set; } = "Thickness";

	public string Mode { get; set; } = "Mode";
	public string Outside { get; set; } = "Outside";
	public string Center { get; set; } = "Center";
	public string Inside { get; set; } = "Inside";

	public string Colors { get; set; } = "Colors";
	public string Fill { get; set; } = "Fill";
	public string Background { get; set; } = "Background";

	// Label
	public string RightAlignmentShift { get; set; } = "Right Alignment Shift";
	public string Color { get; set; } = "Color";
	public string Shadow { get; set; } = "Shadow";

	public string Font { get; set; } = "Font";


	[JsonIgnore]
	public string[] FillDirections { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] OutlineModes { get; set; } = Array.Empty<string>();

	public LocalizedStrings_ImGui()
	{
		FillDirections = [LeftToRight, RightToLeft, TopToBottom, BottomToTop];
		OutlineModes = [Outside, Center, Inside];
	}
}