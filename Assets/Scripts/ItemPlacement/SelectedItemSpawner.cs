using UnityEngine;

public class SelectedItemSpawner : MonoBehaviour
{
    private void Awake() =>
        InputManager.Instance.OnStartPlaceItem += HandleActivate;

    private void HandleActivate()
    {
        var itemData = ItemSelector.Instance.SelectedItem;
        CircuitElementSpawner.TrySpawnItem(
                itemData,
                MouseManager.MouseWorldPosition - new Vector2(itemData.Size.x / 2, - itemData.Size.y / 2)
            );
    }     
}