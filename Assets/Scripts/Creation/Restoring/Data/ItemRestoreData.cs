using System;
using UnityEngine;

[Serializable]
public struct ItemRestoreData
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
}