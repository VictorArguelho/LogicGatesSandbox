using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image _image;

    private ItemData _item;
    private ItemSelector _itemSelector;

    public void Initialize(ItemData item, ItemSelector itemSelector)
    {
        _item = item;
        _itemSelector = itemSelector;

        _image.sprite = item.Image;
    }

    public void Select() =>
        _itemSelector.Select(_item);
}