using UnityEngine;

public class SelectedItemSpawner : MonoBehaviour
{
    private void Awake() =>
        InputManager.Instance.OnStartPlaceItem += HandleActivate;

    private void HandleActivate() =>
        CircuitElementSpawner.TrySpawnItem(
                ItemSelector.Instance.SelectedItem.Code,
                MouseManager.MouseWorldPosition
            );
}