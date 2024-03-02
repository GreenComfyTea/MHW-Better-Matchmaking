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

				//ImGui.PushFont(font);
				ImGui.Begin($"{Constants.MOD_NAME} v{Constants.VERSION}", ref _isOpened);

				ImGui.Text(LocalizationManagerInstance.ImGui.MadeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.MOD_AUTHOR_COLOR, Constants.MOD_AUTHOR);

				if (ImGui.Button(LocalizationManagerInstance.ImGui.NexusMods)) Utils.OpenLink(Constants.NEXUSMODS_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManagerInstance.ImGui.GitHubRepo)) Utils.OpenLink(Constants.GITHUB_REPO_LINK);

				if (ImGui.Button(LocalizationManagerInstance.ImGui.Twitch)) Utils.OpenLink(Constants.TWITCH_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManagerInstance.ImGui.Twitter)) Utils.OpenLink(Constants.TWITTER_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManagerInstance.ImGui.ArtStation)) Utils.OpenLink(Constants.ARTSTATION_LINK);

				ImGui.Text(LocalizationManagerInstance.ImGui.DonationMessage1);
				ImGui.Text(LocalizationManagerInstance.ImGui.DonationMessage2);

				if (ImGui.Button(LocalizationManagerInstance.ImGui.Donate)) Utils.OpenLink(Constants.STREAMELEMENTS_TIP_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManagerInstance.ImGui.PayPal)) Utils.OpenLink(Constants.PAYPAL_LINK);
				ImGui.SameLine();
				if (ImGui.Button(LocalizationManagerInstance.ImGui.BuyMeATea)) Utils.OpenLink(Constants.KOFI_LINK);

				ImGui.Separator();
				ImGui.NewLine();
				ImGui.Separator();

				ConfigManagerInstance.Customization.RenderImGui();
				changed = LocalizationManagerInstance.Customization.RenderImGui() || changed;
				changed = RegionLockFixInstance.Customization.RenderImGui() || changed;
				changed = MaxSearchResultLimitInstance.Customization.RenderImGui() || changed;
				changed = SessionPlayerCountFilterInstance.Customization.RenderImGui() || changed;

				//font.Scale = oldScale;
				//ImGui.PopFont();

				ImGui.End();

				if (changed)
				{
					ConfigManagerInstance.Current.Save();
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
