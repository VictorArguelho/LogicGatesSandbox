using System;
using TMPro;
using UnityEngine;

public class CircuitSavePanel : Singleton<CircuitSavePanel>
{
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _description;

    public event Action<string, string, string> OnConfirmSave;

    protected override void Awake()
    {
        base.Awake();

        gameObject.SetActive(false);
    }

    public void Show()
    {
        InputManager.Instance.DetectKeyPress = false;

        _name.text = string.Empty;
        _description.text = string.Empty;

        gameObject.SetActive(true);
    }
        

    public void ConfirmNameAndDescription()
    {
        if (_name.text == string.Empty || _description.text == string.Empty)
            return;

        InputManager.Instance.DetectKeyPress = true;

        gameObject.SetActive(false);
        var image = ImageSelector.SelectImage();
        OnConfirmSave?.Invoke(_name.text, _description.text, image);
    }
}