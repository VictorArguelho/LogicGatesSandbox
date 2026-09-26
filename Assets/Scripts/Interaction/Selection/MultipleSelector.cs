using System;
using UnityEngine;

public static class MultipleSelector
{
    public static bool IsSelecting { get; private set; }
    public static Vector2 StartSelectionPoint { get; private set; }

    public static event Action<Bounds> OnSelectionStopped;

    public static void StartSelect()
    {
        if (IsSelecting)
            return;

        IsSelecting = true;
        StartSelectionPoint = MouseManager.MouseWorldPosition;
    } 

    public static void StopSelect()
    {
        if (!IsSelecting)
            return;

        IsSelecting = false;

        OnSelectionStopped?.Invoke(GetSelectionBounds());
    }

    private static Bounds GetSelectionBounds()
    {
        Vector2 currentMousePosition = MouseManager.MouseWorldPosition;

        var min = Vector2.Min(StartSelectionPoint, currentMousePosition);
        var max = Vector2.Max(StartSelectionPoint, currentMousePosition);

        return new(
            (min + max) / 2f,
            max - min
        );
    }
}