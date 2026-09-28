using UnityEngine;

[RequireComponent(typeof(Port))]
[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(PortAppearance))]
public class ToggablePort : MonoBehaviour
{
    private Selectable _selectable;
    private Port _port;

    public bool Signal => _port.Signal;

    private void Awake()
    {
        _selectable = GetComponent<Selectable>();
        _port = GetComponent<Port>();
    }

    private void Update()
    {
        if (_selectable.State == SelectionState.Selected && Input.GetKeyDown(KeyCode.E))
            SetSignal(!_port.Signal);
    }

    public void SetSignal(bool signal) =>
        _port.SetSignal(signal);
}