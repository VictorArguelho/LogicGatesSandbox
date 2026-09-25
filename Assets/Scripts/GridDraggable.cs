using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
public class GridDraggable : MonoBehaviour
{
    [SerializeField] private float _gridSize = 1f;
    [SerializeField] private Vector2 _gridOffset = new(0.5f, 0.5f);
    [SerializeField] private float _smoothSpeed = 15f;

    private MouseCollider _mouseCollider;

    private bool _isDragging;

    private Vector2 _realPosition;
    private Vector2 _targetPosition;

    private void Awake()
    {
        _mouseCollider = GetComponent<MouseCollider>();

        _realPosition = transform.position;
        _targetPosition = transform.position;
    }

    private void Update()
    {
        RefreshIsDragging();
        RefreshTargetPosition();
        UpdateTargetPosition();
        UpdatePosition();
    }

    private void RefreshIsDragging()
    {
        if (MouseManager.LeftButtonDown &&
            _mouseCollider.IsColliding)
        {
            _isDragging = true;
        }

        if (MouseManager.LeftButtonUp)
            _isDragging = false;
    }

    private void RefreshTargetPosition()
    {
        if (_isDragging)
            _realPosition += MouseManager.MouseWorldPositionDelta;
    }

    private void UpdateTargetPosition()
    {
        _targetPosition = new Vector2(
            Mathf.Round(_realPosition.x / _gridSize) * _gridSize,
            Mathf.Round(_realPosition.y / _gridSize) * _gridSize
        ) + _gridOffset;
    }

    private void UpdatePosition()
    {
        transform.position = Vector2.Lerp(
            transform.position,
            _targetPosition,
            _smoothSpeed * Time.deltaTime
        );
    }
}