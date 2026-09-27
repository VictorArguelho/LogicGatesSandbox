using UnityEngine;

[RequireComponent(typeof(Port))]
[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(PortAppearance))]
public class ToggablePort : MonoBehaviour
{
    private Selectable _selectable;
    private Port _port;

    private void Awake()
    {
        _selectable = GetComponent<Selectable>();
        _port = GetComponent<Port>();
    }

    private void Update()
    {
        if (_selectable.State == SelectionState.Selected && Input.GetKeyDown(KeyCode.E))
            _port.SetSignal(!_port.Signal);
    }
}