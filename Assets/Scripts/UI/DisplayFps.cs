using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    private float _fps;
    private float _timer;
    private int _frameCount;

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        _frameCount++;

        if (_timer >= 1f)
        {
            _fps = _frameCount / _timer;

            _timer = 0f;
            _frameCount = 0;
        }
    }

    private void OnGUI()
    {
        GUI.Label(
            new Rect(Screen.width - 100, 10, 90, 20),
            $"FPS: {_fps:F0}"
        );
    }
}