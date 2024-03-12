using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterMatchmaking;

public enum OutlineModes { Outside, Center, Inside }
public enum FillDirections { LeftToRight, RightToLeft, TopToBottom, BottomToTop }
public enum PlayerTypes { Beginners, Experienced, Any, Matchmake }
public enum SimilarHunterRank { Disabled, Enabled = 6 }
public enum SimilarMasterRank { Disabled, Enabled = 10 }

public enum Rewards { NoRewards, RewardsAvailable }
public enum RewardTypes { NoPreference, RewardsAvailable }

public enum SearchTypes
{
	None = 0,
	Session = 537292564,
	Quest = 421652,
	GuidingLands = 1074163476
}

public enum GuidingLands
{
	No = 17,
	Yes = 273
}

public enum SessionSearchKeyIDs
{
	PlayerType = 1,
	QuestPreference = 2,
	Language = 4,
	SimilarHunterRank = 5,
	SimilarMasterRank = 6
}

public enum QuestSearchKeyIDs
{
	RewardsAvailable = 1,
	Target = 2,
	Rank = 4,
	Language = 5,
	QuestType = 6
}

public enum GuidingLandsSearchKeyIDs
{
	IsGuidingLands = 1,
	ExpeditionObjective = 2,
	TargetMonster = 3,
	Conditions = 4,
	Language = 5,
	RegionLevel = 6
}

public enum LanguageSearchTypes { SameLanguage, AnyLanguage };

public enum Languages
{
	Matchmake = -1,
	Japanese = 0,
	English = 1,
	French = 2,
	Italian = 5,
	German = 4,
	Spanish = 3,
	BrazilianPortuguese = 21,
	Polish = 11,
	Russian = 10,
	Korean = 6,
	TraditionalChinese = 7,
	SimplifiedChinese = 8,
	Arabic = 22,
	LatinAmericanSpanish = 23
}

public enum QuestTypes
{
	NoPreference = 0,
	OptionalQuests = 1,
	Assignments = 2,
	Investigations = 3,
	Expeditions = 4,
	EventQuests = 5,
	SpecialInvestigations = 6
}

public enum Difficulties
{
	LowRank1 = 1,
	LowRank2 = 2,
	LowRank3 = 3,
	LowRank4 = 4,
	LowRank5 = 5,

	HighRank6 = 6,
	HighRank7 = 7,
	HighRank8 = 8,
	HighRank9 = 9,

	HighRankSearch = 10,

	MasterRank1 = 11,
	MasterRank2 = 12,
	MasterRank3 = 13,
	MasterRank4 = 14,
	MasterRank5 = 15,
	MasterRank6 = 16,

	LowRank = 20,
	HighRank = 21,
	MasterRank = 22
}

public enum Targets
{
	None = 0,
	Assignments = 1,
	Optional = 2,
	Investigation = 3,
	TheGuidingLandsExpedition = 1710092,
	EventQuests = 4,
	SpecialAssignments = 699999,
	Arena = 5,
	Expeditions = 6,
	TemperedMonsters = 710099,
	SmallMonsters =			130000,

	GreatJagras =			130001,
	KuluYaKu =				200000,
	PukeiPukei =			210000,
	Barroth =				300000,
	Jyuratodus =			310000,
	TobiKadachi =			320000,
	Anjanath =				330000,
	Rathian =				420000,
	TzitziYaKu =			420001,
	Paolumu =				420002,
	GreatGirros =			430000,
	Radobaan =				430001,
	Legiana =				440000,
	Odogaron =				500000,
	Rathalos =				510000,
	Diablos =				510001,
	Kirin =					510002,
	ZorahMagdaros =			400350,
	Dodogama =				610000,
	PinkRathian =			620000,
	Bazelgeuse =			620001,
	Lavasioth =				620002,
	Uragaan =				620003,
	AzureRathalos =			630004,
	BlackDiablos =			620005,
	Nergigante =			710000,
	Teostra =				800000,
	KushalaDaora =			800001,
	VaalHazak =				800002,
	Xenojiiva =				810000,

	KulveTaroth =			900440,
	Deviljho =				700000,
	Lunastra =				900450,
	Behemoth =				900449,
	AncientLeshen =			900448,

	Beotodus =				1100100,
	Banbaro =				1100400,
	ViperTobiKadachi =		1200200,
	NightshadePaolumu =		1210199,
	CoralPukeiPukei =		1210200,
	Barioth =				1300200,
	Nargacuga =				1320099,
	Glavenus =				1320100,
	Tigrex =				1330099,
	Brachydios =			1330100,
	ShriekingLegiana =		1400200,
	FulgurAnjanath =		1410100,
	AcidicGlavenus =		1420199,
	EbonyOdogaron =			1420200,
	Velkhana =				1330500,
	SeethingBazelgeuse =	1510200,
	BlackveilVaalHazak =	1520300,
	Namielle =				1530400,
	RuinerNergigante =		1600100,
	SharaIshvalda =			1600500,

	SavageDeviljho =		1710094,
	BruteTigrex =			1710095,
	Zinogre =				1710096,
	YianGaruga =			1710097,
	ScarredYianGaruga =		1710098,
	GoldRathian =			1710099,
	SilverRathalos =		1710100,
	Rajang =				1600009,
	StygianZinogre =		1600008,
	FuriousRajang =			1600010,
	RagingBrachydios =		1600011,
	FrostfangBarioth =		1510000,
	Safijiiva =				1600013,
	Alatreon =				1600012,
	Fatalis =				1600014
}

public static class Constants
{
	public const string MOD_AUTHOR = "GreenComfyTea";
	public const string MOD_NAME = "Better Matchmaking";
	public const string MOD_FOLDER_NAME = "BetterMatchmaking";

	public const string VERSION = "2.0.0";

	public const float EPSILON = 0.000001f;

	public const string DEFAULT_LOCALIZATION = "en-us";

	public const string REPO_PATH = $@"E:\GitHub\MHW-Better-Matchmaking\{MOD_FOLDER_NAME}\";

	public const string PLUGIN_PATH = $@"nativePC\plugins\CSharp\{MOD_FOLDER_NAME}\";
	public const string PLUGIN_DATA_PATH = $@"{PLUGIN_PATH}data\";
	public const string LOCALIZATIONS_PATH = $@"{PLUGIN_DATA_PATH}localizations\";

	public const string DEFAULT_CONFIG = "config";
	public const string DEFAULT_CONFIG_WITH_EXTENSION = $"{DEFAULT_CONFIG}.json";
	public const string DEFAULT_CONFIG_FILE_PATH_NAME = $"{PLUGIN_DATA_PATH}{DEFAULT_CONFIG_WITH_EXTENSION}";

	public const float DRAG_FLOAT_SPEED = 0.1f;
	public const float DRAG_FLOAT_MAX = 15360f;
	public const float DRAG_FLOAT_MIN = -DRAG_FLOAT_MAX;
	public const string DRAG_FLOAT_FORMAT = "0.0";

	public static readonly Vector4 MOD_AUTHOR_COLOR = new(0.702f, 0.851f, 0.424f, 1f);
	public static readonly Vector4 IMGUI_USERNAME_COLOR = new(0.5f, 0.710f, 1f, 1f);

	public const string NEXUSMODS_LINK = "https://nexusmods.com";
	public const string GITHUB_REPO_LINK = "https://github.com/GreenComfyTea/MHW-Better-Matchmaking";
	public const string TWITCH_LINK = "https://twitch.tv/GreenComfyTea";
	public const string TWITTER_LINK = "https://twitter.com/GreenComfyTea";
	public const string ARTSTATION_LINK = "https://greencomfytea.artstation.com";
	public const string STREAMELEMENTS_TIP_LINK = "https://streamelements.com/greencomfytea/tip";
	public const string PAYPAL_LINK = "https://paypal.me/greencomfytea";
	public const string KOFI_LINK = "https://ko-fi.com/greencomfytea";

	public const int DEFAULT_SEARCH_RESULT_LIMIT_MAX = 20;

	public const int SEARCH_RESULT_LIMIT_MAX = 32;

	public const int DEFAULT_SESSION_PLAYER_COUNT_MIN = 1;
	public const int DEFAULT_SESSION_PLAYER_COUNT_MAX = 15;

	public const string SEARCH_KEY_NAME = "Name";
	public const string SEARCH_KEY_OWNER_ID = "OwnerId";
	public const string SEARCH_KEY_IS_BEHIND_NAT = "IsBehindNAT";
	public const string SEARCH_KEY_SLOT_PUBLIC_MAX = "SlotPublicMax";
	public const string SEARCH_KEY_SLOT_PUBLIC_OPEN = "SlotPublicOpen";
	public const string SEARCH_KEY_SLOT_PRIVATE_MAX = "SlotPrivateMax";
	public const string SEARCH_KEY_SLOT_PRIVATE_OPEN = "SlotPrivateOpen";
	public const string SEARCH_KEY_NUM = "SearchKeyNum";
	public const string SEARCH_KEY_D = "SearchKey%d";
	public const string SEARCH_KEY_BINARY_SIZE = "BinarySize";
	public const string SEARCH_KEY_BINARY_DATA = "BinaryData";

	public const int SEARCH_KEY_SEARCH_TYPE_ID = 0;

	public const string SEARCH_KEY_SESSION_PLAYER_TYPE = "SearchKey1";
	public const string SEARCH_KEY_SESSION_QUEST_PREFERENCE = "SearchKey2";
	public const string SEARCH_KEY_SESSION_LANGUAGE = "SearchKey4";
	public const string SEARCH_KEY_SESSION_SIMILAR_HUNTER_RANK = "SearchKey5";
	public const string SEARCH_KEY_SESSION_SIMILAR_MASTER_RANK = "SearchKey6";

	public const string SEARCH_KEY_SESSION_QUEST_TYPE = SEARCH_KEY_SESSION_SIMILAR_MASTER_RANK;
	public const string SEARCH_KEY_SESSION_QUEST_DIFFICULTY = SEARCH_KEY_SESSION_LANGUAGE;
	public const string SEARCH_KEY_SESSION_QUEST_REWARDS = SEARCH_KEY_SESSION_QUEST_PREFERENCE;
	public const string SEARCH_KEY_QUEST_LANGUAGE = SEARCH_KEY_SESSION_SIMILAR_HUNTER_RANK;
	public const string SEARCH_KEY_QUEST_TARGET = "SearchKey3";

	public const string SEARCH_KEY_GUIDING_LANDS_LANGUAGE = SEARCH_KEY_QUEST_LANGUAGE;

	public static readonly Vector4 IMGUI_RED_COLOR = new(1f, 0.25f, 0.25f, 1f);
	public static readonly Vector4 IMGUI_YELLOW_COLOR = new(1f, 0.75f, 0.5f, 1f);
	public static readonly Vector4 IMGUI_BLUE_COLOR = new(0.4f, 0.6f, 1f, 1f);
	public static readonly Vector4 IMGUI_GREEN_COLOR = new(0.5f, 1f, 0.5f, 1f);
	public static readonly Vector4 IMGUI_PURPLE_COLOR = new(0.666f, 0.4f, 0.666f, 1f);
	public static readonly Vector4 IMGUI_LIGHT_GREEN_COLOR = new(0.4f, 0.666f, 0.4f, 1f);

	public const float COMBOBOX_WIDTH_MULTIPLIER = 0.4f;

	public static readonly Vector2 DEFAULT_WINDOW_POSITION = new(480, 60);
	public static readonly Vector2 DEFAULT_WINDOW_SIZE = new(600, 500);
}