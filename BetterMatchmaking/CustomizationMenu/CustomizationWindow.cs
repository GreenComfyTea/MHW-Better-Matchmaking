using ImGuiNET;
using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Configuration;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.View;
using System;
using System.Collections.Generic;
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

		private CustomizationWindow() { }

		public CustomizationWindow Init()
		{
			InstantiateSingletons();
			return this;
		}

		public CustomizationWindow Render()
		{
			if (!IsOpened) return this;

			//var font = ImGui.GetFont();
			//var oldScale = font.Scale;
			//font.Scale *= 1.5f;

			try
			{
				var changed = false;

				ImGui.SetNextWindowSize(new Vector2(640, 640));

				//ImGui.PushFont(font);
				ImGui.Begin($"{Constants.MOD_NAME} v{Constants.VERSION}", ref _isOpened);

				ImGui.Text(LocalizationManager_I.ImGui.MadeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.MOD_AUTHOR_COLOR, Constants.MOD_AUTHOR);

				if (ImGui.Button(LocalizationManager_I.ImGui.NexusMods)) Utils.OpenLink(Constants.NEXUSMODS_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManager_I.ImGui.GitHubRepo)) Utils.OpenLink(Constants.GITHUB_REPO_LINK);

				if (ImGui.Button(LocalizationManager_I.ImGui.Twitch)) Utils.OpenLink(Constants.TWITCH_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManager_I.ImGui.Twitter)) Utils.OpenLink(Constants.TWITTER_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManager_I.ImGui.ArtStation)) Utils.OpenLink(Constants.ARTSTATION_LINK);

				ImGui.Text(LocalizationManager_I.ImGui.DonationMessage1);
				ImGui.Text(LocalizationManager_I.ImGui.DonationMessage2);

				if (ImGui.Button(LocalizationManager_I.ImGui.Donate)) Utils.OpenLink(Constants.STREAMELEMENTS_TIP_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManager_I.ImGui.PayPal)) Utils.OpenLink(Constants.PAYPAL_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManager_I.ImGui.BuyMeATea)) Utils.OpenLink(Constants.KOFI_LINK);

				ImGui.Separator();
				ImGui.NewLine();
				ImGui.Separator();

				ConfigManager_I.Customization.RenderImGui();
				changed = LocalizationManager_I.Customization.RenderImGui() || changed;
				changed = DebugManager_I.Customization.RenderImGui() || changed;

				ImGui.Separator();
				ImGui.NewLine();
				ImGui.Separator();

				changed = RegionLockFix_I.Customization.RenderImGui() || changed;
				changed = MaxSearchResultLimit_I.Customization.RenderImGui() || changed;
				changed = SessionPlayerCountFilter_I.Customization.RenderImGui() || changed;
				changed = ConfigManager_I.Current.InGameFilterOverride.RenderImGui() || changed;

				ImGui.End();

				if (changed)
				{
					ConfigManager_I.Current.Save();
				}

				return this;
			}
			catch (Exception e)
			{
				TeaLog.Error(e.ToString());
				return this;
				//font.Scale = oldScale;
				//ImGui.PopFont();
			}
		}
	}
}
