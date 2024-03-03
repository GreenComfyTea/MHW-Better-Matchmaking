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

		if(ImGui.TreeNode(LocalizationManagerInstance.ImGui.Errors))
		{
			ImGui.TextColored(Constants.IMGUI_BLUE_COLOR, $"{LocalizationManagerInstance.ImGui.CurrentTime}:");
			ImGui.SameLine();
			ImGui.Text(DateTime.Now.ToString("HH:mm:ss.fffffff"));
			
			if(DebugManagerInstance.Reports.Count == 0)
			{
				ImGui.Text(LocalizationManagerInstance.ImGui.EverythingSeemsToBeOk);
			}
			else
			{
				foreach(var report in DebugManagerInstance.Reports)
				{
					ImGui.Button(report.Value.Timestamp.ToString("HH:mm:ss.fffffff"));
					ImGui.SameLine();
					ImGui.TextColored(Constants.IMGUI_RED_COLOR, report.Value.Message);
				}
			}

			if(ImGui.TreeNode(LocalizationManagerInstance.ImGui.History))
			{
				changed = ImGui.DragInt(LocalizationManagerInstance.ImGui.HistorySize, ref _historySize, 1, 0, 999) || changed;

				if(changed) DebugManagerInstance.TrimHistory();

				var i = 1;
				foreach(var report in DebugManagerInstance.History)
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
