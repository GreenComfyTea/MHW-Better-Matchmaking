using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal static unsafe partial class SteamApi
{
	[LibraryImport("steam_api64.dll")]
	private static partial nint SteamInternal_ContextInit(nint request);

	private static nint _steamMatchmakingInterfaceGetter;

	public enum VirtualFunctionIndex
	{
		GetFavoriteGameCount = 0,
		GetFavoriteGame = 1,
		AddFavoriteGame = 2,
		RemoveFavoriteGame = 3,
		RequestLobbyList = 4,
		AddRequestLobbyListStringFilter = 5,
		AddRequestLobbyListNumericalFilter = 6,
		AddRequestLobbyListNearValueFilter = 7,
		AddRequestLobbyListFilterSlotsAvailable = 8,
		AddRequestLobbyListDistanceFilter = 9,
		AddRequestLobbyListResultCountFilter = 10,
		AddRequestLobbyListCompatibleMembersFilter = 11,
		GetLobbyByIndex = 12,
		CreateLobby = 13,
		JoinLobby = 14,
		LeaveLobby = 15,
		InviteUserToLobby = 16,
		GetNumLobbyMembers = 17,
		GetLobbyMemberByIndex = 18,
		GetLobbyData = 19,
		SetLobbyData = 20,
		GetLobbyDataCount = 21,
		GetLobbyDataByIndex = 22,
		DeleteLobbyData = 23,
		GetLobbyMemberData = 24,
		SetLobbyMemberData = 25,
		SendLobbyChatMessage = 26,
		GetLobbyChatEntry = 27,
		RequestLobbyData = 28,
		SetLobbyGameServer = 29,
		GetLobbyGameServer = 30,
		SetLobbyMemberLimit = 31,
		GetLobbyMemberLimit = 32,
		SetLobbyType = 33,
		SetLobbyJoinable = 34,
		GetLobbyOwner = 35,
		SetLobbyOwner = 36,
		SetLinkedLobby = 37,
	}

	public static void Init()
	{
		var leaInstruction = PatternScanner.FindFirst(Pattern.FromString("48 8B D6 48 8B 08 48 8B 01 FF 90 88 00 00 00")) - 13;
		TeaLog.Info($"SteamAPI: Found Lea Instruction at 0x{leaInstruction:X}");

		var afterLeaInstruction = leaInstruction + 4;
		var offset = MemoryUtil.Read<int>(leaInstruction);
		TeaLog.Info($"SteamAPI: Found Offset {offset:X}");

		_steamMatchmakingInterfaceGetter = afterLeaInstruction + offset;

		TeaLog.Info($"SteamAPI: Found SteamMatchmaking Interface Getter at 0x{_steamMatchmakingInterfaceGetter:X}");
	}

	public static void AddRequestLobbyListResultCountFilter(int maxResults)
	{
		var steamInterface = GetSteamMatchmakingInterface();
		new NativeAction<nint, int>(
			GetVirtualFunction(steamInterface, VirtualFunctionIndex.AddRequestLobbyListResultCountFilter)
		).Invoke(steamInterface, maxResults);
	}

	public static nint GetVirtualFunction(VirtualFunctionIndex functionIndex)
	{
		var steamInterface = GetSteamMatchmakingInterface();
		return GetVirtualFunction(steamInterface, functionIndex);
	}

	private static nint GetVirtualFunction(nint steamInterface, VirtualFunctionIndex functionIndex)
	{
		var vtable = *(nint*) steamInterface;
		return MemoryUtil.Read<nint>(vtable + (int) functionIndex * nint.Size);
	}

	private static nint GetSteamMatchmakingInterface()
	{
		return MemoryUtil.Read<nint>(SteamInternal_ContextInit(_steamMatchmakingInterfaceGetter));
	}
}
