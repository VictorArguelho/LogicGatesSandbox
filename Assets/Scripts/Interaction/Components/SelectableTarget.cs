using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
[RequireComponent(typeof(SelectionController))]
public class SelectableTarget : MonoBehaviour
{
    private MouseCollider _mouseCollider;
    private SelectionController _selectionController;

    private void Awake()
    {
        _mouseCollider = GetComponent<MouseCollider>();
        _selectionController = GetComponent<SelectionController>();

        SelectionManager.RegisterSelectable(this);
    }

    private void Update()
    {
        if (_mouseCollider.IsColliding)
            _selectionController.StartSemiSelect();
        else
            _selectionController.StopSemiSelect();
    }

    private void OnDestroy() =>
        SelectionManager.UnregisterSelectable(this);

    public bool TrySelect()
    {
        if (!_mouseCollider.IsColliding)
            return false;

        _selectionController.Select();
        return true;
    }

    public bool TrySelect(Bounds bounds)
    {
        if (!_mouseCollider.Intersects(bounds))
            return false;

        _selectionController.Select();
        return true;
    }

    public void Deselect()
    {
        _selectionController.Deselect();
    }
}