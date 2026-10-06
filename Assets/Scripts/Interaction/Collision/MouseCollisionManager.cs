using System.Collections.Generic;
using UnityEngine;

public class MouseCollisionManager : Singleton<MouseCollisionManager>
{
    [SerializeField] private float _cellSize = 5f;

    private readonly Dictionary<Vector2Int, List<MouseCollider>> _cells = new();
    private readonly Dictionary<MouseCollider, Vector2Int> _colliderCells = new();

    private readonly List<MouseCollider> _currentColliders = new();

    public void Register(MouseCollider collider)
    {
        var cell = GetCell(collider.transform.position);

        if (!_cells.TryGetValue(cell, out var colliders))
        {
            colliders = new List<MouseCollider>();
            _cells.Add(cell, colliders);
        }

        colliders.Add(collider);
        _colliderCells.Add(collider, cell);
    }

    public void Unregister(MouseCollider collider)
    {
        if (!_colliderCells.TryGetValue(collider, out var cell))
            return;

        var colliders = _cells[cell];

        colliders.Remove(collider);

        if (colliders.Count == 0)
            _cells.Remove(cell);

        _colliderCells.Remove(collider);
    }

    private void Update()
    {
        var mouseCell = GetCell(MouseManager.MouseWorldPosition);

        _currentColliders.Clear();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                var cell = mouseCell + new Vector2Int(x, y);

                if (!_cells.TryGetValue(cell, out var colliders))
                    continue;

                _currentColliders.AddRange(colliders);
            }
        }

        for (int i = 0; i < _currentColliders.Count; i++)
            _currentColliders[i].UpdateMouseState();
    }

    private Vector2Int GetCell(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / _cellSize),
            Mathf.FloorToInt(position.y / _cellSize)
        );
    }
}