using UnityEngine;

public class AssetsManager : Singleton<AssetsManager>
{
    [Header("Gates")]
    [SerializeField] private Sprite _andSprite;
    [SerializeField] private Sprite _orSprite;
    [SerializeField] private Sprite _notSprite;
    [SerializeField] private Sprite _xorSprite;
    [SerializeField] private Sprite _norSprite;
    [SerializeField] private Sprite _nandSprite;
    [SerializeField] private Sprite _xnorSprite;

    [Header("Items")]
    [SerializeField] private Sprite _portSprite;
    [SerializeField] private Sprite _toggablePortSprite;

    public SpriteCode TryGetSpriteCode(Sprite sprite)
    {
        if (sprite == _andSprite) return SpriteCode.GateAnd;
        if (sprite == _orSprite) return SpriteCode.GateOr;
        if (sprite == _notSprite) return SpriteCode.GateNot;
        if (sprite == _xorSprite) return SpriteCode.GateXor;
        if (sprite == _norSprite) return SpriteCode.GateNor;
        if (sprite == _nandSprite) return SpriteCode.GateNand;
        if (sprite == _xnorSprite) return SpriteCode.GateXnor;

        if (sprite == _portSprite) return SpriteCode.Port;
        if (sprite == _toggablePortSprite) return SpriteCode.ToggablePort;

        return SpriteCode.None;
    }

    public Sprite TryGetSprite(SpriteCode code)
    {
        return code switch
        {
            SpriteCode.GateAnd => _andSprite,
            SpriteCode.GateOr => _orSprite,
            SpriteCode.GateNot => _notSprite,
            SpriteCode.GateXor => _xorSprite,
            SpriteCode.GateNor => _norSprite,
            SpriteCode.GateNand => _nandSprite,
            SpriteCode.GateXnor => _xnorSprite,

            SpriteCode.Port => _portSprite,
            SpriteCode.ToggablePort => _toggablePortSprite,

            _ => null
        };
    }
}