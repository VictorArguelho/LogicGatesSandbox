using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager
{
    private readonly static List<SelectableTarget> _selectables = new();

    public static void RegisterSelectable(SelectableTarget selectable)
    {
        if (!_selectables.Contains(selectable))
            _selectables.Add(selectable);
    }

    public static void UnregisterSelectable(SelectableTarget selectable) =>
        _selectables.Remove(selectable);

    public static void Initialize()
    {
        MouseManager.OnButtonDown += HandleMouseClick;
        MouseManager.OnButtonUp += HandleMouseUp;

        MultipleSelector.OnSelectionStopped += HandleSelectionStopped;
    }

    private static void HandleMouseClick(MouseButtonCode button)
    {
        if (button == MouseButtonCode.Left)
            if (!ClickSelector.Click(_selectables))
                MultipleSelector.StartSelect();
    }

    private static void HandleMouseUp(MouseButtonCode button)
    {
        if (button == MouseButtonCode.Left)
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