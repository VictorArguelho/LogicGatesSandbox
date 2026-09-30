using System;
using UnityEngine;

[Serializable]
public struct ToggablePortRestoreData
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
}