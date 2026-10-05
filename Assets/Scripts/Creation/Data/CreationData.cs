using System;
using UnityEngine;

[Serializable]
public struct CreationData : IEquatable<CreationData>
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private CircuitData _circuitData;
    [SerializeField] private bool _isItem;

    public readonly ItemData ItemData => _itemData;
    public readonly CircuitData CircuitData => _circuitData;
    public readonly bool IsItem => _isItem;

    public readonly ItemCategoryCode Category => _isItem ? _itemData.Category : _circuitData.Category;
    public readonly string Name => _isItem ? _itemData.Name : _circuitData.Name;
    public readonly string Description => _isItem ? _itemData.Description : _circuitData.Description;

    public CreationData(ItemData itemData)
    {
        _itemData = itemData;
        _circuitData = CircuitData.Invalid;
        _isItem = true;
    }

    public CreationData(CircuitData circuitData)
    {
        _itemData = ItemData.Invalid;
        _circuitData = circuitData;
        _isItem = false;
    }

    public readonly Sprite GetSprite()
    {
        if (IsItem)
            return AssetsManager.Instance.TryGetSprite(ItemData.SpriteCode);

        if (CircuitLoader.TryLoadCircuitSprite(CircuitData.DirectoryName, out var sprite))
            return sprite;

        return null;
    }

    public readonly bool Equals(CreationData other) =>
        _isItem == other._isItem &&
        _itemData == other._itemData &&
        _circuitData == other._circuitData;

    public override readonly bool Equals(object obj) =>
        obj is CreationData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _itemData,
            _circuitData,
            _isItem
        );

    public static bool operator ==(CreationData left, CreationData right) =>
        left.Equals(right);

    public static bool operator !=(CreationData left, CreationData right) =>
        !left.Equals(right);
}