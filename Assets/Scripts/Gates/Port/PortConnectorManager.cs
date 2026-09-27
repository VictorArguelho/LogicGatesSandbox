using System.Collections.Generic;
using UnityEngine;

public class PortConnectorManager : MonoBehaviour
{
    private readonly static List<Port> _ports = new();

    public static void RegisterPort(Port port) =>
        _ports.Add(port);

    public static void UnregisterPort(Port port) =>
        _ports.Remove(port);

    private Wire _previewWire;

    private bool _isConnecting;
    private Port _outPort;

    private void Awake()
    {
        MouseManager.OnButtonDown += MouseDownHandle;
        MouseManager.OnButtonUp += MouseUpHandle;

        _previewWire = WireFactory.CreateWire(Vector2.zero, Vector2.zero);
        _previewWire.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isConnecting)
            _previewWire.SetPoints(
                _outPort.transform.position,
                MouseManager.MouseWorldPosition
            );
    }

    private void OnDestroy()
    {
        MouseManager.OnButtonDown -= MouseDownHandle;
        MouseManager.OnButtonUp -= MouseUpHandle;
    }

    private void MouseDownHandle(MouseButtonCode button)
    {
        if (button != MouseButtonCode.Left)
            return;

        if (ToolManager.CurrentTool != ToolCode.Wire)
            return;

        if (_isConnecting)
            return;

        foreach (var port in _ports)
        {
            if (port.IsMouseOver)
            {
                StartConnection(port);
                break;
            }
        }
    }

    private void MouseUpHandle(MouseButtonCode button)
    {
        if (button != MouseButtonCode.Left)
            return;

        if (ToolManager.CurrentTool != ToolCode.Wire)
            return;

        if (!_isConnecting)
            return;

        foreach (var port in _ports)
        {
            if (port.IsMouseOver && port != _outPort && !port.IsConnectedIn)
            {
                StopConnection(port);
                return;
            }
        }

        StopConnection();
    }

    private void StartConnection(Port port)
    {
        _isConnecting = true;
        _outPort = port;
        _previewWire.gameObject.SetActive(true);
    }

    private void StopConnection(Port port)
    {
        ResolveConnection(_outPort, port);
        StopConnection();
    }

    private void StopConnection()
    {
        _isConnecting = false;
        _outPort = null;
        _previewWire.gameObject.SetActive(false);
    }

    private void ResolveConnection(Port outPort, Port inPort)
    {
        if (outPort.TryConnectAtOut(inPort))
            PortWireFactory.CreateWire(outPort, inPort);
    }
}