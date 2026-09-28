using UnityEngine;

public class WireFactory : MonoBehaviour
{
    private static GameObject _wirePrefabStatic;

    [SerializeField] private GameObject _wirePrefab;

    private void Awake() =>
        _wirePrefabStatic = _wirePrefab;

    public static Wire CreateWire(Vector2 pointA, Vector2 pointB)
    {
        var wire = Instantiate(_wirePrefabStatic).GetComponent<Wire>();

        wire.SetPoints(pointA, pointB);

        return wire;
    }
}