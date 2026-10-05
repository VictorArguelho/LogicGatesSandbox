using System;
using UnityEngine;

[Serializable]
public struct PortRestoreData : IEquatable<PortRestoreData>
{
    [SerializeField] private uint _id;

    [SerializeField] private bool _isFromItem;
    [SerializeField] private ItemPortData _itemPortData;

    public readonly uint Id => _id;

    public readonly bool IsFromItem => _isFromItem;
    public readonly ItemPortData ItemPortData => _itemPortData;

    public static PortRestoreData Invalid =>
        new(0, ItemPortData.Invalid);

    public PortRestoreData(uint id)
    {
        _isFromItem = false;
        _id = id;
        _itemPortData = default;
    }

    public PortRestoreData(uint id, ItemPortData itemPortData)
    {
        _isFromItem = true;
        _id = id;
        _itemPortData = itemPortData;
    }

    public readonly bool Equals(PortRestoreData other) =>
        _id == other._id &&
        _isFromItem == other._isFromItem &&
        _itemPortData == other._itemPortData;

    public override readonly bool Equals(object obj) =>
        obj is PortRestoreData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _id,
            _isFromItem,
            _itemPortData
        );

    public static bool operator ==(PortRestoreData left, PortRestoreData right) =>
        left.Equals(right);

    public static bool operator !=(PortRestoreData left, PortRestoreData right) =>
        !left.Equals(right);
}