using System;
using UnityEngine;

[Serializable]
public struct CircuitData : IEquatable<CircuitData>
{
    [SerializeField] private CircuitRestoreData _restoreData;
    [SerializeField] private ItemCategoryCode _category;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _directoryName;

    public readonly CircuitRestoreData RestoreData => _restoreData;
    public readonly ItemCategoryCode Category => _category;

    public readonly string Name => _name;
    public readonly string Description => _description;
    public readonly string DirectoryName => _directoryName;

    public static CircuitData Invalid =>
        new(
            CircuitRestoreData.Invalid,
            ItemCategoryCode.None,
            string.Empty,
            string.Empty,
            string.Empty
        );

    public CircuitData(
        CircuitRestoreData restoreData,
        ItemCategoryCode category,
        string name,
        string description,
        string directoryName
    )
    {
        _restoreData = restoreData;
        _category = category;
        _name = name;
        _description = description;
        _directoryName = directoryName;
    }

    public readonly bool Equals(CircuitData other) =>
        _restoreData == other._restoreData &&
        _category == other._category &&
        _name == other._name &&
        _description == other._description &&
        _directoryName == other._directoryName;

    public override readonly bool Equals(object obj) =>
        obj is CircuitData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _restoreData,
            _category,
            _name,
            _description,
            _directoryName
        );

    public static bool operator ==(CircuitData left, CircuitData right) =>
        left.Equals(right);

    public static bool operator !=(CircuitData left, CircuitData right) =>
        !left.Equals(right);
}