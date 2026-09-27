using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }
    private readonly static List<Draggable> _draggables = new();

    public static void RegisterDraggable(Draggable draggable)
    {
        if (!_draggables.Contains(draggable))
            _draggables.Add(draggable);
    }

    public static void UnregisterDraggable(Draggable draggable) =>
        _draggables.Remove(draggable);

    private void Awake() =>
        Instance = this;

    private void Update()
    {
        if (MouseManager.RightButtonPressed)
        {
            foreach (var draggable in _draggables)
            {
                if (!draggable.IsSelected)
                    continue;

                draggable.StartDragging();
                draggable.ApplyMove(MouseManager.MouseWorldPositionDelta);
            }
        }
    }
}