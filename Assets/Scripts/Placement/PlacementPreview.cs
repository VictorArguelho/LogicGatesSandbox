using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    [SerializeField] private Color _previewColor;

    private bool _showPreview;

    private readonly List<(SpriteRenderer, Vector2)> _previewRenderers = new();

    private void Awake()
    {
        InputManager.Instance.OnStartPlaceItem += HandleStartPlace;
        InputManager.Instance.OnConfirmPlaceItem += HandleConfirmPlace;
    }

    private void Update()
    {
        if (!_showPreview)
            return;

        var mousePosition = MouseManager.MouseWorldPosition;

        foreach (var (renderer, relativePosition) in _previewRenderers)
            renderer.transform.position = mousePosition + relativePosition;
    }

    private void HandleStartPlace()
    {
        _showPreview = true;

        var creationData = PlacementSelector.Instance.SelectedCreationData;

        if (creationData.IsItem)
        {
            var itemData = creationData.ItemData;
            GeneratePreviewObject(
                itemData, 
                new(- itemData.Size.x / 2, itemData.Size.y / 2)
            );
            return;
        }

        GeneratePreviewObjects(creationData.CircuitData.RestoreData);
    }

    private void HandleConfirmPlace()
    {
        if (!_showPreview)
            return;

        _showPreview = false;

        foreach (var (renderer, _) in _previewRenderers)
            Destroy(renderer.gameObject);

        _previewRenderers.Clear();
    }

    private void GeneratePreviewObject(ItemData itemData, Vector2 relativePosition)
    {
        var previewObject = new GameObject(
            $"Preview_{itemData.Code}"
        ).transform;

        previewObject.SetParent(transform);
        previewObject.localScale = Vector2.one * itemData.Scale;

        var previewRenderer = previewObject.AddComponent<SpriteRenderer>();
        previewRenderer.sprite = AssetsManager.Instance.TryGetSprite(
            itemData.SpriteCode
        );
        previewRenderer.color = _previewColor;

        _previewRenderers.Add((
            previewRenderer,
            relativePosition
        ));
    }

    private void GeneratePreviewObjects(CircuitRestoreData restoreData)
    {
        foreach (var itemRestoreData in restoreData.Items)
        {
            GeneratePreviewObject(
                itemRestoreData.ItemData,
                itemRestoreData.RelativePosition
            );
        }
    }
}