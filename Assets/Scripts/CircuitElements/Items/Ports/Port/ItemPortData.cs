using System;
using UnityEngine;

[Serializable]
public struct ItemPortData
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
}