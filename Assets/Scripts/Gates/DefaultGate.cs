using UnityEngine;

[RequireComponent(typeof(SelectableTarget))]
[RequireComponent(typeof(GridDraggable))]
[RequireComponent(typeof(MouseCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class DefaultGate : MonoBehaviour
{
    [SerializeField] private DefaultGateCode _gateCode;

    private void Awake()
    {
        var col = GetComponent<MouseCollider>();
        col.Size = new Vector2(3f, 2f);
        col.Offset = new Vector2(0.5f, -0.5f);

        var render = GetComponent<SpriteRenderer>();
        render.sprite = AssetsManager.Instance.GetDefaultGateSprite(_gateCode);
    }
}