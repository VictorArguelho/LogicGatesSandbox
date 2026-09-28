public static class Conversions
{
    public static DefaultGateCode ItemCodeToGateCode(ItemCode item) =>
        item switch
        {
            ItemCode.GateAnd => DefaultGateCode.And,
            ItemCode.GateOr => DefaultGateCode.Or,
            ItemCode.GateNot => DefaultGateCode.Not,
            ItemCode.GateXor => DefaultGateCode.Xor,
            ItemCode.GateNor => DefaultGateCode.Nor,
            ItemCode.GateNand => DefaultGateCode.Nand,
            ItemCode.GateXnor => DefaultGateCode.Xnor,
            _ => DefaultGateCode.And
        };
}