namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizOpcode
{
    public EzirizOpcode(int code)
    {
        Code = code;
    }

    public EzirizCode EzirizCode => EzirizCode.Unknown;

    public int Code { get; }

    public override string ToString()
    {
        return $"opcode_{EzirizCode} ({Code})";
    }
}