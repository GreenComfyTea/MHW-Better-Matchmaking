using ImGuiNET;
using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Configuration;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Rendering;
using SharpPluginLoader.Core.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BetterMatchmaking
{
	internal sealed class CustomizationWindow : SingletonAccessor
	{
		// Singleton Pattern
		private static readonly CustomizationWindow _singleton = new();

		public static CustomizationWindow Instance { get { return _singleton; } }

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static CustomizationWindow() { }

		// Singleton Pattern End

		private bool _isOpened = false;
		public bool IsOpened { get => _isOpened; set => _isOpened = value; }

		public float ComboBoxWidth { get; set; } = 100f;

		private bool IsForceModInfoOpen { get; set; } = true;

		private CustomizationWindow() { }

		public CustomizationWindow Init()
		{
			InstantiateSingletons();

			return this;
		}

		private static ImFontPtr Font { get; set; }
		private static bool IsFontInitialized { get; set; } = false;


		public static unsafe void InitFont()
		{
			IsFontInitialized = true;

			var io = ImGuiNative.igGetIO();
			var fonts = io->Fonts;

			ImFontConfig* config = ImGuiNative.ImFontConfig_ImFontConfig();
			config->MergeMode = 0;
			config->FontDataOwnedByAtlas = 0;

			//fonts.AddFontDefault();

			//config->MergeMode = 1;

			var path = Encoding.ASCII.GetBytes(@"\nativePC\plugins\CSharp\BetterMatchmaking\data\NotoSansKR-Bold.otf");

			fixed(byte* pathPointer = &path[0])
			{
				ImGuiNative.ImFontAtlas_AddFontFromFileTTF(fonts, pathPointer, 26f, config, ImGuiNative.ImFontAtlas_GetGlyphRangesKorean(fonts));
			}

			ImGuiNative.ImFontAtlas_Build(fonts);
			

			//Font = fonts.AddFontFromFileTTF(Path.Combine(@"D:\Programs\Steam\steamapps\common\Monster Hunter World\nativePC\plugins\CSharp\BetterMatchmaking\data\NotoSansKR-Bold.otf"), 26f, config, fonts.GetGlyphRangesKorean());

			//fonts.GetTexDataAsRGBA32(out byte* pixels, out var texDataWidth, out var texDataHeight);

			//var texture = LoadTexture("./path/to/texture.png", out var width, out var height);

			//fonts.SetTexID(texture);
			//fonts.ClearTexData();
		}

		public CustomizationWindow Render()
		{
			if(!IsOpened) return this;

			//if(!IsFontInitialized) InitFont();

			ImGui.PushFont(Renderer.KoreanFont);
			ImGui.DebugTextEncoding("안녕하세요");
			ImGui.PopFont();

			try
			{
				var changed = false;

				ImGui.SetNextWindowPos(Constants.DEFAULT_WINDOW_POSITION, ImGuiCond.FirstUseEver);
				ImGui.SetNextWindowSize(Constants.DEFAULT_WINDOW_SIZE, ImGuiCond.FirstUseEver);

				ImGui.ShowMetricsWindow();

				ImGui.Begin($"{Constants.MOD_NAME} v{Constants.VERSION}", ref _isOpened);

				ComboBoxWidth = Constants.COMBOBOX_WIDTH_MULTIPLIER * ImGui.GetWindowSize().X;

				if(IsForceModInfoOpen) ImGui.SetNextItemOpen(true);

				if(ImGui.TreeNode(LocalizationManager_I.ImGui.ModInfo))
				{
					ImGui.Text(LocalizationManager_I.ImGui.MadeBy);
					ImGui.SameLine();
					ImGui.TextColored(Constants.MOD_AUTHOR_COLOR, Constants.MOD_AUTHOR);

					if(ImGui.Button(LocalizationManager_I.ImGui.NexusMods)) Utils.OpenLink(Constants.NEXUSMODS_LINK);
					ImGui.SameLine();
					if(ImGui.Button(LocalizationManager_I.ImGui.GitHubRepo)) Utils.OpenLink(Constants.GITHUB_REPO_LINK);

					if(ImGui.Button(LocalizationManager_I.ImGui.Twitch)) Utils.OpenLink(Constants.TWITCH_LINK);
					ImGui.SameLine();
					if(ImGui.Button(LocalizationManager_I.ImGui.Twitter)) Utils.OpenLink(Constants.TWITTER_LINK);
					ImGui.SameLine();
					if(ImGui.Button(LocalizationManager_I.ImGui.ArtStation)) Utils.OpenLink(Constants.ARTSTATION_LINK);

					ImGui.Text(LocalizationManager_I.ImGui.DonationMessage1);
					ImGui.Text(LocalizationManager_I.ImGui.DonationMessage2);

					if(ImGui.Button(LocalizationManager_I.ImGui.Donate)) Utils.OpenLink(Constants.STREAMELEMENTS_TIP_LINK);
					ImGui.SameLine();
					if(ImGui.Button(LocalizationManager_I.ImGui.PayPal)) Utils.OpenLink(Constants.PAYPAL_LINK);
					ImGui.SameLine();
					if(ImGui.Button(LocalizationManager_I.ImGui.BuyMeATea)) Utils.OpenLink(Constants.KOFI_LINK);

					ImGui.TreePop();
				}
				else
				{
					IsForceModInfoOpen = false;
				}

				ImGui.Separator();
				ImGui.NewLine();
				ImGui.Separator();

				ConfigManager_I.Customization.RenderImGui();
				changed = LocalizationManager_I.Customization.RenderImGui() || changed;
				changed = DebugManager_I.Customization.RenderImGui() || changed;

				ImGui.Separator();
				ImGui.NewLine();
				ImGui.Separator();

				changed = ConfigManager_I.Current.Sessions.RenderImGui() || changed;
				changed = ConfigManager_I.Current.Quests.RenderImGui() || changed;
				changed = ConfigManager_I.Current.GuidingLands.RenderImGui() || changed;

				ImGui.End();

				if (changed) ConfigManager_I.Current.Save();

				return this;
			}
			catch (Exception exception)
			{
				ImGui.End();

				DebugManager_I.Report("CustomizationWindow.Render()", exception.ToString());

				return this;
			}
		}
	}
}
