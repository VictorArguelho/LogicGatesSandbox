using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
public class Draggable : MonoBehaviour
{
    private MouseCollider _mouseCollider;
    private bool _isDragging;

    private void Awake() =>
        _mouseCollider = GetComponent<MouseCollider>();

    private void Update()
    {
        RefreshIsDragging();
        if (_isDragging)
            transform.position += (Vector3)MouseManager.MouseWorldPositionDelta;
    }

    private void RefreshIsDragging()
    {
        if (MouseManager.LeftButtonDown && _mouseCollider.IsColliding)
            _isDragging = true;

        if (MouseManager.LeftButtonUp)
            _isDragging = false;
    }
}