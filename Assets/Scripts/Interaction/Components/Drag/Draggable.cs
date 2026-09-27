using UnityEngine;

public class Draggable : MonoBehaviour, IDraggable
{
    public bool IsDragging { get; private set; }

    public void StartDragging() =>
    IsDragging = true;

    public void StopDragging() =>
        IsDragging = false;

    private void Update()
    {
        if (IsDragging)
            transform.position += (Vector3)MouseManager.MouseWorldPositionDelta;
    }
}