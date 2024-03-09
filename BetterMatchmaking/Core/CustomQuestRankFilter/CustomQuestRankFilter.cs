using SharpPluginLoader.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class CustomQuestRankFilter : SingletonAccessor
{
	// Singleton Pattern
	private static readonly CustomQuestRankFilter _singleton = new();

	public static CustomQuestRankFilter Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static CustomQuestRankFilter() { }

	// Singleton Pattern End

	public CustomQuestRankFilterCustomization Customization { get; set; }

	private CustomQuestRankFilter() { }

	public CustomQuestRankFilter Init()
	{
		InstantiateSingletons();
		return this;
	}

	public bool Apply(ref string key, ref int value, ref int comparison)
	{
		if(Customization.Enabled) return false;
		if(Core_I.CurrentSearchType != SearchTypes.Quest) return false;

		if(key.Equals(Constants.SEARCH_KEY_QUEST_RANK)
		&& value == (int) PlayerTypes.Any)
		{
			TeaLog.Info($"CustomQuestRankFilter: ...");
			//return true;
		}

		return false;
	}
}
