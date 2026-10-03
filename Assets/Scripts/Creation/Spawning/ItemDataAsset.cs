using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemDataAsset : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private ItemCode _code;
    [SerializeField] private string _name;

    [Header("Appearance")]
    [SerializeField] private Sprite _sprite;

    [Header("Information")]
    [TextArea]
    [SerializeField] private string _description;

    [Header("Category")]
    [SerializeField] private ItemCategory _category;

    [Header("Transform")]
    [SerializeField] private float _scale = 1f;
    [SerializeField] private Vector2 _baseSize = Vector2.one;
    
    public ItemData GetData() => new(
        _code,
        _category,
        _name,
        AssetsManager.Instance.TryGetSpriteCode(_sprite),
        _description,
        _scale,
        _baseSize
    );
}