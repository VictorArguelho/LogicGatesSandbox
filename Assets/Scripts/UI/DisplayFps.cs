using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    private float _fps;
    private float _timer;

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;

        if (_timer >= 1f)
        {
            _fps = 1f / Time.unscaledDeltaTime;
            _timer = 0f;
        }
    }

    private void OnGUI()
    {
        var text = $"FPS: {_fps:F0}";
        var size = GUI.skin.label.CalcSize(new GUIContent(text));

        GUI.Label(
            new Rect(
                Screen.width - size.x - 10,
                10,
                size.x,
                size.y
            ),
            text
        );
    }
}