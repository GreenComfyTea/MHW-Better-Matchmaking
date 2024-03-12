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

		public float ComboBoxWidth { get; set; } = 100f;

		private ImFontPtr Font { get; set; }

		private bool IsFontInitialized { get; set; } = false;

		private CustomizationWindow() { }

		public CustomizationWindow Init()
		{
			InstantiateSingletons();

			return this;
		}

		private CustomizationWindow InitFont()
		{
			IsFontInitialized = true;

			//var fonts = ImGui.GetIO().Fonts;

			//fonts.AddFontDefault();

			//var mergeMode = true;

			//var config = new ImFontConfig();

			//config.OversampleH = 2;
			//config.OversampleV = 2;
			//config.GlyphExtraSpacing.X = 1f;

			//Font = fonts.AddFontFromFileTTF(Path.Combine(Constants.PLUGIN_DATA_PATH, "NotoSansKR-Bold.otf"), 26f, config, fonts.GetGlyphRangesKorean());

			//fonts.Build();

			//var loaded = Font.IsLoaded();

			//TeaLog.Info(loaded.ToString());

			return this;
		}

		public CustomizationWindow Render()
		{
			if (!IsOpened) return this;

			if(!IsFontInitialized) InitFont();

			try
			{
				var changed = false;

				//ImGui.ShowMetricsWindow();

				//ImGui.DebugTextEncoding("대법원에 대법관을");

				ImGui.SetNextWindowPos(Constants.DEFAULT_WINDOW_POSITION, ImGuiCond.FirstUseEver);
				ImGui.SetNextWindowSize(Constants.DEFAULT_WINDOW_SIZE, ImGuiCond.FirstUseEver);



				//ImGui.PushFont(Font);
				ImGui.Begin($"{Constants.MOD_NAME} v{Constants.VERSION}", ref _isOpened);

				ComboBoxWidth = Constants.COMBOBOX_WIDTH_MULTIPLIER * ImGui.GetWindowSize().X;

				//ImGui.Text("대법원에 대법관을 둔다. 대통령은 법률안의 일부에 대하여 또는 법률안을 수정하여 재의를 요구할 수 없다. 국회는 국무총리 또는 국무위원의 해임을 대통령에게 건의할 수 있다. 공무원인 근로자는 법률이 정하는 자에 한하여 단결권·단체교섭권 및 단체행동권을 가진다.\r\n\r\n대통령에 대한 탄핵소추는 국회재적의원 과반수의 발의와 국회재적의원 3분의 2 이상의 찬성이 있어야 한다. 중임할 수 없다. 사법절차가 준용되어야 한다. 국민의 모든 자유와 권리는 국가안전보장·질서유지 또는 공공복리를 위하여 필요한 경우에 한하여 법률로써 제한할 수 있으며.\r\n\r\n각급 선거관리위원회의 조직·직무범위 기타 필요한 사항은 법률로 정한다. 가부동수인 때에는 부결된 것으로 본다, 다만, 대법관은 대법원장의 제청으로 국회의 동의를 얻어 대통령이 임명한다.\r\n\r\n국가는 그 균형있는 개발과 이용을 위하여 필요한 계획을 수립한다, 대법관은 대법원장의 제청으로 국회의 동의를 얻어 대통령이 임명한다. 국회의 폐회중에도 또한 같다. 종교와 정치는 분리된다.\r\n\r\n헌법재판소의 장은 국회의 동의를 얻어 재판관중에서 대통령이 임명한다. 공무원은 국민전체에 대한 봉사자이며, 군사법원의 조직·권한 및 재판관의 자격은 법률로 정한다. 국회는 국무총리 또는 국무위원의 해임을 대통령에게 건의할 수 있다.\r\n\r\n직전대통령이 없을 때에는 대통령이 지명한다, 대법원장의 임기는 6년으로 하며. 군인·군무원·경찰공무원 기타 법률이 정하는 자가 전투·훈련등 직무집행과 관련하여 받은 손해에 대하여는 법률이 정하는 보상외에 국가 또는 공공단체에 공무원의 직무상 불법행위로 인한 배상은 청구할 수 없다, 제3항의 승인을 얻지 못한 때에는 그 처분 또는 명령은 그때부터 효력을 상실한다.\r\n\r\n1차에 한하여 중임할 수 있다. 대통령은 국무회의의 의장이 되고. 새로운 회계연도가 개시될 때까지 예산안이 의결되지 못한 때에는 정부는 국회에서 예산안이 의결될 때까지 다음의 목적을 위한 경비는 전년도 예산에 준하여 집행할 수 있다. 그 정치적 중립성은 준수된다.\r\n\r\n국가는 주택개발정책등을 통하여 모든 국민이 쾌적한 주거생활을 할 수 있도록 노력하여야 한다. 대통령은 헌법과 법률이 정하는 바에 의하여 국군을 통수한다, 제1항의 지시를 받은 당해 행정기관은 이에 응하여야 한다, 법률과 적법한 절차에 의하지 아니하고는 처벌·보안처분 또는 강제노역을 받지 아니한다.\r\n\r\n사회적 특수계급의 제도는 인정되지 아니하며, 대통령이 임시회의 집회를 요구할 때에는 기간과 집회요구의 이유를 명시하여야 한다. 국토와 자원은 국가의 보호를 받으며. 근로자는 근로조건의 향상을 위하여 자주적인 단결권·단체교섭권 및 단체행동권을 가진다.\r\n\r\n모든 국민은 근로의 의무를 진다. 대통령이 궐위된 때 또는 대통령 당선자가 사망하거나 판결 기타의 사유로 그 자격을 상실한 때에는 60일 이내에 후임자를 선거한다. 국회는 국가의 예산안을 심의·확정한다. 제5항에 의하여 법률이 확정된 후 또는 제4항에 의한 확정법률이 정부에 이송된 후 5일 이내에 대통령이 공포하지 아니할 때에는 국회의장이 이를 공포한다.");

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

				changed = ConfigManager_I.Current.Sessions.RenderImGui() || changed;
				changed = ConfigManager_I.Current.Quests.RenderImGui() || changed;

				ImGui.End();
				//ImGui.PopFont();

				if (changed)
				{
					ConfigManager_I.Current.Save();
				}

				return this;
			}
			catch (Exception e)
			{
				TeaLog.Error(e.ToString());

				ImGui.End();
				//ImGui.PopFont();

				return this;
			}
		}
	}
}
