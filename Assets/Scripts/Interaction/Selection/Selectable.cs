using System;
using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
public class Selectable : MonoBehaviour
{
    [SerializeField] private int _priority;

    private MouseCollider _collider;

    public int Priority => _priority;
    public bool IsMouseOver => _collider.IsColliding;
    public SelectionState State { get; private set; } = SelectionState.Default;

    public event Action OnSelected;
    public event Action OnDeselected;

    private void Awake()
    {
        _collider = GetComponent<MouseCollider>();

        _collider.OnMouseEnter += HandleMouseEnter;
        _collider.OnMouseExit += HandleMouseExit;

        SelectionManager.RegisterSelectable(this);
    }

    private void OnDestroy()
    {
        if (_collider != null)
        {
            _collider.OnMouseEnter -= HandleMouseEnter;
            _collider.OnMouseExit -= HandleMouseExit;
        }

        SelectionManager.UnregisterSelectable(this);
    }

    private void HandleMouseEnter()
    {
        if (State == SelectionState.Selected)
            return;

        if (MouseManager.IsPointerOverUI)
            return;

        State = SelectionState.MouseOver;
    }

    private void HandleMouseExit()
    {
        if (State != SelectionState.MouseOver)
            return;

        State = SelectionState.Default;
    }

    public bool IntersectsBounds(Bounds bounds) =>
        _collider.Intersects(bounds);

    public void Select()
    {
        OnSelected?.Invoke();
        State = SelectionState.Selected;
    }

    public void Deselect()
    {
        OnDeselected?.Invoke();
        State = SelectionState.Default;
    }
}