using System;
using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
public class Selectable : MonoBehaviour
{
    private MouseCollider _collider;

    public bool IsMouseOver => _collider.IsColliding;
    public SelectionState State { get; private set; } = SelectionState.Default;

    public event Action OnSelected;
    public event Action OnDeselected;

    private void Awake()
    {
        _collider = GetComponent<MouseCollider>();

        SelectionManager.RegisterSelectable(this);
    }

    private void Update()
    {
        if (State == SelectionState.Selected)
            return;

        if (MouseManager.IsPointerOverUI)
            return;

        if (IsMouseOver && State == SelectionState.Default)
            State = SelectionState.MouseOver;

        if (!IsMouseOver && State == SelectionState.MouseOver)
            State = SelectionState.Default;
    }

    private void OnDestroy() =>
        SelectionManager.UnregisterSelectable(this);

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