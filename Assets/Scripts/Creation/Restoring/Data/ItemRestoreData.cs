using System;
using UnityEngine;

[Serializable]
public struct ItemRestoreData : IEquatable<ItemRestoreData>
{
    [SerializeField] private uint _id;
    [SerializeField] private Vector2 _relativePosition;
    [SerializeField] private ItemData _itemData;

    public readonly uint Id => _id;
    public readonly Vector2 RelativePosition => _relativePosition;
    public readonly ItemData ItemData => _itemData;

    public static ItemRestoreData Invalid =>
        new(0, Vector2.zero, ItemData.Invalid);

    public ItemRestoreData(
        uint id,
        Vector2 relativePosition,
        ItemData itemData
    )
    {
        _id = id;
        _relativePosition = relativePosition;
        _itemData = itemData;
    }

    public readonly bool Equals(ItemRestoreData other) =>
        _id == other._id &&
        _relativePosition == other._relativePosition &&
        _itemData == other._itemData;

    public override readonly bool Equals(object obj) =>
        obj is ItemRestoreData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _id,
            _relativePosition,
            _itemData
        );

    public static bool operator ==(ItemRestoreData left, ItemRestoreData right) =>
        left.Equals(right);

    public static bool operator !=(ItemRestoreData left, ItemRestoreData right) =>
        !left.Equals(right);
}