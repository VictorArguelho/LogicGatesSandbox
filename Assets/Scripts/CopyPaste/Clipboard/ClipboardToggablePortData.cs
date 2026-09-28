public readonly struct ClipboardToggablePortData
{
    public readonly ClipboardItemData ItemData { get; }
    public readonly bool Signal { get; }

    public ClipboardToggablePortData(ClipboardItemData itemData, bool isActive)
    {
        ItemData = itemData;
        Signal = isActive;
    }
}