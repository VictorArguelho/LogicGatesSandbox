using UnityEngine;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
    [SerializeField] private Image _image;

    private CreationData _creationData;

    public void Initialize(CreationData creationData)
    {
        _creationData = creationData;
        _image.sprite = creationData.GetSprite();
    }

    public void Select() =>
        PlacementSelector.Instance.Select(_creationData);

    public void ShowInfo() =>
        SelectionInfoPanel.Instance.Show(_creationData);
}