using System;
using UnityEngine;

[Serializable]
public struct CircuitRestoreData
{
    [SerializeField] private ItemRestoreData[] _items;
    [SerializeField] private ToggablePortRestoreData[] _toggablePorts;
    [SerializeField] private CableRestoreData[] _cables;

    public readonly ItemRestoreData[] Items => _items;
    public readonly ToggablePortRestoreData[] ToggablePorts => _toggablePorts;
    public readonly CableRestoreData[] Cables => _cables;

    public CircuitRestoreData(
        ItemRestoreData[] items,
        ToggablePortRestoreData[] toggablePorts,
        CableRestoreData[] cables
    )
    {
        _items = items;
        _toggablePorts = toggablePorts;
        _cables = cables;
    }
}