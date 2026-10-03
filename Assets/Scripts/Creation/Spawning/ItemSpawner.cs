using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _gatePrefab;
    [SerializeField] private GameObject _portPrefab;
    [SerializeField] private GameObject _toggablePortPrefab;

    private readonly HashSet<uint> _takenIds = new();

    public SpawnedItem TrySpawnItem(ItemData itemData, Vector2 position)
    {
        var item = TryCreateItem(itemData, position);

        if (item == null)
            return null;

        item.transform.localScale = Vector2.one * itemData.Scale;

        var spawnedComponent = item.AddComponent<SpawnedItem>();
        var id = GenerateItemId();

        spawnedComponent.Initialize(id, itemData);
        spawnedComponent.OnDeleted += () => _takenIds.Remove(id);

        return spawnedComponent;
    }

    private GameObject TryCreateItem(ItemData itemData, Vector2 position)
    {
        if (itemData.Code.ToGateCode() != GateCode.None)
            return SpawnGate(itemData, position);

        var prefab = GetPrefab(itemData);

        if (prefab == null)
            return null;

        return Instantiate(prefab, position, Quaternion.identity);
    }

    private GameObject SpawnGate(ItemData itemData, Vector2 position)
    {
        var gate = Instantiate(_gatePrefab, position, Quaternion.identity).GetComponent<Gate>();
        gate.Initialize(itemData);
        return gate.gameObject;
    }

    private GameObject GetPrefab(ItemData itemData) =>
        itemData.Code switch
        {
            ItemCode.Port => _portPrefab,
            ItemCode.ToggablePort => _toggablePortPrefab,
            _ => null
        };

    private uint GenerateItemId()
    {
        uint id;

        do
            id = GetRandomId();
        while (!_takenIds.Add(id));

        return id;
    }

    private uint GetRandomId()
    {
        uint id;

        do
        {
            id = (uint)(
                RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue)
                + (long)int.MaxValue
                + 1
            );
        }
        while (id == 0);

        return id;
    }
}