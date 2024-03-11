using SharpPluginLoader.Core.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class PlayerCountFilter : SingletonAccessor
{
    // Singleton Pattern
    private static readonly PlayerCountFilter _singleton = new();

    public static PlayerCountFilter Instance => _singleton;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static PlayerCountFilter() { }

    // Singleton Pattern End

    public PlayerCountFilterCustomization Customization { get; set; }

    private PlayerCountFilter()
    {
        InstantiateSingletons();
    }

    public PlayerCountFilter ApplyMin()
    {
        if (Core_I.CurrentSearchType != SearchTypes.Session) return this;
        if (!Customization.Min.Enabled) return this;
        if (Customization.Min.Value == Constants.DEFAULT_SESSION_PLAYER_COUNT_MIN) return this;

        var openSlotsMax = Constants.DEFAULT_SESSION_PLAYER_COUNT_MAX - Customization.Min.Value + 1;

        // if value = 5
        // openSlotsMax = 15 + 1 - 5 = 11
        // filter: openSlots <= openSlotsMax
        // filter: openSlots <= 11

        Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SLOT_PUBLIC_OPEN, openSlotsMax, LobbyComparison.EqualToOrLessThan);

        TeaLog.Info($"PlayerCountFilter: Set Min to {Customization.Min.Value}.");
        return this;
    }

    public PlayerCountFilter ApplyMax()
    {
		if(Core_I.CurrentSearchType != SearchTypes.Session) return this;
		if (!Customization.Max.Enabled) return this;
        if (Customization.Max.Value == Constants.DEFAULT_SESSION_PLAYER_COUNT_MAX) return this;

        var openSlotsMin = Constants.DEFAULT_SESSION_PLAYER_COUNT_MAX - Customization.Max.Value + 1;

        // if value = 14
        // openSlotsMin = 15 + 1 - 14 = 2
        // filter: openSlots >= openSlotsMin
        // filter: openSlots >= 2

        Matchmaking.AddRequestLobbyListNumericalFilter(Constants.SEARCH_KEY_SLOT_PUBLIC_OPEN, openSlotsMin, LobbyComparison.EqualToOrGreaterThan);

        TeaLog.Info($"PlayerCountFilter: Set Max to {Customization.Max.Value}.");
        return this;
    }
}