using System.Linq;
using UnityEngine;

public class CircuitSaveManager : MonoBehaviour
{
    private bool _isSaving;
    private CircuitRestoreData _circuitToSave;

    private void Awake()
    {
        InputManager.Instance.OnSave += HandleSave;
        CircuitSavePanel.Instance.OnConfirmSave += HandleConfirmSave;
    }

    private void HandleSave()
    {
        if (_isSaving)
            return;

        _circuitToSave = CircuitDataBuilder.Build(
                SelectionManager.SelectedSelectables
                    .Select(s => s.gameObject)
                    .ToArray(),
                MouseManager.MouseWorldPosition
            );

        _isSaving = true;

        CircuitSavePanel.Instance.Show();
    }

    private void HandleConfirmSave(string name, string description, string imagePath)
    {
        if (_isSaving)
        {
            var circuitData = new CircuitData(_circuitToSave, ItemCategoryCode.Custom, name, description, name);
            var saveResult = CircuitSaver.Save(circuitData, imagePath);

            if (saveResult.Succeeded)
            {
                SelectionMenu.Instance.AddItem(new(circuitData));
                CircuitLoadManager.Instance.RegisterCircuit(saveResult.DirectoryName);
            }

            _isSaving = false;
        }
    }
}