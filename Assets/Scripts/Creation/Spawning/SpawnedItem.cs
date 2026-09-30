using System;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    [SerializeField] private uint id;
    public uint Id { get; private set; }
    public ItemCode Code { get; private set; }

    public event Action OnDeleted;

    public void Initialize(uint id, ItemCode code)
    {
        this.id = id;
        Id = id;
        Code = code;
    }

    private void OnDestroy() =>
        OnDeleted?.Invoke();
}