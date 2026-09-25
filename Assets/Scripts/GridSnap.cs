using UnityEngine;

public class GridSnap : MonoBehaviour
{
    [SerializeField] private float _gridSize = 1f;
    [SerializeField] private Vector2 _gridOffset = Vector2.zero / 2f;

    private void Update()
    {
        var position = Vector2.zero;

        position.x =
            Mathf.Round(transform.position.x / _gridSize) * _gridSize;

        position.y =
            Mathf.Round(transform.position.y / _gridSize) * _gridSize;

        transform.position = position + _gridOffset;
    }
}