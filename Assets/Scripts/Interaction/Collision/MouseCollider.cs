using System;
using UnityEngine;

public class MouseCollider : MonoBehaviour
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private Vector2 _offset;

    private bool _lastFrameIsColliding;

    public event Action OnMouseEnter;
    public event Action OnMouseExit;
    public event Action OnMouseStay;

    public Vector2 Size
    {
        get => _size;
        set => _size = value;
    }

    public Vector2 Offset
    {
        get => _offset;
        set => _offset = value;
    }

    public Bounds Bounds
    {
        get
        {
            var scale = transform.lossyScale;
            var size = _size * scale;
            var position = (Vector2)transform.position + _offset * size;

            return new Bounds(position, size);
        }
    }

    public bool IsColliding
    {
        get
        {
            var localMousePosition =
                transform.InverseTransformPoint(MouseManager.MouseWorldPosition);

            var center = _offset * _size;
            var halfSize = _size * 0.5f;
            var delta = (Vector2)localMousePosition - center;

            return Mathf.Abs(delta.x) <= halfSize.x &&
                   Mathf.Abs(delta.y) <= halfSize.y;
        }
    }

    private void Awake() =>
        MouseCollisionManager.Instance.Register(this);

    private void OnDestroy()
    {
        if (MouseCollisionManager.Instance != null)
            MouseCollisionManager.Instance.Unregister(this);
    }

    public void UpdateMouseState()
    {
        var isColliding = IsColliding;

        if (isColliding)
        {
            if (!_lastFrameIsColliding)
            {
                _lastFrameIsColliding = true;
                OnMouseEnter?.Invoke();
            }

            OnMouseStay?.Invoke();
            return;
        }

        if (!_lastFrameIsColliding)
            return;

        _lastFrameIsColliding = false;
        OnMouseExit?.Invoke();
    }

    public bool Intersects(Bounds otherBounds) =>
        Bounds.Intersects(otherBounds);
}