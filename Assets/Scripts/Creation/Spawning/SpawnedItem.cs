using System;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    [SerializeField] private uint id;
    public uint Id { get; private set; }
    public ItemData Data { get; private set; }

    public event Action OnDeleted;

    public void Initialize(uint id, ItemData data)
    {
        this.id = id;
        Id = id;
        Data = data;
    }

    private void OnDestroy() =>
        OnDeleted?.Invoke();
}