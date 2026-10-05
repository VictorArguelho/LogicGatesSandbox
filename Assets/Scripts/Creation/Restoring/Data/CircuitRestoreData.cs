using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct CircuitRestoreData : IEquatable<CircuitRestoreData>
{
    [SerializeField] private ItemRestoreData[] _items;
    [SerializeField] private ToggablePortRestoreData[] _toggablePorts;
    [SerializeField] private CableRestoreData[] _cables;

    public readonly ItemRestoreData[] Items => _items;
    public readonly ToggablePortRestoreData[] ToggablePorts => _toggablePorts;
    public readonly CableRestoreData[] Cables => _cables;

    public static CircuitRestoreData Invalid =>
        new(
            Array.Empty<ItemRestoreData>(),
            Array.Empty<ToggablePortRestoreData>(),
            Array.Empty<CableRestoreData>()
        );

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

    public readonly bool Equals(CircuitRestoreData other) =>
        _items.SequenceEqual(other._items) &&
        _toggablePorts.SequenceEqual(other._toggablePorts) &&
        _cables.SequenceEqual(other._cables);

    public override readonly bool Equals(object obj) =>
        obj is CircuitRestoreData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _items,
            _toggablePorts,
            _cables
        );

    public static bool operator ==(CircuitRestoreData left, CircuitRestoreData right) =>
        left.Equals(right);

    public static bool operator !=(CircuitRestoreData left, CircuitRestoreData right) =>
        !left.Equals(right);
}