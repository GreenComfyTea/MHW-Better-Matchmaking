using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class DebugCustomization : SingletonAccessor
{
	private int _historySize = 32;
	public int HistorySize { get => _historySize; set => _historySize = value; }

	public DebugCustomization()
	{
		InstantiateSingletons();
	}

	public DebugCustomization Init()
	{
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.Errors))
		{
			ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, $"{LocalizationManager_I.ImGui.CurrentTime}:");
			ImGui.SameLine();
			ImGui.Text(DateTime.Now.ToString("HH:mm:ss.fffffff"));

			if(DebugManager_I.Reports.Count == 0)
			{
				ImGui.Text(LocalizationManager_I.ImGui.EverythingSeemsToBeOk);
			}
			else
			{
				foreach(var report in DebugManager_I.Reports)
				{
					ImGui.Button(report.Value.Timestamp.ToString("HH:mm:ss.fffffff"));
					ImGui.SameLine();
					ImGui.TextColored(Constants.IMGUI_RED_COLOR, report.Value.Message);
				}
			}

			if(ImGui.TreeNode(LocalizationManager_I.ImGui.History))
			{
				changed = ImGui.DragInt(LocalizationManager_I.ImGui.HistorySize, ref _historySize, 1, 0, 999) || changed;

				if(changed) DebugManager_I.TrimHistory();

				var i = 1;
				foreach(var report in DebugManager_I.History)
				{
					ImGui.AlignTextToFramePadding();
					ImGui.TextColored(Constants.IMGUI_LIGHT_GREEN_COLOR, i.ToString());
					ImGui.SameLine();
					ImGui.Button(report.Timestamp.ToString("HH:mm:ss.fffffff"));
					ImGui.SameLine();
					ImGui.TextColored(Constants.IMGUI_RED_COLOR, report.Message);

					i++;
				}

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return changed;
	}
}
