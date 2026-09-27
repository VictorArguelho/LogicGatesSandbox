using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Selectable))]
public class SelectableAppearance : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _mouseOverColor = new(0.75f, 0.75f, 0.75f, 0.9f);
    [SerializeField] private Color _selectedColor = new(0.5f, 0.5f, 0.5f, 0.8f);

    public Color DefaultColor
    {
        get => _defaultColor;
        set => _defaultColor = value;
    }

    public Color MouseOverColor
    {
        get => _mouseOverColor;
        set => _mouseOverColor = value;
    }

    public Color SelectedColor
    {
        get => _selectedColor;
        set => _selectedColor = value;
    }

    private SpriteRenderer _renderer;
    private Selectable _selectable;

    private SelectionState _currentState;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _selectable = GetComponent<Selectable>();

        _currentState = _selectable.State;
        RefreshColor();
    }

    private void Update()
    {
        if (_selectable.State != _currentState)
        {
            _currentState = _selectable.State;
            RefreshColor();
        }
    }

    public void RefreshColor()
    {
        switch (_currentState)
        {
            case SelectionState.Default:
                _renderer.color = DefaultColor;
                break;
            case SelectionState.MouseOver:
                _renderer.color = MouseOverColor;
                break;
            case SelectionState.Selected:
                _renderer.color = SelectedColor;
                break;
        }
    }
}