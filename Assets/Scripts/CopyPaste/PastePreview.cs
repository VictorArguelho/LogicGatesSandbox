using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PastePreview : MonoBehaviour
{
    [SerializeField] private Color _previewColor;

    private bool _showPreview;
    private CircuitRestoreData _clipboard;

    private readonly List<(SpriteRenderer, Vector2)> _previewRenderers = new();

    private void Awake()
    {
        InputManager.Instance.OnStartPaste += HandleStartPaste;
        InputManager.Instance.OnConfirmPaste += HandleConfirmPaste;
    }

    private void Update()
    {
        if (_showPreview)
        {
            var mousePosition = MouseManager.MouseWorldPosition;

            foreach (var (renderer, relativePosition) in _previewRenderers)
                renderer.transform.position = mousePosition + relativePosition;
        }
    }

    private void HandleStartPaste()
    {
        if (CopyPasteManager.Instance.HasClipboard)
        {
            _showPreview = true;
            _clipboard = CopyPasteManager.Instance.Clipboard;

            GeneratePreviewObjects();
        }
    }

    private void HandleConfirmPaste()
    {
        if (_showPreview)
        {
            _showPreview = false;

            foreach (var (renderer, _) in _previewRenderers)
                Destroy(renderer.gameObject);

            _previewRenderers.Clear();
        }
    }

    private void GeneratePreviewObjects()
    {
        foreach (var itemRestoreData in _clipboard.Items)
        {
            var previewObject = new GameObject($"Preview_{itemRestoreData.Id}").transform;
            previewObject.SetParent(transform);
            previewObject.localScale = Vector2.one * itemRestoreData.ItemData.Scale;

            var previewRenderer = previewObject.AddComponent<SpriteRenderer>();
            previewRenderer.sprite = AssetsManager.Instance.TryGetSprite(itemRestoreData.ItemData.SpriteCode);
            previewRenderer.color = _previewColor;
            _previewRenderers.Add((
                previewRenderer,
                itemRestoreData.RelativePosition
            ));
        }
    }
}