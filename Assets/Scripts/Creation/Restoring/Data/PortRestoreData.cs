using System;
using UnityEngine;

[Serializable]
public struct PortRestoreData
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
}