using System.Collections.Generic;
using UnityEngine;

public class ClipboardManager : MonoBehaviour
{
    private static readonly List<ClipboardItemData> _clipboardItemsData = new();
    private static readonly List<PortData> _clipboardPortsData = new();
    private static readonly List<ClipboardToggablePortData> _clipboardToggablePortsData = new();
    private static readonly List<ClipboardPortWireData> _clipboardPortWiresData = new();

    public static IReadOnlyList<ClipboardItemData> ClipboardItemsData =>
        _clipboardItemsData;

    public static IReadOnlyList<PortData> ClipboardPortsData =>
        _clipboardPortsData;

    public static IReadOnlyList<ClipboardToggablePortData> ClipboardToggablePortsData =>
        _clipboardToggablePortsData;

    public static IReadOnlyList<ClipboardPortWireData> ClipboardPortWiresData =>
        _clipboardPortWiresData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            Copy();
    }

    private void Copy()
    {
        _clipboardItemsData.Clear();
        _clipboardPortsData.Clear();
        _clipboardToggablePortsData.Clear();
        _clipboardPortWiresData.Clear();

        foreach (var selectable in SelectionManager.SelectedSelectables)
        {
            if (selectable.TryGetComponent<SpawnedItem>(out var spawnedInfo))
            {
                if (spawnedInfo.TryGetComponent<ToggablePort>(out var toggablePort))
                {
                    CopyToggablePort(toggablePort, spawnedInfo);
                    continue;
                }

                CopyItem(spawnedInfo);
            }
            else if (selectable.TryGetComponent<Port>(out var port))
                CopyPort(port);
            else if (selectable.transform.parent.TryGetComponent<PortWire>(out var wire))
                CopyPortWire(wire);
        }
    }

    private void CopyItem(SpawnedItem item) =>
        _clipboardItemsData.Add(GetItemClipboardData(item));

    private void CopyPort(Port port) =>
        _clipboardPortsData.Add(port.GetData());

    private void CopyToggablePort(ToggablePort port, SpawnedItem item) =>
        _clipboardToggablePortsData.Add(new(
            GetItemClipboardData(item),
            port.Signal
        ));

    private void CopyPortWire(PortWire wire) =>
        _clipboardPortWiresData.Add(new(
            wire.OutPort.GetData(),
            wire.InPort.GetData()
        ));    

    private ClipboardItemData GetItemClipboardData(SpawnedItem item)
    {
        return new(
            item.Id,
            MouseManager.GriddedMouseWorldPosition - (Vector2)item.transform.position,
            item.Code,
            item.Category
        );
    }  
}