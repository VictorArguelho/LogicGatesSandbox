using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class SelectionController : MonoBehaviour
{
    private Selectable _selectable;
    private IDraggable _draggable;

    private void Awake()
    {
        _selectable = GetComponent<Selectable>();
        _draggable = GetComponent<IDraggable>();
    }

    public void StartSemiSelect()
    {
        if (_selectable.State == SelectionState.Default)
            _selectable.SetState(SelectionState.SemiSelected);
    }

    public void StopSemiSelect()
    {
        if (_selectable.State == SelectionState.SemiSelected)
            _selectable.SetState(SelectionState.Default);
    }

    public void Select()
    {
        if (_selectable.State == SelectionState.Selected)
            return;

        _draggable.StartDragging();
        _selectable.SetState(SelectionState.Selected);
    }

    public void Deselect()
    {
        if (_selectable.State != SelectionState.Selected)
            return;

        _draggable.StopDragging();
        _selectable.SetState(SelectionState.Default);
    }
}