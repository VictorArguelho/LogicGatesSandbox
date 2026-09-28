using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager
{
    private readonly static List<Selectable> _selectables = new();
    private readonly static List<Selectable> _selectedSelectables = new();

    public static bool IsMultipleSelecting { get; private set; } = false;
    public static Vector2 StartMultipleSelectingPosition { get; private set; }

    public static IReadOnlyList<Selectable> SelectedSelectables =>
        _selectedSelectables;

    public static void RegisterSelectable(Selectable selectable)
    {
        if (!_selectables.Contains(selectable))
            _selectables.Add(selectable);
    }

    public static void UnregisterSelectable(Selectable selectable)
    {
        _selectables.Remove(selectable);
        _selectedSelectables.Remove(selectable);
    }

    public static void Initialize()
    {
        MouseManager.OnButtonDown += HandleMouseClick;
        MouseManager.OnButtonUp += HandleMouseUp;

        ToolManager.OnToolChange += (tool) =>
        {
            if (tool != ToolCode.Selection)
                DeselectAll();
        };
    }

    private static void HandleMouseClick(MouseButtonCode button)
    {
        if (MouseManager.IsPointerOverUI)
            return;

        if (button != MouseButtonCode.Left)
            return;

        if (ToolManager.CurrentTool != ToolCode.Selection)
            return;

        DeselectAll();

        if (MouseIsOverAnySelectable(out var selectable))
        {
            selectable.Select();
            _selectedSelectables.Add(selectable);
            return;
        }

        IsMultipleSelecting = true;
        StartMultipleSelectingPosition = MouseManager.MouseWorldPosition;
    }

    private static void HandleMouseUp(MouseButtonCode button)
    {
        if (button != MouseButtonCode.Left)
            return;

        if (ToolManager.CurrentTool != ToolCode.Selection)
            return;

        if (!IsMultipleSelecting)
            return;

        IsMultipleSelecting = false;
        var selectablesInSelectionBounds = GetSelectablesInBounds(GetMultipleSelectionBounds());
        foreach (var selectable in selectablesInSelectionBounds)
        {
            selectable.Select();
            _selectedSelectables.Add(selectable);
        }
    }

    private static Bounds GetMultipleSelectionBounds()
    {
        Vector2 currentMousePosition = MouseManager.MouseWorldPosition;

        var min = Vector2.Min(StartMultipleSelectingPosition, currentMousePosition);
        var max = Vector2.Max(StartMultipleSelectingPosition, currentMousePosition);

        return new(
            (min + max) / 2f,
            max - min
        );
    }

    private static void DeselectAll()
    {
        foreach (var selectable in _selectedSelectables)
            selectable.Deselect();

        _selectedSelectables.Clear();
    }

    private static bool MouseIsOverAnySelectable(out Selectable selectable)
    {
        selectable = null;

        foreach (var current in _selectables)
        {
            if (!current.IsMouseOver)
                continue;

            if (selectable == null ||
                current.Priority > selectable.Priority)
            {
                selectable = current;
            }
        }

        return selectable != null;
    }

    private static List<Selectable> GetSelectablesInBounds(Bounds bounds)
    {
        var selectables = new List<Selectable>();

        foreach (var selectable in _selectables)
        {
            if (selectable.IntersectsBounds(bounds))
                selectables.Add(selectable);
        }

        return selectables;
    }
}