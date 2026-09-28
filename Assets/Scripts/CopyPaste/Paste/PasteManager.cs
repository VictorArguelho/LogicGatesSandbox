using System.Collections.Generic;
using UnityEngine;

public class PasteManager : MonoBehaviour
{
    private readonly Dictionary<uint, GameObject> _idMap = new();

    private IReadOnlyList<ClipboardItemData> _itemsData;
    private IReadOnlyList<PortData> _portsData;
    private IReadOnlyList<ClipboardToggablePortData> _toggablePortsData;
    private IReadOnlyList<ClipboardPortWireData> _portWiresData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            Paste();
    }

    private void Paste()
    {
        _idMap.Clear();

        _itemsData = ClipboardManager.ClipboardItemsData;
        _portsData = ClipboardManager.ClipboardPortsData;
        _toggablePortsData = ClipboardManager.ClipboardToggablePortsData;
        _portWiresData = ClipboardManager.ClipboardPortWiresData;

        PasteItems();
        PasteToggablePorts();
        PastePortWires();
    }

    private void PasteItems()
    {
        foreach (var item in _itemsData)
            PasteItem(item);
    }

    private void PasteToggablePorts()
    {
        foreach (var port in _toggablePortsData)
        {
            var portObject = PasteItem(port.ItemData);
            portObject.GetComponent<Port>().SetSignal(port.Signal);
        }
    }

    private void PastePortWires()
    {
        foreach (var wire in _portWiresData)
        {
            var outPort = GetWirePort(wire.OutPortData);
            var inPort = GetWirePort(wire.InPortData);

            if (outPort != null && inPort != null)
                PortConnectorManager.ResolveConnection(outPort, inPort);
        }
    }

    private GameObject PasteItem(ClipboardItemData item)
    {
        var itemObject = ItemSpawner.Instance.SpawnObject(
                item.ItemCode,
                item.CategoryCode
            );

        if (itemObject.TryGetComponent<GridDraggable>(out var draggable))
            draggable.SetPosition(MouseManager.GriddedMouseWorldPosition - item.DistanceAtMouse);
        else
            itemObject.transform.position = MouseManager.GriddedMouseWorldPosition;

        _idMap.Add(item.Id, itemObject);

        return itemObject;
    }

    private Port GetWirePort(PortData data)
    {
        if (!_idMap.TryGetValue(data.Id, out var itemObject))
            return null;

        if (!data.IsFromGate)
            return itemObject.GetComponent<Port>();

        if (itemObject.TryGetComponent<DefaultGate>(out var gate))
            return gate.GetPortByCode(data.GateCode);

        return null;
    }
}