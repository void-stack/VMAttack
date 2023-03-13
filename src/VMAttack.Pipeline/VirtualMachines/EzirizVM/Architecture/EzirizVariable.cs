namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizVariable
{
    public EzirizVariable(uint index, EzirizType type, bool isByRef)
    {
        Index = index;
        Type = type;
        IsByRef = isByRef;
    }

    public uint Index { get; }
    public bool IsByRef { get; }
    public EzirizType Type { get; }

    public override string ToString()
    {
        return $"V_{Index:X4} : Type {Type} : IsByRef: {IsByRef}";
    }
}