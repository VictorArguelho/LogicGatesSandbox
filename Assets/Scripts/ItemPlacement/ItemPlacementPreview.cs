using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemPlacementPreview : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _showPreview;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;

        InputManager.Instance.OnStartPlaceItem += HolderStartPlace;
        InputManager.Instance.OnConfirmPlaceItem += HolderConfirmPlace;
    }

    private void Update()
    {
        if (_showPreview)
        {
            var itemData = ItemSelector.Instance.SelectedItem;
            var position = MouseManager.MouseWorldPosition - new Vector2(itemData.Size.x / 2, -itemData.Size.y / 2);
            transform.position = position;
        }
    }

    private void HolderStartPlace()
    {
        _showPreview = true;
        _renderer.enabled = true;

        var itemData = ItemSelector.Instance.SelectedItem;
        _renderer.sprite = AssetsManager.Instance.TryGetSprite(itemData.SpriteCode);
        transform.localScale = Vector2.one * itemData.Scale;
    }

    private void HolderConfirmPlace()
    {
        _showPreview = false;
        _renderer.enabled = false;
    }
}