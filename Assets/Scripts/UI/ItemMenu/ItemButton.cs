using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image _image;

    private ItemDataAsset _item;
    private ItemSelector _itemSelector;
    private ItemInfoPanel _itemInfoPanel;

    public void Initialize(
        ItemDataAsset item,
        ItemSelector itemSelector,
        ItemInfoPanel itemInfoPanel)
    {
        _item = item;
        _itemSelector = itemSelector;
        _itemInfoPanel = itemInfoPanel;

        _image.sprite = AssetsManager.Instance.TryGetSprite(item.GetData().SpriteCode);
    }

    public void Select() =>
        _itemSelector.Select(_item);

    public void ShowInfo() =>
        _itemInfoPanel.Show(_item.GetData());
}