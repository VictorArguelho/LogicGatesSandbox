using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedToolDisplay : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    private void Awake() =>
        FindFirstObjectByType<ToolManager>().OnToolChange += UpdateDisplay;

    private void UpdateDisplay(ToolCode toolCode)
    {
        _image.sprite = AssetsManager.Instance.GetToolSprite(toolCode);
        _name.text = GetToolName(toolCode);
    }

    private string GetToolName(ToolCode toolCode) =>
        toolCode switch
        {
            ToolCode.Selection => "Ferramenta de seleção",
            ToolCode.Cable => "Ferramenta de cabeamento",
            _ => "Unknown Tool"
        };
}