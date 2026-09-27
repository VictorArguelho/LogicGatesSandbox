using UnityEngine;

public class Wire : MonoBehaviour
{
    private const float AB_PART_SIZE = 0.055f;

    [SerializeField] private Transform _a;
    [SerializeField] private Transform _middle;
    [SerializeField] private Transform _b;

    private SpriteRenderer _aRenderer;
    private SpriteRenderer _bRenderer;
    private SpriteRenderer _middleRenderer;

    private void Awake()
    {
        _aRenderer = _a.GetComponent<SpriteRenderer>();
        _bRenderer = _b.GetComponent<SpriteRenderer>();
        _middleRenderer = _middle.GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        _aRenderer.color = color;
        _bRenderer.color = color;
        _middleRenderer.color = color;
    }

    public void SetPoints(Vector2 pointA, Vector2 pointB)
    {
        var difference = pointB - pointA;
        var distance = difference.magnitude;
        float angle = Mathf.Atan2(
            difference.y,
            difference.x
        ) * Mathf.Rad2Deg;

        var scale = transform.localScale;
        transform.localScale = Vector3.one;

        _a.localPosition = Vector2.zero;
        _b.localPosition = new(distance / scale.x, 0, 0);
        _middle.localPosition = new(AB_PART_SIZE, 0f, 0f);

        _middle.localScale = new(
            distance / scale.x - AB_PART_SIZE * 2f,
            1f,
            1f
        );

        transform.SetPositionAndRotation(pointA, Quaternion.Euler(0f, 0f, angle));
        transform.localScale = scale;
    }
}