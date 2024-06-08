using ABI.System.Collections.Generic;
using SharpPluginLoader.Core.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.UI.Notifications;

using GlyphRange = (ushort start, ushort end);

namespace BetterMatchmaking;

internal sealed class FontManager : SingletonAccessor, IDisposable
{

	// Singleton Pattern
	private static readonly FontManager _singleton = new();

	public static FontManager Instance { get { return _singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static FontManager() { }

	// Singleton Pattern End

	private FontManager() { }

	private bool IsInitialized { get; set; } = false;

	private FontCustomization _customization;
	public FontCustomization Customization { get => _customization; set => _customization = value; }

	private GlyphRange[] FullGlyphRange { get; } = [(0x0020, ushort.MaxValue)];
	private GlyphRange[] EmojiGlyphRange { get; } = [(0x2122, 0x2B55)];

	private List<nint> Ranges { get; set; } = new();

	public Dictionary<string, string> Fonts { get; set; } = new();

	private List<string> FontNames { get; set; } = new();

	private List<string> LabeledFontNames { get; set; } = new();

	private string _currentFont = string.Empty;

	public string CurrentFont { get => _currentFont; set => _currentFont = value; }

	public FontManager Init()
	{
		TeaLog.Info("FontManager: Initializing...");

		IsInitialized = true;

		InstantiateSingletons();

		LoadAllFonts();
		SetCurrentFont(LocalizationManager_I.Current);

		TeaLog.Info("FontManager: Initialization Done!");

		return this;
	}

	public FontManager LoadAllFonts()
	{
		TeaLog.Info("FontManager: Loading All Fonts...");

		var emojiGlyphRangeAddress = GlyphRangeFactory.CreateGlyphRanges(EmojiGlyphRange);
		Ranges.Add(emojiGlyphRangeAddress);

		foreach(var localizationPair in LocalizationManager_I.Localizations)
		{
			var localizationIsoName = localizationPair.Key;
			var localization = localizationPair.Value;

			if(Fonts.TryGetValue(localization.IsoName, out _)) continue;

			Fonts[localizationIsoName] = LoadFont(localization, emojiGlyphRangeAddress);
		}

		TeaLog.Info("FontManager: Loading All Fonts Done!");
		return this;
	}

	public string LoadFont(Localization localization, nint emojiGlyphRangeAddress)
	{
		var fontConfig = ConfigManager_I.Current.Fonts;

		var fontInfo = localization.FontInfo;
		var fontName = fontInfo.Name;

		TeaLog.Info($"Font {fontName}: Loading...");

		var labeledFontName = $"{fontName}##{Constants.MOD_FOLDER_NAME}";

		if(LabeledFontNames.Contains(labeledFontName))
		{
			TeaLog.Info($"Font {fontName}: Already Loaded. Skipping.");
			return labeledFontName;
		}

		GlyphRange[] glyphRanges = GetGlyphRanges(localization);

		var glyphRangesAddress = GlyphRangeFactory.CreateGlyphRanges(glyphRanges);
		Ranges.Add(glyphRangesAddress);

		FontCustomization customization;
		var fontCustomizationExist = fontConfig.TryGetValue(fontName, out customization);

		if(!fontCustomizationExist)
		{
			customization = new();
			fontConfig[fontName] = customization;
		}

		Renderer.RegisterFont(
			labeledFontName,
			$"{Constants.PLUGIN_FONTS_PATH}{fontName}",
			customization.Size,
			glyphRangesAddress,
			false,
			customization.VerticalOversample,
			customization.HorizontalOversample
		);

		Renderer.RegisterFont(
			labeledFontName,
			$"{Constants.PLUGIN_FONTS_PATH}{Constants.EMOJI_FONT}",
			customization.Size,
			emojiGlyphRangeAddress,
			true,
			customization.VerticalOversample,
			customization.HorizontalOversample
		);

		LabeledFontNames.Add(labeledFontName);
		FontNames.Add(fontName);

		TeaLog.Info($"Font {fontName}: Loading Done!");
		return labeledFontName;
	}

	public FontManager SetCurrentFont(Localization localization)
	{
		Fonts.TryGetValue(localization.IsoName, out _currentFont);

		if(!IsInitialized) return this;

		ConfigManager_I.Current.Fonts.TryGetValue(localization.FontInfo.Name, out _customization);

		return this;
	}

	public FontManager RecreateFontCustomizations()
	{
		var fontConfig = ConfigManager_I.Current.Fonts;

		foreach(var fontName in FontNames)
		{
			fontConfig[fontName] = new();
		}

		return this;
	}

	private static GlyphRange[] GetGlyphRanges(Localization localization)
	{
		var glyphRangeStringArray = localization.FontInfo.GlyphRanges;

		var glyphRanges = new GlyphRange[glyphRangeStringArray.Length / 2];

		var j = 0;
		for(var i = 0; i < glyphRangeStringArray.Length; i += 2)
		{
			var rangeStart = Convert.ToUInt16(glyphRangeStringArray[i], 16);
			var rangeEnd = Convert.ToUInt16(glyphRangeStringArray[i + 1], 16);

			glyphRanges[j] = (rangeStart, rangeEnd);
			j++;
		}

		return glyphRanges;
	}

	public void Dispose()
	{
		foreach(var range in Ranges)
		{
			GlyphRangeFactory.DestroyGlyphRanges(range);
		}
	}
}
