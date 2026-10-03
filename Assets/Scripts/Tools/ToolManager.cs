using System;

public class ToolManager : Singleton<ToolManager>
{
    public ToolCode CurrentTool { get; private set; } = ToolCode.Selection;

    public event Action<ToolCode> OnToolChange;

    protected override void Awake()
    {
        base.Awake();

        InputManager.Instance.OnSwitchSelectTool += () => SwitchTool(ToolCode.Selection);
        InputManager.Instance.OnSwitchCableTool += () => SwitchTool(ToolCode.Wire);
    }

    private void SwitchTool(ToolCode code)
    {
        if (CurrentTool == code)
            return;

        CurrentTool = code;
        OnToolChange?.Invoke(CurrentTool);
    }
}