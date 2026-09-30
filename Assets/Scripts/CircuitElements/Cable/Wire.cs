using UnityEngine;

public class Wire : MonoBehaviour
{
    private const float AB_PART_SIZE = 0.055f;

    [SerializeField] private Transform _a;
    [SerializeField] private Transform _b;

    public void SetPoints(Vector2 pointA, Vector2 pointB)
    {
        var difference = pointB - pointA;
        var distance = difference.magnitude;

        if (distance <= AB_PART_SIZE * 2f)
            return;

        var direction = difference / distance;

        var angle = Mathf.Atan2(
            difference.y,
            difference.x
        ) * Mathf.Rad2Deg;

        var middleDistance = distance - AB_PART_SIZE * 2f;

        transform.SetPositionAndRotation(
            pointA + direction * AB_PART_SIZE,
            Quaternion.Euler(0f, 0f, angle)
        );

        transform.localScale = new(
            middleDistance,
            1f,
            1f
        );

        _a.localPosition = new(
            -AB_PART_SIZE / middleDistance,
            0f,
            0f
        );

        _b.localPosition = new(
            (distance - AB_PART_SIZE) / middleDistance,
            0f,
            0f
        );

        _a.localScale = new(
            1f / middleDistance,
            1f,
            1f
        );

        _b.localScale = new(
            1f / middleDistance,
            1f,
            1f
        );
    }
}