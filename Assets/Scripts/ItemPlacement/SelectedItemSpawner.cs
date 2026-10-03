using UnityEngine;

public class SelectedItemPlacer : MonoBehaviour
{
    private void Awake() =>
        InputManager.Instance.OnConfirmPlaceItem += HandleActivate;

    private void HandleActivate()
    {
        var itemData = ItemSelector.Instance.SelectedItem;
        CircuitElementSpawner.TrySpawnItem(
                itemData,
                MouseManager.MouseWorldPosition - new Vector2(itemData.Size.x / 2f, - itemData.Size.y / 2f)
            );
    }     
}