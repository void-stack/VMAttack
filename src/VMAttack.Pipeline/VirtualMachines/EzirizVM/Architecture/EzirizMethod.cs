using AsmResolver.DotNet;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethod
{
    public EzirizMethod(MethodDefinition parent, uint id, ulong methodOffset)
    {
        Id = id;
        MethodOffset = methodOffset;
        Parent = parent;
        EzirizBody = new EzirizMethodBody(this);
    }

    public uint Id { get; }
    public ulong MethodOffset { get; }

    public MethodDefinition Parent { get; }
    public EzirizMethodBody EzirizBody { get; }
    public override string ToString() => $"method_{Id:X4}";
}