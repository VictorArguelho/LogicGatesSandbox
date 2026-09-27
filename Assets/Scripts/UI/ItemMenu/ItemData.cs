using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private ItemCode _code;
    [SerializeField] private string _name;

    [Header("Appearance")]
    [SerializeField] private Sprite _image;

    [Header("Information")]
    [TextArea]
    [SerializeField] private string _description;

    [Header("Category")]
    [SerializeField] private ItemCategory _category;

    public ItemCode Code => _code;
    public string Name => _name;
    public Sprite Image => _image;
    public string Description => _description;
    public ItemCategory Category => _category;
}