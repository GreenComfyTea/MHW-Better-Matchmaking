using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking
{
	public static class TeaLog
	{

		public static void Info(string text)
		{
			Log.Info($"[{Constants.MOD_NAME}] {text}");
		}

		public static void Warn(string text)
		{

			Log.Warn($"[{Constants.MOD_NAME}] {text}");
		}

		public static void Error(string text)
		{
			Log.Error($"[{Constants.MOD_NAME}] {text}");
		}

		public static void Debug(string text)
		{
			Log.Debug($"[{Constants.MOD_NAME}] {text}");
		}
	}
}
