using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
public class Port : MonoBehaviour
{
    private MouseCollider _collider;
    
    public bool Signal { get; private set; }
    public List<Port> ConnectedPortsOut { get; } = new();
    public Port ConnectedPortIn { get; private set; }
    public bool IsConnectedOut => ConnectedPortsOut.Count > 0;
    public bool IsConnectedIn => ConnectedPortIn != null;

    public bool IsMouseOver =>
        _collider.IsColliding;

    private void Awake()
    {
        _collider = GetComponent<MouseCollider>();
        PortConnectorManager.RegisterPort(this);
    }

    private void OnDestroy() =>
        PortConnectorManager.UnregisterPort(this);

    public void ConnectIn(Port other) =>
        ConnectedPortIn = other;
    public void DisconnectIn() =>
    ConnectedPortIn = null;

    public void ConnectOut(Port other) =>
        ConnectedPortsOut.Add(other);
    public void DisconnectOut(Port port) =>
        ConnectedPortsOut.Remove(port);

    public void SetSignal(bool signal)
    {
        if (Signal == signal)
            return;

        Signal = signal;

        ConnectedPortsOut.RemoveAll(item => item == null);
        foreach (var port in ConnectedPortsOut)
            port.SetSignal(signal);
    }
}