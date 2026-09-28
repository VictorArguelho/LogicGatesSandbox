public readonly struct ClipboardPortWireData
{
    public readonly PortData OutPortData { get; }
    public readonly PortData InPortData { get; }

    public ClipboardPortWireData(PortData outPortData, PortData inPortData)
    {
        OutPortData = outPortData;
        InPortData = inPortData;
    }
}