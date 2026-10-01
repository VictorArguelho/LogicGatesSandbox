using UnityEngine;

public class SelectedItemSpawner : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CircuitElementSpawner.TrySpawnItem(
                ItemSelector.Instance.SelectedItem.Code,
                MouseManager.MouseWorldPosition
            );
    }
}