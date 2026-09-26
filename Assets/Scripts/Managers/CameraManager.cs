using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float _zoomSensitivity = 1f;
    [SerializeField] private float _minZoom = 1f;
    [SerializeField] private float _maxZoom = 200f;
    [SerializeField] private float _zoomSmoothSpeed = 5f;

    [Header("Move")]
    [SerializeField] private float _moveSensitivity = 1f;
    [SerializeField] private float _moveSmoothSpeed = 15f;

    private Camera _camera;

    private float _targetZoom;
    private Vector3 _targetPosition;

    private void Start()
    {
        _camera = Camera.main;

        _targetZoom =
            _camera.orthographicSize;

        _targetPosition =
            _camera.transform.position;
    }

    private void Update()
    {
        HandleZoom();
        HandleMove();
        SmoothCameraMovement();
    }

    private void HandleZoom()
    {
        float scrollInput =
            MouseManager.ScrollDelta;

        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            float zoomAmount =
                scrollInput *
                _zoomSensitivity *
                _targetZoom;

            _targetZoom -= zoomAmount;

            _targetZoom =
                Mathf.Clamp(
                    _targetZoom,
                    _minZoom,
                    _maxZoom
                );
        }

        _camera.orthographicSize =
            Mathf.Lerp(
                _camera.orthographicSize,
                _targetZoom,
                _zoomSmoothSpeed * Time.deltaTime
            );
    }

    private void HandleMove()
    {
        if (!MouseManager.MiddleButtonPressed)
            return;

        float worldUnitsPerPixel =
            (_camera.orthographicSize * 2f) /
            Screen.height;

        Vector2 movement =
            _moveSensitivity *
            worldUnitsPerPixel *
            MouseManager.MouseScreenPositionDelta;

        _targetPosition -=
            (Vector3)movement;
    }

    private void SmoothCameraMovement()
    {
        _camera.transform.position =
            Vector3.Lerp(
                _camera.transform.position,
                _targetPosition,
                _moveSmoothSpeed * Time.deltaTime
            );
    }
}