using System;
using UnityEngine;

[Serializable]
public struct ItemData : IEquatable<ItemData>
{
    [SerializeField] private ItemCode _code;
    [SerializeField] private ItemCategoryCode _category;

    [SerializeField] private string _name;
    [SerializeField] private SpriteCode _spriteCode;
    [SerializeField] private string _description;

    [SerializeField] private float _scale;
    [SerializeField] private Vector2 _baseSize;

    public readonly ItemCode Code => _code;
    public readonly ItemCategoryCode Category => _category;

    public readonly string Name => _name;
    public readonly SpriteCode SpriteCode => _spriteCode;
    public readonly string Description => _description;

    public readonly float Scale => _scale;
    public readonly Vector2 BaseSize => _baseSize;
    public readonly Vector2 Size => _baseSize * _scale;

    public static ItemData Invalid =>
        new(
            ItemCode.None,
            ItemCategoryCode.None,
            string.Empty,
            SpriteCode.None,
            string.Empty,
            0f,
            Vector2.zero
        );

    public ItemData(
        ItemCode code,
        ItemCategoryCode category,

        string name,
        SpriteCode spriteCode,
        string description,

        float scale,
        Vector2 baseSize
    )
    {
        _code = code;
        _category = category;

        _name = name;
        _spriteCode = spriteCode;
        _description = description;

        _scale = scale;
        _baseSize = baseSize;
    }

    public readonly bool Equals(ItemData other) =>
        _code == other._code &&
        _category == other._category &&
        _name == other._name &&
        _spriteCode == other._spriteCode &&
        _description == other._description &&
        _scale == other._scale &&
        _baseSize == other._baseSize;

    public override readonly bool Equals(object obj) =>
        obj is ItemData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _code,
            _category,
            _name,
            _spriteCode,
            _description,
            _scale,
            _baseSize
        );

    public static bool operator ==(ItemData left, ItemData right) =>
        left.Equals(right);

    public static bool operator !=(ItemData left, ItemData right) =>
        !left.Equals(right);
}