using UnityEngine;

public class CableSpawner : Singleton<CableSpawner>
{
    [SerializeField] private GameObject _cablePrefab;

    public Cable TrySpawnCable(Port outPort, Port inPort)
    {
        if (!outPort.TryConnectAtOut(inPort))
            return null;

        var cable = Instantiate(_cablePrefab, Vector2.zero, Quaternion.identity).GetComponent<Cable>();
        cable.Initialize(outPort, inPort);
        return cable;
    }
}