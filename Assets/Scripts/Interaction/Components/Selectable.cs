using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Selectable : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _semiSelectedColor = new(0.75f, 0.75f, 0.75f, 0.9f);
    [SerializeField] private Color _selectedColor = new(0.5f, 0.5f, 0.5f, 0.8f);

    private SpriteRenderer _renderer;

    private SelectionState _state = SelectionState.Default;
    public SelectionState State => _state;

    private void Awake() =>
        _renderer = GetComponent<SpriteRenderer>();

    public void SetState(SelectionState state)
    {
        if (_state == state)
            return;

        _state = state;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _renderer.color = _state switch
        {
            SelectionState.Default => _defaultColor,
            SelectionState.SemiSelected => _semiSelectedColor,
            SelectionState.Selected => _selectedColor,
            _ => _defaultColor
        };
    }
}