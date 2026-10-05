using System;
using UnityEngine;

[Serializable]
public struct ItemPortData : IEquatable<ItemPortData>
{
    [SerializeField] private PortDirection _direction;
    [SerializeField] private int _index;

    public readonly PortDirection Direction => _direction;
    public readonly int Index => _index;

    public readonly bool IsValid =>
        Direction != PortDirection.None &&
        Index >= 0;

    public static ItemPortData Invalid =>
        new(PortDirection.None, -1);

    public ItemPortData(PortDirection direction, int index)
    {
        _direction = direction;
        _index = index;
    }

    public readonly bool Equals(ItemPortData other) =>
        _direction == other._direction &&
        _index == other._index;

    public override readonly bool Equals(object obj) =>
        obj is ItemPortData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _direction,
            _index
        );

    public static bool operator ==(ItemPortData left, ItemPortData right) =>
        left.Equals(right);

    public static bool operator !=(ItemPortData left, ItemPortData right) =>
        !left.Equals(right);
}