using UnityEngine;

public class FreeDraggable : Draggable
{
    public override void ApplyMove(Vector2 move) =>
        transform.position += (Vector3)move;
}