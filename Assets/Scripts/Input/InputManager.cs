using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public KeyCode SelectToolKey { get; set; } = KeyCode.Alpha1;
    public KeyCode CableToolKey { get; set; } = KeyCode.Alpha2;

    public KeyCode PlaceKey { get; set; } = KeyCode.E;
    public KeyCode DeleteKey { get; set; } = KeyCode.Backspace;

    public KeyCode CopyKey { get; set; } = KeyCode.C;
    public KeyCode PasteKey { get; set; } = KeyCode.V;

    public KeyCode ActivateKey { get; set; } = KeyCode.F;

    public event Action OnSwitchSelectTool;
    public event Action OnSwitchCableTool;

    public event Action OnStartPlaceItem;
    public event Action OnConfirmPlaceItem;
    public event Action OnDelete;

    public event Action OnCopy;
    public event Action OnStartPaste;
    public event Action OnConfirmPaste;

    public event Action OnActivateItem;

    private bool _isPlacing;
    private bool _isPasting;

    private void Update()
    {
        HandleTool();
        HandlePlace();
        HandleDelete();
        HandleCopyPaste();
        HandleActivate();
    }

    private void HandleTool()
    {
        if (Input.GetKeyDown(SelectToolKey))
            OnSwitchSelectTool?.Invoke();

        if (Input.GetKeyDown(CableToolKey))
            OnSwitchCableTool?.Invoke();
    }

    private void HandlePlace()
    {
        if (Input.GetKeyDown(PlaceKey) && _isPlacing == false)
        {
            OnStartPlaceItem?.Invoke();
            _isPlacing = true;
        }

        if (Input.GetKeyUp(PlaceKey) && _isPlacing == true)
        {
            OnConfirmPlaceItem?.Invoke();
            _isPlacing = false;
        }
    }

    private void HandleDelete()
    {
        if (Input.GetKeyDown(DeleteKey))
            OnDelete?.Invoke();
    }

    private void HandleCopyPaste()
    {
        if (Input.GetKeyDown(PasteKey) && _isPasting == false)
        {
            OnStartPaste?.Invoke();
            _isPasting = true;
        }

        if (Input.GetKeyUp(PasteKey) && _isPasting == true)
        {
            OnConfirmPaste?.Invoke();
            _isPasting = false;
        }

        if (Input.GetKeyDown(CopyKey))
            OnCopy?.Invoke();
    }

    private void HandleActivate()
    {
        if (Input.GetKeyDown(ActivateKey))
            OnActivateItem?.Invoke();
    }
}