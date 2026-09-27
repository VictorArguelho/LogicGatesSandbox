using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SelectionVisualizer : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake() =>
        _renderer = GetComponent<SpriteRenderer>();

    private void Update()
    {
        if (!SelectionManager.IsMultipleSelecting)
        {
            _renderer.enabled = false;
            return;
        }

        _renderer.enabled = true;

        var start = SelectionManager.StartMultipleSelectingPosition;
        var end = MouseManager.MouseWorldPosition;

        var min = Vector2.Min(start, end);
        var max = Vector2.Max(start, end);

        transform.position = (min + max) / 2f;
        transform.localScale = max - min;
    }
}