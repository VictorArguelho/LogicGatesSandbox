using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager
{
    private readonly static List<DraggableSelectable> _selectables = new();

    public static void RegisterSelectable(DraggableSelectable selectable)
    {
        if (!_selectables.Contains(selectable))
            _selectables.Add(selectable);
    }

    public static void UnregisterSelectable(DraggableSelectable selectable) =>
        _selectables.Remove(selectable);

    public static void Initialize()
    {
        MouseManager.OnButtonDown += HandleMouseClick;
        MouseManager.OnButtonUp += HandleMouseUp;

        MultipleSelector.OnSelectionStopped += HandleSelectionStopped;
    }

    private static void HandleMouseClick(MouseButtonCode button)
    {
        if (button == MouseButtonCode.Left && ToolManager.CurrentTool == ToolCode.Drag)
            if (!ClickSelector.Click(_selectables))
                MultipleSelector.StartSelect();
    }

    private static void HandleMouseUp(MouseButtonCode button)
    {
        if (button == MouseButtonCode.Left && ToolManager.CurrentTool == ToolCode.Drag)
        {
            DeselectAll();
            MultipleSelector.StopSelect();
        }
    }

    private static void HandleSelectionStopped(Bounds selectionBounds)
    {
        foreach (var selectable in _selectables)
        {
            if (!selectable.TrySelect(selectionBounds))
                selectable.Deselect();
        }
    }

    private static void DeselectAll()
    {
        foreach (var selectable in _selectables)
            selectable.Deselect();
    }
}