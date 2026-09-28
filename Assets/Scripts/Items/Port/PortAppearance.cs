using UnityEngine;

[RequireComponent(typeof(Port))]
[RequireComponent(typeof(SpriteRenderer))]
public class PortAppearance : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite _offSprite;
    [SerializeField] Sprite _onSprite;

    private Port _port;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _port = GetComponent<Port>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_port.Signal)
            _spriteRenderer.sprite = _onSprite;
        else
            _spriteRenderer.sprite = _offSprite;
    }
}