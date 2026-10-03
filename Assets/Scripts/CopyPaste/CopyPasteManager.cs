using System;
using System.Linq;

public class CopyPasteManager : Singleton<CopyPasteManager>
{
    public bool HasClipboard { get; private set; }
    public CircuitRestoreData Clipboard { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InputManager.Instance.OnCopy += HandleCopy;
        InputManager.Instance.OnConfirmPaste += HandlePaste;
    }

    private void HandleCopy()
    {
        Clipboard = CircuitDataBuilder.Build(
                SelectionManager.SelectedSelectables
                    .Select(s => s.gameObject)
                    .ToArray(),
                MouseManager.MouseWorldPosition
            );

        HasClipboard = true;
    }

    private void HandlePaste()
    {
        if (HasClipboard)
        {
            CircuitRestorer.Restore(
                Clipboard,
                MouseManager.MouseWorldPosition
            );
        }
    }
}