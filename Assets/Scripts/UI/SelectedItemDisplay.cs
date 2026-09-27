using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    private void Start() =>
        FindFirstObjectByType<ItemSelector>().OnItemSelected += UpdateDisplay;

    private void UpdateDisplay(ItemData item)
    {
        _image.sprite = item.Image;
        _name.text = item.Name;
    }
}