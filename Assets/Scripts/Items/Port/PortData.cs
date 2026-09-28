public readonly struct PortData
{
    public readonly uint Id { get; }
    public readonly bool IsFromGate { get; }
    public readonly PortGateCode GateCode { get; }

    public PortData(Port port)
    {
        IsFromGate = !port.TryGetComponent<SpawnedItem>(out var spawnedInfo);

        if (IsFromGate)
        {
            var gate = port.transform.parent;
            Id = gate.GetComponent<SpawnedItem>().Id;
            GateCode = gate.GetComponent<DefaultGate>().GetPortCode(port);
        }
        else
        {
            Id = spawnedInfo.Id;
            GateCode = (PortGateCode)(-1);
        }
    }
}