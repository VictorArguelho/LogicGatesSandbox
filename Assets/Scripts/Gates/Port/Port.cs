using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseCollider))]
public class Port : MonoBehaviour
{
    private MouseCollider _collider;
    
    public bool Signal { get; private set; }

    public Port ConnectedPortAtIn { get; private set; }
    public List<Port> ConnectedPortsAtOut { get; } = new();

    public bool IsConnectedIn => ConnectedPortAtIn != null;
    public bool IsConnectedOut => ConnectedPortsAtOut.Count > 0;

    public bool IsMouseOver =>
        _collider.IsColliding;

    private void Awake()
    {
        _collider = GetComponent<MouseCollider>();
        PortConnectorManager.RegisterPort(this);
    }

    private void OnDestroy()
    {
        PortConnectorManager.UnregisterPort(this);

        if (IsConnectedIn)
            ConnectedPortAtIn.TryDisconnectAtOut(this);

        foreach (var port in ConnectedPortsAtOut)
            port.TryDisconnectAtIn();

        ConnectedPortsAtOut.Clear();
    }

    public void SetSignal(bool signal)
    {
        if (Signal == signal)
            return;

        Signal = signal;

        foreach (var port in ConnectedPortsAtOut)
            port.SetSignal(signal);
    }

    public bool TryConnectAtOut(Port port)
    {
        if (ConnectedPortsAtOut.Contains(port))
            return false;

        if (!port.TryConnectAtIn(this))
            return false;

        ConnectedPortsAtOut.Add(port);
        return true;
    }  

    public bool TryDisconnectAtOut(Port port)
    {
        if (!ConnectedPortsAtOut.Contains(port))
            return false;

        if (port.ConnectedPortAtIn != this)
            return false;

        if (!port.TryDisconnectAtIn())
            return false;

        ConnectedPortsAtOut.Remove(port);
        return true;
    }

    private bool TryConnectAtIn(Port port)
    {
        if (IsConnectedIn)
            return false;

        ConnectedPortAtIn = port;
        return true;
    }


    private bool TryDisconnectAtIn()
    {
        if (!IsConnectedIn)
            return false;

        ConnectedPortAtIn = null;
        return true;
    }
}