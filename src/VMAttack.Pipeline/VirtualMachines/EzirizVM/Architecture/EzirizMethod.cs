using AsmResolver.DotNet;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethod
{
    public EzirizMethod(MethodDefinition physicalParent, uint id, ulong methodOffset)
    {
        Id = id;
        MethodOffset = methodOffset;
        PhysicalParent = physicalParent;
        EzirizBody = new EzirizMethodBody(this);
    }

    public uint Id { get; }
    public ulong MethodOffset { get; }

    public MethodDefinition PhysicalParent { get; }
    public EzirizMethodBody EzirizBody { get; }

    public uint ResolvedInstructions
    {
        get
        {
            return (uint) EzirizBody.Instructions.FindAll(x => x.Opcode.IsIdentified).Count;
        }
    }

    public uint MissingInstructions => (uint) EzirizBody.Instructions.Count - ResolvedInstructions;
    public float ResolvedPercentage => ResolvedInstructions / (float) EzirizBody.Instructions.Count;

    public override string ToString() => $"method_{Id:X4}";
}