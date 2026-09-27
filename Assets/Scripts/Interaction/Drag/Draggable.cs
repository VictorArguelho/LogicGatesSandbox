using UnityEngine;

[RequireComponent(typeof(Selectable))]
public abstract class Draggable : MonoBehaviour
{
    private Selectable _selectable;

    public bool IsSelected => _selectable.State == SelectionState.Selected;
    public bool IsDragging { get; protected set; }

    protected virtual void Awake()
    {
        _selectable = GetComponent<Selectable>();
        DragManager.RegisterDraggable(this);

        _selectable.OnDeselected += HandleDeselected;
    }

    private void HandleDeselected()
    {
        if (IsDragging)
            StopDragging();
    }

    protected virtual void OnDestroy()
    {
        DragManager.UnregisterDraggable(this);
        _selectable.OnDeselected -= HandleDeselected;
    }

    public virtual void StartDragging()
    {
        if (IsDragging)
            return;

        IsDragging = true;
    }

    public virtual void StopDragging()
    {
        if (!IsDragging)
            return;

        IsDragging = false;
    }
        

    public abstract void ApplyMove(Vector2 moveAmount);
}