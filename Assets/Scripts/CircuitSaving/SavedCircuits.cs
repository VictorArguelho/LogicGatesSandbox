using System;
using UnityEngine;

[Serializable]
public struct SavedCircuits
{
    [SerializeField] private string[] _circuitPaths;

    public readonly string[] CircuitPaths => _circuitPaths;

    public SavedCircuits(string[] circuitPaths) =>
        _circuitPaths = circuitPaths;
}