using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal sealed class FontCustomization : SingletonAccessor
{
	private float _size = 17f;
	public float Size { get => _size; set => _size = value; }

	private int _horizontalOversample = 2;
	public int HorizontalOversample { get => _horizontalOversample; set => _horizontalOversample = value; }

	private int _verticalOversample = 2;
	public int VerticalOversample { get => _verticalOversample; set => _verticalOversample = value; }



	public FontCustomization()
	{
		InstantiateSingletons();
	}

	public bool RenderImGui()
	{
		var changed = false;

		if(ImGui.TreeNode(LocalizationManager_I.ImGui.Font))
		{

			ImGui.Text(LocalizationManager_I.ImGui.AnyChangesToFontRequireGameRestart);

			changed = ImGui.DragFloat(LocalizationManager_I.ImGui.Size, ref _size, Constants.DRAG_FLOAT_SPEED, 1f, 128f, "%.1f") || changed;
			changed = ImGui.SliderInt(LocalizationManager_I.ImGui.HorizontalOversample, ref _horizontalOversample, 0, 8) || changed;
			changed = ImGui.SliderInt(LocalizationManager_I.ImGui.VerticalOversample, ref _verticalOversample, 0, 8) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}
