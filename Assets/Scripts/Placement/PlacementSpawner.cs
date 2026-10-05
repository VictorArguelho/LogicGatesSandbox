using UnityEngine;

public class PlacementSpawner : MonoBehaviour
{
    private void Awake() =>
        InputManager.Instance.OnConfirmPlaceItem += HandleActivate;

    private void HandleActivate()
    {
        var creationData = PlacementSelector.Instance.SelectedCreationData;
        var offSet = creationData.IsItem ? new Vector2(creationData.ItemData.Size.x / 2f, - creationData.ItemData.Size.y / 2f) : Vector2.zero;

        CreationManager.TryCreate(
            creationData,
            MouseManager.MouseWorldPosition - offSet
        );
    }     
}