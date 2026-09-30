using System.Linq;
using UnityEngine;

public class CopyPasteManager : MonoBehaviour
{
    private CircuitRestoreData _clipboard;
    private bool _hasClipboard;

    private void Update()
    {
        HandleCopy();
        HandlePaste();
    }

    private void HandleCopy()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _clipboard = CircuitDataBuilder.Build(
                SelectionManager.SelectedSelectables
                    .Select(s => s.gameObject)
                    .ToArray(),
                MouseManager.GriddedMouseWorldPosition
            );

            _hasClipboard = true;
        }
    }

    private void HandlePaste()
    {
        if (Input.GetKeyDown(KeyCode.V) && _hasClipboard)
        {
            CircuitRestorer.Restore(
                _clipboard,
                MouseManager.GriddedMouseWorldPosition
            );
        }
    }
}