using UnityEngine;

public class ItemCategoryButton : MonoBehaviour
{
    [SerializeField] private ItemCategory _category;
    [SerializeField] private ItemMenu _itemMenu;

    public void Select() =>
        _itemMenu.ShowCategory(_category);
}