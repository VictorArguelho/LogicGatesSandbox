using System;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolCode CurrentTool { get; private set; } = ToolCode.Selection;

    public static event Action<ToolCode> OnToolChange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentTool = ToolCode.Selection;
            OnToolChange?.Invoke(CurrentTool);
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentTool = ToolCode.Wire;
            OnToolChange?.Invoke(CurrentTool);
        }
    }
}