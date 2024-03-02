using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

public static class Utils
{
	public static int Clamp(int value, int min, int max)
	{
		if(value < min)
		{
			return min;
		}
		else if(value > max)
		{
			return max;
		}

		return value;
	}

	public static float Clamp(float value, float min, float max) {
		if(value < min)
		{
			return min;
		}
		else if(value > max)
		{
			return max;
		}

		return value;
	}

	public static bool IsApproximatelyEqual(float a, float b)
	{
		return Math.Abs(a - b) <= Constants.EPSILON;
	}

	public static void OpenLink(string url)
	{
		try
		{
			Process.Start(url);
		}
		catch(Exception exception)
		{
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				url = url.Replace("&", "^&");
				Process.Start(new ProcessStartInfo(url) { FileName = url, UseShellExecute = true });
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Process.Start("xdg-open", url);
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", url);
			}
			else
			{
				TeaLog.Warn(exception.ToString());
			}
		}
	}

	public static void ImGuiEndRect(float additionalSize = 0f, float rounding = -1f, float thickness = 1f)
	{
		ImGui.EndGroup();

		float _rounding = rounding < 0f ? ImGui.GetStyle().FrameRounding : rounding;

		var itemRectMin = ImGui.GetItemRectMin();
		itemRectMin.X -= additionalSize;
		itemRectMin.Y -= additionalSize;

		var itemRectMax = ImGui.GetItemRectMax();
		itemRectMax.X += additionalSize;
		itemRectMax.Y += additionalSize;

		ImGui.GetWindowDrawList().AddRect(itemRectMin, itemRectMax, ImGui.GetColorU32(ImGuiCol.Border), _rounding, ImDrawFlags.RoundCornersAll, thickness);
		ImGui.NewLine();
	}
}
