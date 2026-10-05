using System.Collections.Generic;
using UnityEngine;

public class SelectionMenu : Singleton<SelectionMenu>
{
    [SerializeField] private SelectionButton _itemButtonPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private ItemDataAsset[] _defaultItens;

    private readonly List<CreationData> _creations = new();
    private ItemCategoryCode _currentCategory;

    private void Start()
    {
        foreach (var item in _defaultItens)
            _creations.Add(new(item.GetData()));

        ShowCategory(ItemCategoryCode.Gates);
    }

    public void AddItem(CreationData item)
    {
        _creations.Add(item);

        if (item.Category != _currentCategory)
            return;

        var itemButton = Instantiate(
            _itemButtonPrefab,
            _content
        );

        itemButton.Initialize(item);
    }

    public void ShowCategory(ItemCategoryCode category)
    {
        _currentCategory = category;

        ClearItems();
        CreateItems();
    }

    private void CreateItems()
    {
        foreach (var item in _creations)
        {
            if (item.Category != _currentCategory)
                continue;

            var itemButton = Instantiate(
                _itemButtonPrefab,
                _content
            );

            itemButton.Initialize(item);
        }
    }

    private void ClearItems()
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);
    }
}