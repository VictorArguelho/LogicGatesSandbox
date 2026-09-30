using System.Collections.Generic;
using UnityEngine;

public static class CircuitDataBuilder
{
    private static readonly List<ItemRestoreData> _restoredItems = new();
    private static readonly List<ToggablePortRestoreData> _restoredToggablePorts = new();
    private static readonly List<CableRestoreData> _restoredCables = new();

    public static CircuitRestoreData Build(GameObject[] gameObjects, Vector2 relativePosition)
    {
        _restoredItems.Clear();
        _restoredToggablePorts.Clear();
        _restoredCables.Clear();

        foreach (var gameObject in gameObjects)
        {
            Debug.Log(
                $"GameObject: {gameObject.name}\n" +
                $"Position: {gameObject.transform.position}"
            );

            if (gameObject.TryGetComponent<IRestorableItem>(out var itemRestorable))
            {
                var itemRelativePosition =(Vector2)gameObject.transform.position - relativePosition;

                _restoredItems.Add(itemRestorable.GetItemRestoreData(itemRelativePosition));
            }

            if (gameObject.TryGetComponent<IRestorable<ToggablePortRestoreData>>(out var toggablePortRestorable))
            {
                _restoredToggablePorts.Add(
                    toggablePortRestorable.GetRestoreData()
                );
            }

            if (gameObject.TryGetComponent<IRestorable<CableRestoreData>>(out var cableRestorable))
            {
                _restoredCables.Add(
                    cableRestorable.GetRestoreData()
                );
            }
        }

        CircuitRestoreData a = new(
            _restoredItems.ToArray(),
            _restoredToggablePorts.ToArray(),
            _restoredCables.ToArray()
        );

        Debug.Log(JsonUtility.ToJson(a));

        return a;
    }
}