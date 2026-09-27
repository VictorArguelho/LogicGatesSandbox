public static class DefaultGateOperators
{
    public static void And(Port inA, Port inB, Port outPort) =>
        outPort.SetSignal(inA.Signal && inB.Signal);

    public static void Or(Port inA, Port inB, Port outPort) =>
        outPort.SetSignal(inA.Signal || inB.Signal);

    public static void Xor(Port inA, Port inB, Port outPort) =>
        outPort.SetSignal(inA.Signal ^ inB.Signal);

    public static void Nor(Port inA, Port inB, Port outPort) =>
        outPort.SetSignal(!(inA.Signal || inB.Signal));

    public static void Nand(Port inA, Port inB, Port outPort) =>
        outPort.SetSignal(!(inA.Signal && inB.Signal));
}