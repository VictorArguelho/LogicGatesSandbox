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

    public Bounds Bounds
    {
        get
        {
            var position = (Vector2)transform.position + _offset * _size * transform.lossyScale;
            return new(position, _size * transform.lossyScale);
        }
    }

    public bool IsColliding => Bounds.Contains(MouseManager.MouseWorldPosition);

    private void Update()
    {
        bool isColliding = IsColliding;

        if (isColliding && !_lastFrameIsColliding)
        {
            _lastFrameIsColliding = true;
            OnMouseEnter?.Invoke();
        }

        if (!isColliding && _lastFrameIsColliding)
        {
            _lastFrameIsColliding = false;
            OnMouseExit?.Invoke();
        }

        if (isColliding)
            OnMouseStay?.Invoke();
    }
}