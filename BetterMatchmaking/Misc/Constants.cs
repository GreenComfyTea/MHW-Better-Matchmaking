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

public enum SearchTypes { None, Session, Quest }

public static class Constants
{
	public const string MOD_AUTHOR = "GreenComfyTea";
	public const string MOD_NAME = "Better Matchmaking";
	public const string MOD_FOLDER_NAME = "BetterMatchmaking";

	public const string VERSION = "2.0.0";

	public const float EPSILON = 0.000001f;

	public const string DEFAULT_LOCALIZATION = "en-us";

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

	public const int SEARCH_RESULT_LIMIT_MAX_SESSIONS = 32;
	public const int SEARCH_RESULT_LIMIT_MAX_QUESTS = 31;

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

	public const char SEARCH_KEY_SESSION_PLAYER_TYPE_ID = '1';
	public const char SEARCH_KEY_SESSION_QUEST_PREFERENCE_ID = '2';

	public const char SEARCH_KEY_SESSION_LANGUAGE_ID = '4';
	public const char SEARCH_KEY_SESSION_SIMILAR_HUNTER_RANK_ID = '5';
	public const char SEARCH_KEY_SESSION_SIMILAR_MASTER_RANK_ID = '6';

	public const char SEARCH_KEY_QUEST_REWARDS_AVAILABLE_ID = '2';
	public const char SEARCH_KEY_QUEST_TARGET_ID = '3';
	public const char SEARCH_KEY_QUEST_RANK_ID = '4';
	public const char SEARCH_KEY_QUEST_LANGUAGE_ID = '5';
	public const char SEARCH_KEY_QUEST_TYPE_ID = '6';

	public const int SESSION_SEARCH_ID = 537292564;
	public const int QUEST_SEARCH_ID = 421652;
}