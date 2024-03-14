using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal static unsafe partial class SteamMatchmaking
{
	[LibraryImport("steam_api64.dll")]
	private static partial nint SteamInternal_ContextInit(nint request);

	private static nint SteamMatchmakingInterfaceGetter { get; set; }

	public static void Init()
	{
		var leaInstruction = PatternScanner.FindFirst(Pattern.FromString("48 8B D6 48 8B 08 48 8B 01 FF 90 88 00 00 00")) - 13;
		var afterLeaInstruction = leaInstruction + 4;
		var offset = MemoryUtil.Read<int>(leaInstruction);
		SteamMatchmakingInterfaceGetter = afterLeaInstruction + offset;

		Log.Debug($"[Matchmaking: Found SteamMatchmaking interface getter at 0x{SteamMatchmakingInterfaceGetter:X}");
	}

	public static nint GetSteamMatchmakingInterface()
	{
		return MemoryUtil.Read<nint>(SteamInternal_ContextInit(SteamMatchmakingInterfaceGetter));
	}
}
