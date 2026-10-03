using UnityEngine;

[RequireComponent(typeof(Port))]
[RequireComponent(typeof(Selectable))]
public class ToggablePort : MonoBehaviour, IRestorable<ToggablePortRestoreData>
{
    private Selectable _selectable;
    private Port _port;

    public bool Signal => _port.Signal;

    private void Awake()
    {
        _selectable = GetComponent<Selectable>();
        _port = GetComponent<Port>();
        InputManager.Instance.OnActivateItem += HandleActivate;
    }

    public void SetSignal(bool signal) =>
        _port.SetSignal(signal);

    public ToggablePortRestoreData GetRestoreData()
    {
        if (TryGetComponent<SpawnedItem>(out var spawnedComponent))
            return new(spawnedComponent.Id, Signal);
        return ToggablePortRestoreData.Invalid;
    }

    private void HandleActivate()
    {
        if (_selectable.State == SelectionState.Selected)
            SetSignal(!_port.Signal);
    }
}