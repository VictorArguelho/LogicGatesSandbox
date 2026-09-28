using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance { get; private set; }

    [Header("References")]
    [SerializeField] private ItemSelector _itemSelector;

    [Header("Gates Prefabs")]
    [SerializeField] private DefaultGate _defaultGate;
    [SerializeField] private DefaultGate _notGate;

    [Header("Items Prefabs")]
    [SerializeField] private GameObject _port;
    [SerializeField] private GameObject _toggablePort;

    private readonly HashSet<uint> _takenIds = new();

    private void Awake() =>
        Instance = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnObject(
                _itemSelector.SelectedItem.Code, 
                _itemSelector.SelectedItem.Category
            );
    }

    public GameObject SpawnObject(ItemCode item, ItemCategory category)
    {
        var gameObject = category switch
        {
            ItemCategory.Gates => SpawnGate(item),
            ItemCategory.Items => SpawnItem(item),
            _ => null
        };

        if (gameObject != null)
        {
            var id = GenerateItemId();

            var spawnedComponent = gameObject.AddComponent<SpawnedItem>();
            spawnedComponent.Initialize(id, item, category);

            spawnedComponent.OnDeleted += () => _takenIds.Remove(id);
        }
            
        return gameObject;
    }

    private GameObject SpawnGate(ItemCode item)
    {
        var prefab = item == ItemCode.GateNot ? _notGate : _defaultGate;

        var gate = InstantiatePrefab(prefab);
        gate.SetGateCode(Conversions.ItemCodeToGateCode(item));
        return gate.gameObject;
    }

    private GameObject SpawnItem(ItemCode item)
    {
        return item switch
        {
            ItemCode.Port => InstantiatePrefab(_port),
            ItemCode.PortToggable => InstantiatePrefab(_toggablePort),
            _ => null
        };
    }

    private T InstantiatePrefab<T>(T prefab) where T : MonoBehaviour =>
        Instantiate(prefab, MouseManager.MouseWorldPosition, Quaternion.identity);

    private GameObject InstantiatePrefab(GameObject prefab) =>
        Instantiate(prefab, MouseManager.MouseWorldPosition, Quaternion.identity);

    private uint GenerateItemId()
    {
        uint id;

        do
            id = GetRandomId();
        while (!_takenIds.Add(id));

        return id;
    }

    private uint GetRandomId() =>
        (uint)(RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue) + (long)int.MaxValue + 1);
}