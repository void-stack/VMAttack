namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizInstruction
{
    public EzirizInstruction(EzirizOpCode opCode)
        : this(0, opCode)
    {
    }

    public EzirizInstruction(EzirizOpCode opCode, object? operand)
        : this(0, opCode, operand)
    {
    }

    public EzirizInstruction(int offset, EzirizOpCode opCode, object? operand = null)
    {
        Offset = offset;
        OpCode = opCode;
        Operand = operand;
    }

    public EzirizOpCode OpCode { get; set; }
    public int Offset { get; set; }
    public object? Operand { get; set; }

    public override string ToString()
    {
        string str = $"VM_{Offset:X4}" + ": " + OpCode;
        return Operand != null ? $"{str} {Operand}" : str;
    }
}