using System;
using UnityEngine;

[Serializable]
public struct ItemRestoreData
{
    [SerializeField] private uint _id;
    [SerializeField] private Vector2 _relativePosition;
    [SerializeField] private ItemCode _itemCode;

    public readonly uint Id => _id;
    public readonly Vector2 RelativePosition => _relativePosition;
    public readonly ItemCode ItemCode => _itemCode;

    public static ItemRestoreData Invalid =>
        new(0, Vector2.zero, ItemCode.None);

    public ItemRestoreData(
        uint id,
        Vector2 relativePosition,
        ItemCode itemCode
    )
    {
        _id = id;
        _relativePosition = relativePosition;
        _itemCode = itemCode;
    }
}