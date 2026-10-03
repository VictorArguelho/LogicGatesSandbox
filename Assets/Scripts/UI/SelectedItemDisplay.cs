using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    private void Awake() =>
        FindFirstObjectByType<ItemSelector>().OnItemSelected += UpdateDisplay;

    private void UpdateDisplay(ItemData item)
    {
        _image.sprite = AssetsManager.Instance.TryGetSprite(item.SpriteCode);
        _name.text = item.Name;
    }
}