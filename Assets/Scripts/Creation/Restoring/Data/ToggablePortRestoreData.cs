using System;
using UnityEngine;

[Serializable]
public struct ToggablePortRestoreData : IEquatable<ToggablePortRestoreData>
{
    [SerializeField] private uint _id;
    [SerializeField] private bool _signal;

    public readonly uint Id => _id;
    public readonly bool Signal => _signal;

    public static ToggablePortRestoreData Invalid =>
        new(0, false);

    public ToggablePortRestoreData(uint id, bool signal)
    {
        _id = id;
        _signal = signal;
    }

    public readonly bool Equals(ToggablePortRestoreData other) =>
        _id == other._id &&
        _signal == other._signal;

    public override readonly bool Equals(object obj) =>
        obj is ToggablePortRestoreData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _id,
            _signal
        );

    public static bool operator ==(
        ToggablePortRestoreData left,
        ToggablePortRestoreData right
    ) =>
        left.Equals(right);

    public static bool operator !=(
        ToggablePortRestoreData left,
        ToggablePortRestoreData right
    ) =>
        !left.Equals(right);
}