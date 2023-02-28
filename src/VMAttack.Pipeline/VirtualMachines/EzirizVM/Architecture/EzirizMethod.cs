using AsmResolver.DotNet;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethod
{
    public EzirizMethod(MethodDefinition? cilMethod, uint id, ulong methodOffset)
    {
        Id = id;
        MethodOffset = methodOffset;
        CilMethod = cilMethod;

        EzirizBody = new EzirizMethodBody(this);
    }

    public uint Id { get; }
    public ulong MethodOffset { get; }
    public MethodDefinition? CilMethod { get; }
    public EzirizMethodBody EzirizBody { get; }

    public override string ToString()
    {
        return $"method_{Id:X4}";
    }
}