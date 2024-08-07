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

using GlyphRange = (ushort start, ushort end);

namespace BetterMatchmaking;

public interface ILocalizationSection;

internal class LocalizationInfoSection : ILocalizationSection
{
	public string Name { get; set; } = "English";
	public string Translators { get; set; } = "GreenComfyTea";
}

internal class FontInfoSection : ILocalizationSection
{
	public string Name { get; set; } = "NotoSans-Bold.ttf";

	public string[] GlyphRanges { get; set; } = ["0x0020", "0xFFFF"];
}

internal class ImGuiSection : ILocalizationSection
{
	// Mod Info
	public string ModInfo { get; set; } = "Mod Info";

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

	public string Translators { get; set; } = "Translator(s)";

	// Debug Manager

	public string Errors { get; set; } = "Errors";
	public string CurrentTime { get; set; } = "Current Time";
	public string EverythingSeemsToBeOk { get; set; } = "Everything Seems to Be OK!";
	public string History { get; set; } = "History";
	public string HistorySize { get; set; } = "History Size";

	// Steam Region Lock Fix

	public string Enabled { get; set; } = "Enabled";
	public string SteamRegionLockFix { get; set; } = "Steam Region Lock Fix";
	public string Sessions { get; set; } = "Sessions";
	public string Quests { get; set; } = "Quests";

	public string DistanceFilter { get; set; } = "Distance Filter";
	public string Close { get; set; } = "Close";
	public string Default { get; set; } = "Default";
	public string Far { get; set; } = "Far";
	public string Worldwide { get; set; } = "Worldwide";

	public string Explanation { get; set; } = "Explanation";

	// Max Search Result Limit
	public string MaxSearchResultLimit { get; set; } = "Max Search Result Limit";
	public string Value { get; set; } = "Value";

	// Session Player Count Filter
	public string PlayerCountFilter { get; set; } = "Player Count Filter";
	public string Min { get; set; } = "Min";
	public string Max { get; set; } = "Max";

	// In-Game Filter Override

	public string InGameFilterOverride { get; set; } = "In-Game Filter Override";
	public string ReplacementTarget { get; set; } = "Replacement Target";
	public string FilterOptions { get; set; } = "Filter Options";

	// Player Type

	public string PlayerType { get; set; } = "Player Type";

	public string Beginners { get; set; } = "Beginners";
	public string Experienced { get; set; } = "Experienced";
	public string Any { get; set; } = "Any";

	// Quest Preference

	public string QuestPreference { get; set; } = "Quest Preference";

	public string General { get; set; } = "General";
	public string BaseGameMSQMonsters { get; set; } = "Base Game MSQ";
	public string BaseGameEndgameMonsters { get; set; } = "Base Game Endgame";
	public string IceborneMSQMonsters { get; set; } = "Iceborne MSQ";
	public string IceborneEndgameMonsters { get; set; } = "Iceborne Endgame";

	public string SelectAll { get; set; } = "Select All";
	public string DeselectAll { get; set; } = "Deselect All";

	public string None { get; set; } = "None";
	public string Assignments { get; set; } = "Assignments";
	public string Optional { get; set; } = "Optional";
	public string Investigation { get; set; } = "Investigation";
	public string TheGuidingLandsExpedition { get; set; } = "The Guiding Lands Expedition";
	public string EventQuests { get; set; } = "Event Quests";
	public string SpecialAssignments { get; set; } = "Special Assignments";
	public string Arena { get; set; } = "Arena";
	public string Expeditions { get; set; } = "Expeditions";
	public string TemperedMonsters { get; set; } = "Tempered Monsters";
	public string SmallMonsters { get; set; } = "Small Monsters";

	public string GreatJagras { get; set; } = "Great Jagras";
	public string KuluYaKu { get; set; } = "Kulu-Ya-Ku";
	public string PukeiPukei { get; set; } = "Pukei-Pukei";
	public string Barroth { get; set; } = "Barroth";
	public string Jyuratodus { get; set; } = "Jyuratodus";
	public string TobiKadachi { get; set; } = "Tobi-Kadachi";
	public string Anjanath { get; set; } = "Anjanath";
	public string Rathian { get; set; } = "Rathian";
	public string TzitziYaKu { get; set; } = "Tzitzi-Ya-Ku";
	public string Paolumu { get; set; } = "Paolumu";
	public string GreatGirros { get; set; } = "Great Girros";
	public string Radobaan { get; set; } = "Radobaan";
	public string Legiana { get; set; } = "Legiana";
	public string Odogaron { get; set; } = "Odogaron";
	public string Rathalos { get; set; } = "Rathalos";
	public string Diablos { get; set; } = "Diablos";
	public string Kirin { get; set; } = "Kirin";
	public string ZorahMagdaros { get; set; } = "Zorah Magdaros";
	public string Dodogama { get; set; } = "Dodogama";
	public string PinkRathian { get; set; } = "Pink Rathian";
	public string Bazelgeuse { get; set; } = "Bazelgeuse";
	public string Lavasioth { get; set; } = "Lavasioth";
	public string Uragaan { get; set; } = "Uragaan";
	public string AzureRathalos { get; set; } = "Azure Rathalos";
	public string BlackDiablos { get; set; } = "Black Diablos";
	public string Nergigante { get; set; } = "Nergigante";
	public string Teostra { get; set; } = "Teostra";
	public string KushalaDaora { get; set; } = "Kushala Daora";
	public string VaalHazak { get; set; } = "Vaal Hazak";
	public string Xenojiiva { get; set; } = "Xeno'jiiva";

	public string KulveTaroth { get; set; } = "Kulve Taroth";
	public string Deviljho { get; set; } = "Deviljho";
	public string Lunastra { get; set; } = "Lunastra";
	public string Behemoth { get; set; } = "Behemoth";
	public string AncientLeshen { get; set; } = "Ancient Leshen";

	public string Beotodus { get; set; } = "Beotodus";
	public string Banbaro { get; set; } = "Banbaro";
	public string ViperTobiKadachi { get; set; } = "Viper Tobi-Kadachi";
	public string NightshadePaolumu { get; set; } = "Nightshade Paolumu";
	public string CoralPukeiPukei { get; set; } = "Coral Pukei-Pukei";
	public string Barioth { get; set; } = "Barioth";
	public string Nargacuga { get; set; } = "Nargacuga";
	public string Glavenus { get; set; } = "Glavenus";
	public string Tigrex { get; set; } = "Tigrex";
	public string Brachydios { get; set; } = "Brachydios";
	public string ShriekingLegiana { get; set; } = "Shrieking Legiana";
	public string FulgurAnjanath { get; set; } = "Fulgur Anjanath";
	public string AcidicGlavenus { get; set; } = "Acidic Glavenus";
	public string EbonyOdogaron { get; set; } = "Ebony Odogaron";
	public string Velkhana { get; set; } = "Velkhana";
	public string SeethingBazelgeuse { get; set; } = "Seething Bazelgeuse";
	public string BlackveilVaalHazak { get; set; } = "Blackveil Vaal Hazak";
	public string Namielle { get; set; } = "Namielle";
	public string RuinerNergigante { get; set; } = "Ruiner Nergigante";
	public string SharaIshvalda { get; set; } = "Shara Ishvalda";

	public string SavageDeviljho { get; set; } = "Savage Deviljho";
	public string BruteTigrex { get; set; } = "Brute Tigrex";
	public string Zinogre { get; set; } = "Zinogre";
	public string YianGaruga { get; set; } = "Yian Garuga";
	public string ScarredYianGaruga { get; set; } = "Scarred Yian Garuga";
	public string GoldRathian { get; set; } = "Gold Rathian";
	public string SilverRathalos { get; set; } = "Silver Rathalos";
	public string Rajang { get; set; } = "Rajang";
	public string StygianZinogre { get; set; } = "Stygian Zinogre";
	public string FuriousRajang { get; set; } = "Furious Rajang";
	public string RagingBrachydios { get; set; } = "Raging Brachydios";
	public string FrostfangBarioth { get; set; } = "Frostfang Barioth";
	public string Safijiiva { get; set; } = "Safi'jiiva";
	public string Alatreon { get; set; } = "Alatreon";
	public string Fatalis { get; set; } = "Fatalis";

	// Language Filter
	public string SameLanguage { get; set; } = "Same Language";
	public string AnyLanguage { get; set; } = "Any Language";

	public string Japanese { get; set; } = "Japanese";
	public string English { get; set; } = "English";
	public string French { get; set; } = "French";
	public string Italian { get; set; } = "Italian";
	public string German { get; set; } = "German";
	public string Spanish { get; set; } = "Spanish";
	public string BrazilianPortuguese { get; set; } = "Brazilian Portuguese";
	public string Polish { get; set; } = "Polish";
	public string Russian { get; set; } = "Russian";
	public string Korean { get; set; } = "Korean";
	public string TraditionalChinese { get; set; } = "Traditional Chinese";
	public string SimplifiedChinese { get; set; } = "Simplified Chinese";
	public string Arabic { get; set; } = "Arabic";
	public string LatinAmericanSpanish { get; set; } = "Latin-American Spanish";

	// Quest Type Filter

	public string QuestType { get; set; } = "Quest Type";

	public string OptionalQuests { get; set; } = "Optional Quests";
	public string Investigations { get; set; } = "Investigations";
	public string SpecialInvestigations { get; set; } = "Special Investigations";

	// Custom Quest Rank Filter

	public string Difficulty { get; set; } = "Difficulty";

	public string LowRank { get; set; } = "Low Rank";
	public string HighRank { get; set; } = "High Rank";
	public string MasterRank { get; set; } = "Master Rank";

	public string _1 { get; set; } = "1";
	public string _2 { get; set; } = "2";
	public string _3 { get; set; } = "3";
	public string _4 { get; set; } = "4";
	public string _5 { get; set; } = "5";

	public string _6 { get; set; } = "6";
	public string _7 { get; set; } = "7";
	public string _8 { get; set; } = "8";
	public string _9 { get; set; } = "9";

	public string MasterRank1 { get; set; } = "M1";
	public string MasterRank2 { get; set; } = "M2";
	public string MasterRank3 { get; set; } = "M3";
	public string MasterRank4 { get; set; } = "M4";
	public string MasterRank5 { get; set; } = "M5";
	public string MasterRank6 { get; set; } = "M6";

	public string LowRank1Star { get; set; } = "1⭐";
	public string LowRank2Star { get; set; } = "2⭐";
	public string LowRank3Star { get; set; } = "3⭐";
	public string LowRank4Star { get; set; } = "4⭐";
	public string LowRank5Star { get; set; } = "5⭐";

	public string HighRank6Star { get; set; } = "6⭐";
	public string HighRank7Star { get; set; } = "7⭐";
	public string HighRank8Star { get; set; } = "8⭐";
	public string HighRank9Star { get; set; } = "9⭐";

	public string MasterRank1Star { get; set; } = "M1⭐";
	public string MasterRank2Star { get; set; } = "M2⭐";
	public string MasterRank3Star { get; set; } = "M3⭐";
	public string MasterRank4Star { get; set; } = "M4⭐";
	public string MasterRank5Star { get; set; } = "M5⭐";
	public string MasterRank6Star { get; set; } = "M6⭐";

	// Rewards Filter

	public string Rewards { get; set; } = "Rewards";

	public string NoPreference { get; set; } = "No Preference";
	public string NoRewards { get; set; } = "No Rewards";
	public string RewardsAvailable { get; set; } = "Rewards Available";

	// Target Filter

	public string Target { get; set; } = "Target";

	// Guiding Lands

	public string GuidingLands { get; set; } = "Guiding Lands";

	// Expedition Objective Filter

	public string ExpeditionObjective { get; set; } = "Expedition Objective";

	public string FieldResearch { get; set; } = "Field Research";
	public string FieldResearchForest { get; set; } = "Field Research: Forest";
	public string FieldResearchWildspire { get; set; } = "Field Research: Wildspire";
	public string FieldResearchCoral { get; set; } = "Field Research: Coral";
	public string FieldResearchRotted { get; set; } = "Field Research: Rotted";
	public string FieldResearchVolcanic { get; set; } = "Field Research: Volcanic";
	public string FieldResearchTundra { get; set; } = "Field Research: Tundra";

	public string Mining { get; set; } = "Mining";
	public string MiningForest { get; set; } = "Mining: Forest";
	public string MiningWildspire { get; set; } = "Mining: Wildspire";
	public string MiningCoral { get; set; } = "Mining: Coral";
	public string MiningRotted { get; set; } = "Mining: Rotted";
	public string MiningVolcanic { get; set; } = "Mining: Volcanic";
	public string MiningTundra { get; set; } = "Mining: Tundra";

	public string BoneResearch { get; set; } = "Bone Research";
	public string BoneResearchForest { get; set; } = "Bone Research: Forest";
	public string BoneResearchWildspire { get; set; } = "Bone Research: Wildspire";
	public string BoneResearchCoral { get; set; } = "Bone Research: Coral";
	public string BoneResearchRotted { get; set; } = "Bone Research: Rotted";
	public string BoneResearchVolcanic { get; set; } = "Bone Research: Volcanic";
	public string BoneResearchTundra { get; set; } = "Bone Research: Tundra";

	public string FixedRegion { get; set; } = "Fixed Region";
	public string FixedRegionForest { get; set; } = "Fixed Region: Forest";
	public string FixedRegionWildspire { get; set; } = "Fixed Region: Wildspire";
	public string FixedRegionCoral { get; set; } = "Fixed Region: Coral";
	public string FixedRegionRotted { get; set; } = "Fixed Region: Rotted";
	public string FixedRegionVolcanic { get; set; } = "Fixed Region: Volcanic";
	public string FixedRegionTundra { get; set; } = "Fixed Region: Tundra";

	// Region Level

	public string RegionLevel { get; set; } = "Region Level";

	public string Level1 { get; set; } = "Level 1";
	public string Level2 { get; set; } = "Level 2";
	public string Level3 { get; set; } = "Level 3";
	public string Level4 { get; set; } = "Level 4";
	public string Level5 { get; set; } = "Level 5";
	public string Level6 { get; set; } = "Level 6";
	public string Level7 { get; set; } = "Level 7";

	// Target Monster
	public string TargetMonster { get; set; } = "Target Monster";

	// Font

	public string Font { get; set; } = "Font";
	public string AnyChangesToFontRequireGameRestart { get; set; } = "Any Changes to Font Require Game Restart!";
	public string Size { get; set; } = "Size";
	public string HorizontalOversample { get; set; } = "Horizontal Oversample";
	public string VerticalOversample { get; set; } = "Vertical Oversample";

	[JsonIgnore]
	public string[] DistanceFilterArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] PlayerTypeArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] QuestPreferenceArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] LanguageSearchTypeArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] QuestTypeArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] QuestRankReplacementTargets { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] StyledQuestRankReplacementTargets { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] RewardReplacementTargets { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] TargetArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] ExpeditionObjectiveArray { get; set; } = Array.Empty<string>();

	[JsonIgnore]
	public string[] RegionLevelArray { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] TargetMonsterArray { get; set; } = Array.Empty<string>();

	public ImGuiSection Init()
	{
		DistanceFilterArray = [Close, Default, Far, Worldwide];

		PlayerTypeArray = [Beginners, Experienced, Any];

		LanguageSearchTypeArray = [SameLanguage, AnyLanguage];

		QuestTypeArray = [NoPreference, OptionalQuests, Assignments, Investigations, Expeditions, EventQuests, SpecialAssignments];

		RewardReplacementTargets = [NoPreference, RewardsAvailable];

		RegionLevelArray = [NoPreference, Level1, Level2, Level3, Level4, Level5, Level6, Level7];

		QuestRankReplacementTargets =
		[
			LowRank,
			HighRank,
			MasterRank,
			_1,
			_2,
			_3,
			_4,
			_5,
			_6,
			_7,
			_8,
			_9,
			MasterRank1,
			MasterRank2,
			MasterRank3,
			MasterRank4,
			MasterRank5,
			MasterRank6
		];

		StyledQuestRankReplacementTargets =
		[
			LowRank,
			HighRank,
			MasterRank,
			LowRank1Star,
			LowRank2Star,
			LowRank3Star,
			LowRank4Star,
			LowRank5Star,
			HighRank6Star,
			HighRank7Star,
			HighRank8Star,
			HighRank9Star,
			MasterRank1Star,
			MasterRank2Star,
			MasterRank3Star,
			MasterRank4Star,
			MasterRank5Star,
			MasterRank6Star
		];

		QuestPreferenceArray =
		[
			None,
			Assignments,
			Optional,
			Investigation,
			TheGuidingLandsExpedition,
			EventQuests,
			SpecialAssignments,
			Arena,
			Expeditions,
			TemperedMonsters,
			SmallMonsters,

			GreatJagras,
			KuluYaKu,
			PukeiPukei,
			Barroth,
			Jyuratodus,
			TobiKadachi,
			Anjanath,
			Rathian,
			TzitziYaKu,
			Paolumu,
			GreatGirros,
			Radobaan,
			Legiana,
			Odogaron,
			Rathalos,
			Diablos,
			Kirin,
			ZorahMagdaros,
			Dodogama,
			PinkRathian,
			Bazelgeuse,
			Lavasioth,
			Uragaan,
			AzureRathalos,
			BlackDiablos,
			Nergigante,
			Teostra,
			KushalaDaora,
			VaalHazak,
			Xenojiiva,

			KulveTaroth,
			Deviljho,
			Lunastra,
			Behemoth,
			AncientLeshen,

			Beotodus,
			Banbaro,
			ViperTobiKadachi,
			NightshadePaolumu,
			CoralPukeiPukei,
			Barioth,
			Nargacuga,
			Glavenus,
			Tigrex,
			Brachydios,
			ShriekingLegiana,
			FulgurAnjanath,
			AcidicGlavenus,
			EbonyOdogaron,
			Velkhana,
			SeethingBazelgeuse,
			BlackveilVaalHazak,
			Namielle,
			RuinerNergigante,
			SharaIshvalda,

			SavageDeviljho,
			BruteTigrex,
			Zinogre,
			YianGaruga,
			ScarredYianGaruga,
			GoldRathian,
			SilverRathalos,
			Rajang,
			StygianZinogre,
			FuriousRajang,
			RagingBrachydios,
			FrostfangBarioth,
			Safijiiva,
			Alatreon,
			Fatalis
		];

		TargetArray =
		[
			NoPreference,
			SmallMonsters,

			GreatJagras,
			KuluYaKu,
			PukeiPukei,
			Barroth,
			Jyuratodus,
			TobiKadachi,
			Anjanath,
			Rathian,
			TzitziYaKu,
			Paolumu,
			GreatGirros,
			Radobaan,
			Legiana,
			Odogaron,
			Rathalos,
			Diablos,
			Kirin,
			ZorahMagdaros,
			Dodogama,
			PinkRathian,
			Bazelgeuse,
			Lavasioth,
			Uragaan,
			AzureRathalos,
			BlackDiablos,
			Nergigante,
			Teostra,
			KushalaDaora,
			VaalHazak,
			Xenojiiva,

			KulveTaroth,
			Deviljho,
			Lunastra,
			Behemoth,
			AncientLeshen,

			Beotodus,
			Banbaro,
			ViperTobiKadachi,
			NightshadePaolumu,
			CoralPukeiPukei,
			Barioth,
			Nargacuga,
			Glavenus,
			Tigrex,
			Brachydios,
			ShriekingLegiana,
			FulgurAnjanath,
			AcidicGlavenus,
			EbonyOdogaron,
			Velkhana,
			SeethingBazelgeuse,
			BlackveilVaalHazak,
			Namielle,
			RuinerNergigante,
			SharaIshvalda,

			SavageDeviljho,
			BruteTigrex,
			Zinogre,
			YianGaruga,
			ScarredYianGaruga,
			GoldRathian,
			SilverRathalos,
			Rajang,
			StygianZinogre,
			FuriousRajang,
			RagingBrachydios,
			FrostfangBarioth,
			Alatreon,
			Fatalis
		];

		ExpeditionObjectiveArray =
		[
			NoPreference,

			FieldResearchForest,
			FieldResearchWildspire,
			FieldResearchCoral,
			FieldResearchRotted,
			FieldResearchVolcanic,
			FieldResearchTundra,

			MiningForest,
			MiningWildspire,
			MiningCoral,
			MiningRotted,
			MiningVolcanic,
			MiningTundra,

			BoneResearchForest,
			BoneResearchWildspire,
			BoneResearchCoral,
			BoneResearchRotted,
			BoneResearchVolcanic,
			BoneResearchTundra,

			FixedRegionForest,
			FixedRegionWildspire,
			FixedRegionCoral,
			FixedRegionRotted,
			FixedRegionVolcanic,
			FixedRegionTundra
		];

		TargetMonsterArray =
		[
			NoPreference,

			GreatJagras,
			KuluYaKu,
			PukeiPukei,
			Barroth,
			TobiKadachi,
			Anjanath,
			Rathian,
			TzitziYaKu,
			Paolumu,
			GreatGirros,
			Radobaan,
			Legiana,
			Odogaron,
			Rathalos,
			Diablos,
			PinkRathian,
			AzureRathalos,
			BlackDiablos,

			Banbaro,
			NightshadePaolumu,
			CoralPukeiPukei,
			Nargacuga,
			Glavenus,
			Tigrex,
			FulgurAnjanath,
			AcidicGlavenus,
			EbonyOdogaron,

			SavageDeviljho,

			Zinogre,
			BruteTigrex,
			YianGaruga,
			ScarredYianGaruga,

			Kirin,
			KushalaDaora,
			Teostra,
			Lunastra,

			Velkhana,
			BlackveilVaalHazak,
			Namielle,
			RuinerNergigante,
			GoldRathian,
			SilverRathalos,

			Dodogama,
			Lavasioth,
			Uragaan,
			Brachydios,
			SeethingBazelgeuse,
			ViperTobiKadachi,
			Barioth,
			ShriekingLegiana,
			StygianZinogre,
			Rajang
		];

		return this;
	}
}