using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    private void Awake() =>
        PlacementSelector.Instance.OnCreationDataSelected += UpdateDisplay;

    private void UpdateDisplay(CreationData creationData)
    {
        _image.sprite = creationData.GetSprite();
        _name.text = creationData.Name;
    }
}