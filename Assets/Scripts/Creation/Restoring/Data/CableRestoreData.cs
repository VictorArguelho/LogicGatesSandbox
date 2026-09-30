using System;
using UnityEngine;

[Serializable]
public struct CableRestoreData
{
    [SerializeField] private PortRestoreData _outPortData;
    [SerializeField] private PortRestoreData _inPortData;

    public readonly PortRestoreData OutPortData => _outPortData;
    public readonly PortRestoreData InPortData => _inPortData;

    public CableRestoreData(PortRestoreData outPortData, PortRestoreData inPortData)
    {
        _outPortData = outPortData;
        _inPortData = inPortData;
    }
}