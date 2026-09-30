using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    private Camera _camera;

    private static Vector2 _lastScreenPosition;
    private static Vector2 _currentScreenPosition;

    private static Vector2 _lastWorldPosition;
    private static Vector2 _currentWorldPosition;

    public static event Action<MouseButtonCode> OnButtonDown;
    public static event Action<MouseButtonCode> OnButtonUp;
    public static event Action<MouseButtonCode> OnButtonPressed;

    public static Vector2 MouseScreenPosition =>
        _currentScreenPosition;
    public static Vector2 MouseScreenPositionDelta =>
        _currentScreenPosition - _lastScreenPosition;

    public static Vector2 MouseWorldPosition =>
        _currentWorldPosition;
    public static Vector2 MouseWorldPositionDelta =>
        _currentWorldPosition - _lastWorldPosition;

    public static Vector2 GriddedMouseWorldPosition => new Vector2(
                Mathf.Floor(MouseWorldPosition.x / 1f) * 1f,
                Mathf.Floor(MouseWorldPosition.y / 1f) * 1f
            ) + Vector2.one / 2f;

    public static bool IsPointerOverUI =>
        EventSystem.current != null &&
        EventSystem.current.IsPointerOverGameObject();

    public static bool LeftButtonDown => Input.GetMouseButtonDown(0);
    public static bool RightButtonDown => Input.GetMouseButtonDown(1);
    public static bool MiddleButtonDown => Input.GetMouseButtonDown(2);

    public static bool LeftButtonUp => Input.GetMouseButtonUp(0);
    public static bool RightButtonUp => Input.GetMouseButtonUp(1);
    public static bool MiddleButtonUp => Input.GetMouseButtonUp(2);

    public static bool LeftButtonPressed => Input.GetMouseButton(0);
    public static bool RightButtonPressed => Input.GetMouseButton(1);
    public static bool MiddleButtonPressed => Input.GetMouseButton(2);

    public static float ScrollDelta => Input.GetAxis("Mouse ScrollWheel");

    private void Awake() =>
        _camera = Camera.main;

    private void Update()
    {
        UpdateMousePosition();
        UpdateMouseButtons();
    }

    private void UpdateMousePosition()
    {
        _lastScreenPosition = _currentScreenPosition;
        _currentScreenPosition = Input.mousePosition;

        _lastWorldPosition = _currentWorldPosition;
        _currentWorldPosition =
            _camera.ScreenToWorldPoint(_currentScreenPosition);
    }

    private void UpdateMouseButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            var button = (MouseButtonCode)i;

            if (Input.GetMouseButtonDown(i))
                OnButtonDown?.Invoke(button);

            if (Input.GetMouseButtonUp(i))
                OnButtonUp?.Invoke(button);

            if (Input.GetMouseButton(i))
                OnButtonPressed?.Invoke(button);
        }
    }
}