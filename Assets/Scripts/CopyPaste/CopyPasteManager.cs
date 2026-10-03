using System.Linq;
using UnityEngine;

public class CopyPasteManager : MonoBehaviour
{
    private CircuitRestoreData _clipboard;
    private bool _hasClipboard;

    private void Awake()
    {
        InputManager.Instance.OnCopy += HandleCopy;
        InputManager.Instance.OnConfirmPaste += HandlePaste;
    }

    private void HandleCopy()
    {
        _clipboard = CircuitDataBuilder.Build(
                SelectionManager.SelectedSelectables
                    .Select(s => s.gameObject)
                    .ToArray(),
                MouseManager.MouseWorldPosition
            );

        _hasClipboard = true;
    }

    private void HandlePaste()
    {
        if (_hasClipboard)
        {
            CircuitRestorer.Restore(
                _clipboard,
                MouseManager.MouseWorldPosition
            );
        }
    }
}