using UnityEngine;

public struct ItemData
{
    [SerializeField] private ItemCode _code;
    [SerializeField] private ItemCategory _category;

    [SerializeField] private string _name;
    [SerializeField] private SpriteCode _spriteCode;
    [SerializeField] private string _description;

    [SerializeField] private float _scale;
    [SerializeField] private Vector2 _baseSize;

    public readonly ItemCode Code => _code;
    public readonly ItemCategory Category => _category;

    public readonly string Name => _name;
    public readonly SpriteCode SpriteCode => _spriteCode;
    public readonly string Description => _description;

    public readonly float Scale => _scale;
    public readonly Vector2 BaseSize => _baseSize;
    public readonly Vector2 Size => _baseSize * _scale;

    public static ItemData Invalid =>
        new(
            ItemCode.None,
            ItemCategory.None,
            string.Empty,
            SpriteCode.None,
            string.Empty,
            0f,
            Vector2.zero
        );

    public ItemData(
        ItemCode code,
        ItemCategory category,

        string name,
        SpriteCode spriteCode,
        string description,

        float scale,
        Vector2 baseSize
    )
    {
        _code = code;
        _category = category;

        _name = name;
        _spriteCode = spriteCode;
        _description = description;

        _scale = scale;
        _baseSize = baseSize;
    }
}