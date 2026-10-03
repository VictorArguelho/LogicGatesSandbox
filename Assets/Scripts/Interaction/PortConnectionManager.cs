using System.Collections.Generic;
using UnityEngine;

public class PortConnectionManager : Singleton<PortConnectionManager>
{
    private readonly List<Port> _ports = new();

    [SerializeField] private Wire _previewWire;

    private bool _isConnecting;
    private Port _outPort;

    protected override void Awake()
    {
        base.Awake();

        MouseManager.OnButtonDown += MouseDownHandle;
        MouseManager.OnButtonUp += MouseUpHandle;

        _previewWire.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isConnecting)
        {
            var offset = new Vector2(0.5f, -0.5f);
            _previewWire.SetPoints(
                (Vector2)_outPort.transform.position + offset * _outPort.transform.localScale,
                MouseManager.MouseWorldPosition
            );
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        MouseManager.OnButtonDown -= MouseDownHandle;
        MouseManager.OnButtonUp -= MouseUpHandle;
    }

    public void RegisterPort(Port port) =>
        _ports.Add(port);

    public void UnregisterPort(Port port) =>
        _ports.Remove(port);

    private void MouseDownHandle(MouseButtonCode button)
    {
        if (button != MouseButtonCode.Left)
            return;

        if (ToolManager.Instance.CurrentTool != ToolCode.Cable)
            return;

        if (_isConnecting)
            return;

        foreach (var port in _ports)
        {
            if (port.IsMouseOver)
            {
                StartConnection(port);
                return;
            }
        }
    }

    private void MouseUpHandle(MouseButtonCode button)
    {
        if (button != MouseButtonCode.Left)
            return;

        if (ToolManager.Instance.CurrentTool != ToolCode.Cable)
            return;

        if (!_isConnecting)
            return;

        foreach (var port in _ports)
        {
            if (port.IsMouseOver && port != _outPort && !port.IsConnectedIn && port.CanBeConnectedIn)
            {
                ResolveConnection(port);
                return;
            }
        }

        StopConnection();
    }

    private void ResolveConnection(Port inPort)
    {
        CircuitElementSpawner.TrySpawnCable(_outPort, inPort);
        StopConnection();
    }

    private void StartConnection(Port port)
    {
        _isConnecting = true;
        _outPort = port;
        _previewWire.gameObject.SetActive(true);
    }

    private void StopConnection()
    {
        _isConnecting = false;
        _outPort = null;
        _previewWire.gameObject.SetActive(false);
    }
}