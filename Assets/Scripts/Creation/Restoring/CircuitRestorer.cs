using System.Collections.Generic;
using UnityEngine;

public static class CircuitRestorer
{
    private static readonly Dictionary<uint, GameObject> _idMap = new();
    private static CircuitRestoreData _restoreData;
    private static Vector2 _position;

    public static void Restore(CircuitRestoreData data, Vector2 position)
    {
        _idMap.Clear();
        _restoreData = data;
        _position = position;

        RestoreItems();
        RestoreToggablePorts();
        RestoreCables();
    }

    private static void RestoreItems()
    {
        foreach (var itemData in _restoreData.Items)
            TryRestoreItem(itemData);
    }

    private static void RestoreToggablePorts()
    {
        foreach (var portData in _restoreData.ToggablePorts)
        {
            if (!_idMap.TryGetValue(portData.Id, out var portObject))
                continue;

            if (portObject == null)
                continue;

            if (portObject.TryGetComponent<Port>(out var portComponent))
                portComponent.SetSignal(portData.Signal);
        }
    }

    private static void RestoreCables()
    {
        foreach (var cableData in _restoreData.Cables)
        {
            var outPort = TryGetCablePort(cableData.OutPortData);
            var inPort = TryGetCablePort(cableData.InPortData);

            if (outPort != null && inPort != null)
                CircuitElementSpawner.TrySpawnCable(outPort, inPort);
        }
    }

    private static GameObject TryRestoreItem(ItemRestoreData itemData)
    {
        var item = CircuitElementSpawner.TrySpawnItem(itemData.ItemData, itemData.RelativePosition + _position);

        if (item == null)
            return null;

        _idMap.Add(itemData.Id, item.gameObject);

        return item.gameObject;
    }

    private static Port TryGetCablePort(PortRestoreData portData)
    {
        if (!_idMap.TryGetValue(portData.Id, out var itemObject))
            return null;

        if (!portData.IsFromItem)
            return itemObject.GetComponent<Port>();

        if (itemObject.TryGetComponent<IHasPorts>(out var item))
            return item.GetPort(portData.ItemPortData);

        return null;
    }
}