using UnityEngine;

public static class CircuitElementSpawner
{
    public static SpawnedItem TrySpawnItem(ItemCode itemCode, Vector2 position) =>
        ItemSpawner.Instance.TrySpawnItem(itemCode, position);

    public static Cable TrySpawnCable(Port outPort, Port inPort) =>
        CableSpawner.Instance.TrySpawnCable(outPort, inPort);
}