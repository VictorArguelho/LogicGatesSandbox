using TMPro;
using UnityEngine;

public class SelectionInfoPanel : Singleton<SelectionInfoPanel>
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;

    private void Start() =>
        gameObject.SetActive(false);

    public void Show(CreationData creationData)
    {
        InputManager.Instance.DetectKeyPress = false;

        _name.text = creationData.Name;
        _description.text = creationData.Description;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        InputManager.Instance.DetectKeyPress = true;

        gameObject.SetActive(false);
    }
}