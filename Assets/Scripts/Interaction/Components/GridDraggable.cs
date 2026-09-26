using UnityEngine;

public class GridDraggable : MonoBehaviour, IDraggable
{
    [SerializeField] private float _gridSize = 1f;
    [SerializeField] private Vector2 _gridOffset = new(0.5f, 0.5f);
    [SerializeField] private float _smoothSpeed = 15f;

    public bool IsDragging { get; private set; }

    private Vector2 _realPosition;
    private Vector2 _targetPosition;

    private void Awake()
    {
        _realPosition = transform.position;
        _targetPosition = transform.position;
    }

    private void Update()
    {
        RefreshTargetPosition();
        UpdateTargetPosition();
        UpdatePosition();
    }
    public void StartDragging() =>
    IsDragging = true;

    public void StopDragging() =>
        IsDragging = false;

    private void RefreshTargetPosition()
    {
        if (IsDragging)
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