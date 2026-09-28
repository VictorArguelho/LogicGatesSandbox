using UnityEngine;

public readonly struct ClipboardItemData
{
    public readonly uint Id { get; }
    public readonly Vector2 DistanceAtMouse { get; }
    public readonly ItemCode ItemCode { get; }
    public readonly ItemCategory CategoryCode { get; }

    public ClipboardItemData(
        uint id,
        Vector2 distanceAtMouse, 
        ItemCode itemCode, 
        ItemCategory categoryCode
    )
    {
        Id = id;
        DistanceAtMouse = distanceAtMouse;
        ItemCode = itemCode;
        CategoryCode = categoryCode;
    }
}