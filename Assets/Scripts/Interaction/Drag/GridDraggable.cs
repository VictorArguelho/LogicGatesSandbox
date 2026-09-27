using UnityEngine;

public class GridDraggable : Draggable
{
    [SerializeField] private float _gridSize = 1f;
    [SerializeField] private Vector2 _gridOffset = new(0.5f, 0.5f);
    [SerializeField] private float _smoothSpeed = 15f;

    private Vector2 _realPosition;
    private Vector2 _griddedPosition;

    protected override void Awake()
    {
        base.Awake();

        _realPosition = transform.position;
        _griddedPosition = transform.position;
        UpdateGriddedPosition();
    }

    private void Update() =>
        UpdatePosition();

    public override void StartDragging()
    {
        if (IsDragging)
            return;

        base.StartDragging();

        _realPosition = _griddedPosition;
    }   

    public override void ApplyMove(Vector2 move)
    {
        _realPosition += move;
        UpdateGriddedPosition();
    }

    private void UpdateGriddedPosition()
    {
        _griddedPosition = new Vector2(
            Mathf.Round(_realPosition.x / _gridSize) * _gridSize,
            Mathf.Round(_realPosition.y / _gridSize) * _gridSize
        ) + _gridOffset;
    }

    private void UpdatePosition()
    {
        transform.position = Vector2.Lerp(
            transform.position,
            _griddedPosition,
            _smoothSpeed * Time.deltaTime
        );
    }
}