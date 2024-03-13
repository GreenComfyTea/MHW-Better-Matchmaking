using SharpPluginLoader.Core.Entities;
using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class PlayerTypeFilter : SingletonAccessor
{
    // Singleton Pattern
    private static readonly PlayerTypeFilter _singleton = new();

    public static PlayerTypeFilter Instance => _singleton;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static PlayerTypeFilter() { }

    // Singleton Pattern End

    public PlayerTypeFilterCustomization Customization { get; set; }

    private PlayerTypeFilter() { }

    public PlayerTypeFilter Init()
    {
        InstantiateSingletons();
        return this;
    }

    public bool Apply(ref string key, ref int value, ref int comparison)
    {
        if (!Customization.Enabled) return false;
        if (Core_I.CurrentSearchType != SearchTypes.Session) return false;
        if (comparison != (int) LobbyComparison.Equal) return false;
        if (!key.Equals(Constants.SEARCH_KEY_SESSION_PLAYER_TYPE)) return false;
        if (value != (int)Customization.ReplacementTargetEnum) return false;

        TeaLog.Info("PlayerTypeFilter: Skipping Original Filter...");

        if (!Customization.FilterOptions.Beginners)
        {
            TeaLog.Info("CustomQuestRankFilter: Skipping Beginners...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_PLAYER_TYPE, (int)PlayerTypes.Beginners, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.Experienced)
        {
            TeaLog.Info("CustomQuestRankFilter: Skipping Experienced...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_PLAYER_TYPE, (int)PlayerTypes.Experienced, LobbyComparison.NotEqual);
        }

        if (!Customization.FilterOptions.Any)
        {
            TeaLog.Info("CustomQuestRankFilter: Skipping Any...");
            Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SESSION_PLAYER_TYPE, (int)PlayerTypes.Any, LobbyComparison.NotEqual);
        }

        return true;
    }
}
