using UnityEngine;

public static class CircuitElementSpawner
{
    public static SpawnedItem TrySpawnItem(ItemData itemData, Vector2 position) =>
        ItemSpawner.Instance.TrySpawnItem(itemData, position);

    public static Cable TrySpawnCable(Port outPort, Port inPort) =>
        CableSpawner.Instance.TrySpawnCable(outPort, inPort);
}