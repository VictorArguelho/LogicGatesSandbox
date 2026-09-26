using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MultipleSelectionVisualizer : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake() =>
        _renderer = GetComponent<Renderer>();

    private void Update()
    {
        if (!MultipleSelector.IsSelecting)
        {
            _renderer.enabled = false;
            return;
        }

        _renderer.enabled = true;

        var start = MultipleSelector.StartSelectionPoint;
        var end = MouseManager.MouseWorldPosition;

        var min = Vector2.Min(start, end);
        var max = Vector2.Max(start, end);

        transform.position = (min + max) / 2f;
        transform.localScale = max - min;
    }
}