using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public record EzirizOpcode(EzirizHandler Handler)
{
    public EzirizHandler Handler { get; } = Handler;
    public static EzirizOpcode DefaultNopOpCode { get; } = new EzirizOpcode(new EzirizHandler());

    public bool IsIdentified { get; set; } = false;
    public bool HasVirtualCode { get; set; } = false;
    public bool HasHandler { get; set; } = false;
    public int? VirtualCode { get; set; } = null!;

    public CilOpCode CilOpCode { get; set; } = CilOpCodes.Nop;
    public override string ToString() => $"CilOpCode: {CilOpCode} ({VirtualCode})";
}