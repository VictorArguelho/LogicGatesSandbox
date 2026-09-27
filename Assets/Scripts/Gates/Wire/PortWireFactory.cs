using UnityEngine;

public class PortWireFactory : MonoBehaviour
{
    private static GameObject _wirePrefabStatic;

    [SerializeField] private GameObject _wirePrefab;

    private void Awake() =>
        _wirePrefabStatic = _wirePrefab;

    public static PortWire CreateWire(Port outPort, Port inPort)
    {
        var wire = Instantiate(_wirePrefabStatic).GetComponent<PortWire>();
        wire.Initialize(outPort, inPort);
        return wire;
    }
}