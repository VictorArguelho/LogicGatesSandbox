using UnityEngine;

[RequireComponent(typeof(GridDraggable))]
[RequireComponent(typeof(MouseCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class DefaultGate : MonoBehaviour
{
    [SerializeField] private DefaultGateCode _gateCode;

    private void Awake()
    {
        var render = GetComponent<SpriteRenderer>();
        render.sprite = AssetsManager.Instance.GetDefaultGateSprite(_gateCode);
    }
}