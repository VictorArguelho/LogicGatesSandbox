using System;
using UnityEngine;

public class ItemSelector : Singleton<ItemSelector>
{
    [SerializeField] private ItemDataAsset _defaultItem;

    public ItemDataAsset SelectedItemAsset { get; private set; }
    public ItemData SelectedItem { get; private set; }

    public event Action<ItemData> OnItemSelected;

    private void Start() =>
        Select(_defaultItem);

    public void Select(ItemDataAsset item)
    {
        if (SelectedItemAsset == item)
            return;

        SelectedItemAsset = item;
        SelectedItem = item.GetData();

        OnItemSelected?.Invoke(SelectedItem);
    }
}