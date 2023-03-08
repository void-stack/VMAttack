namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizInstruction
{
    public EzirizInstruction(EzirizOpcode code)
        : this(0, code)
    {
    }

    public EzirizInstruction(EzirizOpcode code, object? operand)
        : this(0, code, operand)
    {
    }

    public EzirizInstruction(ulong offset, EzirizOpcode opcode, object? operand = null)
    {
        Offset = offset;
        Opcode = opcode;
        Operand = operand;
    }

    public EzirizOpcode Opcode { get; set; }
    public ulong Offset { get; set; }
    public object? Operand { get; set; }

    public override string ToString()
    {
        string str = $"VM_{Offset:X4}" + ": " + Opcode;
        return Operand != null ? $"{str} Operand: {Operand}" : str;
    }
}