using UnityEngine;

public static class CreationManager
{
    public static void TryCreate(CreationData data, Vector2 position)
    {
        if (data.IsItem)
            ItemSpawner.Instance.TrySpawnItem(data.ItemData, position);
        else
            CircuitRestorer.Restore(data.CircuitData, position);
    }
}