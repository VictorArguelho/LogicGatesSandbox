using System;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public uint Id { get; private set; }
    public ItemCode Code { get; private set; }
    public ItemCategory Category { get; private set; }

    public event Action OnDeleted;

    public void Initialize(uint id, ItemCode code, ItemCategory category)
    {
        Id = id;
        Code = code;
        Category = category;
    }

    private void OnDestroy() =>
        OnDeleted?.Invoke();
}