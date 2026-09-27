using System;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolCode CurrentTool { get; private set; } = ToolCode.Selection;

    public static event Action<ToolCode> OnToolChange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchTool(ToolCode.Selection);


        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchTool(ToolCode.Wire);
    }

    public void SwitchTool(ToolCode code)
    {
        if (CurrentTool == code)
            return;

        CurrentTool = code;
        OnToolChange?.Invoke(CurrentTool);
    }
}