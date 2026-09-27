using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ItemSelector _itemSelector;

    [Header("Gates Prefabs")]
    [SerializeField] private DefaultGate _defaultGate;
    [SerializeField] private NotGate _notGate;

    [Header("Items Prefabs")]
    [SerializeField] private GameObject _port;
    [SerializeField] private GameObject _toggablePort;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnObject(
                _itemSelector.SelectedItem.Code, 
                _itemSelector.SelectedItem.Category
            );
    }

    private void SpawnObject(ItemCode item, ItemCategory category)
    {
        var itemTransform = category switch
        {
            ItemCategory.Gates => SpawnGate(item),
            ItemCategory.Items => SpawnItem(item),
            _ => null
        };
    }

    private Transform SpawnGate(ItemCode item)
    {
        if (item == ItemCode.GateNot)
            return Instantiate(_notGate, MouseManager.MouseWorldPosition, Quaternion.identity).transform;

        var gate = Instantiate(_defaultGate, MouseManager.MouseWorldPosition, Quaternion.identity);
        gate.SetGateCode(ItemCodeToGateCode(item));
        return gate.transform;
    }

    private Transform SpawnItem(ItemCode item)
    {
        switch (item)
        {
            case ItemCode.Port:
                return Instantiate(_port, MouseManager.MouseWorldPosition, Quaternion.identity).transform;
            case ItemCode.PortToggable:
                return Instantiate(_toggablePort, MouseManager.MouseWorldPosition, Quaternion.identity).transform;
        }

        return null;
    }

    private DefaultGateCode ItemCodeToGateCode(ItemCode item) =>
        item switch
        {
            ItemCode.GateAnd => DefaultGateCode.And,
            ItemCode.GateOr => DefaultGateCode.Or,
            ItemCode.GateNot => DefaultGateCode.Not,
            ItemCode.GateXor => DefaultGateCode.Xor,
            ItemCode.GateNor => DefaultGateCode.Nor,
            ItemCode.GateNand => DefaultGateCode.Nand,
            ItemCode.GateXnor => DefaultGateCode.Xnor,
            _ => DefaultGateCode.And
        };
}