using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolCode CurrentTool { get; private set; } = ToolCode.Drag;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CurrentTool = ToolCode.Drag;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CurrentTool = ToolCode.Wire;
    }
}