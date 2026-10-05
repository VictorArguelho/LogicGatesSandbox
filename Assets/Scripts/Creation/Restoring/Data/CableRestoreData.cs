using System;
using UnityEngine;

[Serializable]
public struct CableRestoreData : IEquatable<CableRestoreData>
{
    [SerializeField] private PortRestoreData _outPortData;
    [SerializeField] private PortRestoreData _inPortData;

    public readonly PortRestoreData OutPortData => _outPortData;
    public readonly PortRestoreData InPortData => _inPortData;

    public CableRestoreData(
        PortRestoreData outPortData,
        PortRestoreData inPortData
    )
    {
        _outPortData = outPortData;
        _inPortData = inPortData;
    }

    public readonly bool Equals(CableRestoreData other) =>
        _outPortData == other._outPortData &&
        _inPortData == other._inPortData;

    public override readonly bool Equals(object obj) =>
        obj is CableRestoreData other && Equals(other);

    public override readonly int GetHashCode() =>
        HashCode.Combine(
            _outPortData,
            _inPortData
        );

    public static bool operator ==(CableRestoreData left, CableRestoreData right) =>
        left.Equals(right);

    public static bool operator !=(CableRestoreData left, CableRestoreData right) =>
        !left.Equals(right);
}