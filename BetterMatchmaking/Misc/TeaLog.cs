using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

public static class TeaLog
{
	public static void Info(string value) => Log.Info($"[{Constants.MOD_NAME}] {value}");

	public static void Warn(string value) => Log.Warn($"[{Constants.MOD_NAME}] {value}");

	public static void Error(string value) => Log.Error($"[{Constants.MOD_NAME}] {value}");

	public static void Debug(string value) => Log.Debug($"[{Constants.MOD_NAME}] {value}");
}
