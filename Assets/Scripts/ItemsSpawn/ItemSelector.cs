using System;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private ItemData _defaultItem;
    public ItemData SelectedItem { get; private set; }

    public event Action<ItemData> OnItemSelected;

    private void Start() =>
        Select(_defaultItem);

    public void Select(ItemData item)
    {
        if (SelectedItem == item)
            return;

        SelectedItem = item;
        OnItemSelected?.Invoke(item);
    }
}