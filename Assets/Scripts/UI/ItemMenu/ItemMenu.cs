using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private ItemButton _itemButtonPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private ItemSelector _itemSelector;
    [SerializeField] private ItemData[] _items;

    private ItemCategory _currentCategory;

    private void Awake()
    {
        ShowCategory(ItemCategory.Gates);
    }

    public void ShowCategory(ItemCategory category)
    {
        _currentCategory = category;

        ClearItems();
        CreateItems();
    }

    private void CreateItems()
    {
        foreach (var item in _items)
        {
            if (item.Category != _currentCategory)
                continue;

            var itemButton = Instantiate(
                _itemButtonPrefab,
                _content
            );

            itemButton.Initialize(item, _itemSelector);
        }
    }

    private void ClearItems()
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);
    }
}