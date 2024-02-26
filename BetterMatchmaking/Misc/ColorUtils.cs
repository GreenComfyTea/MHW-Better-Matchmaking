using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

public static class ColorUtils
{
	public static void AbgrUintToIndividuals(uint abgrUint, out byte red, out byte green, out byte blue, out byte alpha)
	{
		alpha = (byte)((abgrUint >> 24) & 0xFF);
		blue = (byte)((abgrUint >> 16) & 0xFF);
		green = (byte)((abgrUint >> 8) & 0xFF);
		red = (byte)(abgrUint & 0xFF);
	}

	public static void RgbaUintToIndividuals(uint rgbaUint, out byte red, out byte green, out byte blue, out byte alpha)
	{
		red = (byte)((rgbaUint >> 24) & 0xFF);
		green = (byte)((rgbaUint >> 16) & 0xFF);
		blue = (byte)((rgbaUint >> 8) & 0xFF);
		alpha = (byte)(rgbaUint & 0xFF);
	}

	public static uint RgbaToAbgr(Vector4 colorRgba)
	{
		return (0x1000000 * Convert.ToUInt32(255 * colorRgba.W))
			+ (0x10000 * Convert.ToUInt32(255 * colorRgba.Z))
			+ (0x100 * Convert.ToUInt32(255 * colorRgba.Y))
			+ Convert.ToUInt32(255 * colorRgba.X);
	}

	public static void RgbaToIndividuals(Vector4 colorRgba, out byte red, out byte green, out byte blue, out byte alpha)
	{
		red = Convert.ToByte(255 * colorRgba.X);
		green = Convert.ToByte(255 * colorRgba.Y);
		blue = Convert.ToByte(255 * colorRgba.Z);
		alpha = Convert.ToByte(255 * colorRgba.W);
	}


	public static string IndividualsToRgbaString(byte red, byte green, byte blue, byte alpha)
	{
		return $"0x{red:X2}{green:X2}{blue:X2}{alpha:X2}";
	}

	public static uint IndividualsToAbgrUint(byte red, byte green, byte blue, byte alpha)
	{
		return (uint)((0x1000000 * alpha)
			+ (0x10000 * blue)
			+ (0x100 * green)
			+ red);
	}

	public static Vector4 IndividualsToRgbaVector(byte red, byte green, byte blue, byte alpha)
	{
		return new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
	}

	public static void IndividualsToRgbaVector(Vector4 rgbaVector, byte red, byte green, byte blue, byte alpha)
	{
		rgbaVector.X = red / 255f;
		rgbaVector.Y = green / 255f;
		rgbaVector.Z = blue / 255f;
		rgbaVector.W = alpha / 255f;
	}

	public static bool UpdateColorsFromRgbaString(ref string rgbaString, ref uint abgrUint, Vector4 rgbaVector,
		ref byte red, ref byte green, ref byte blue, ref byte alpha)
	{
		var rgbaPureString = rgbaString;

		if (rgbaString.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase) ||
			rgbaString.StartsWith("&H", StringComparison.CurrentCultureIgnoreCase))
		{
			rgbaPureString = rgbaString[2..];
		}
		else if (rgbaString.StartsWith("#", StringComparison.CurrentCultureIgnoreCase))
		{
			rgbaPureString = rgbaString[1..];

		}

		uint parsedRgbaUint;
		bool success = uint.TryParse(rgbaPureString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out parsedRgbaUint);

		if (!success) return false;

		RgbaUintToIndividuals(parsedRgbaUint, out red, out green, out blue, out alpha);
		rgbaString = "0x" + rgbaPureString;
		abgrUint = IndividualsToAbgrUint(red, green, blue, alpha);
		IndividualsToRgbaVector(rgbaVector, red, green, blue, alpha);

		return true;
	}

	public static void UpdateColorsFromRgbaVector(Vector4 rgbaVector, ref string rgbaString, ref uint abgrUint,
		ref byte red, ref byte green, ref byte blue, ref byte alpha)
	{
		RgbaToIndividuals(rgbaVector, out red, out green, out blue, out alpha);
		rgbaString = IndividualsToRgbaString(red, green, blue, alpha);
		abgrUint = IndividualsToAbgrUint(red, green, blue, alpha);
	}

	public static uint ScaleColorOpacity(byte red, byte green, byte blue, byte alpha, float opacityScale)
	{
		var scaledAlpha = Convert.ToByte(opacityScale * alpha);

		return IndividualsToAbgrUint(red, green, blue, scaledAlpha);
	}
}
