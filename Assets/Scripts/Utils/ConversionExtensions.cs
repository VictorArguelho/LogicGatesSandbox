public static class ConversionExtensions
{
    public static GateCode ToGateCode(this ItemCode itemCode) =>
        itemCode switch
        {
            ItemCode.GateAnd => GateCode.And,
            ItemCode.GateOr => GateCode.Or,
            ItemCode.GateNot => GateCode.Not,
            ItemCode.GateXor => GateCode.Xor,
            ItemCode.GateNor => GateCode.Nor,
            ItemCode.GateNand => GateCode.Nand,
            ItemCode.GateXnor => GateCode.Xnor,
            _ => GateCode.None
        };

    public static ItemCode ToItemCode(this GateCode gateCode) =>
        gateCode switch
        {
            GateCode.And => ItemCode.GateAnd,
            GateCode.Or => ItemCode.GateOr,
            GateCode.Not => ItemCode.GateNot,
            GateCode.Xor => ItemCode.GateXor,
            GateCode.Nor => ItemCode.GateNor,
            GateCode.Nand => ItemCode.GateNand,
            GateCode.Xnor => ItemCode.GateXnor,
            _ => ItemCode.None
        };
}