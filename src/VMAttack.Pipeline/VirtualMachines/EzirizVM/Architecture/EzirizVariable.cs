namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizVariable
{
    public EzirizVariable(uint index, EzirizElement element, bool isByRef)
    {
        Index = index;
        Element = element;
        IsByRef = isByRef;
    }

    public uint Index { get; }
    public bool IsByRef { get; }
    public EzirizElement Element { get; }

    public override string ToString() => $"V_{Index} : Element {Element} : IsByRef: {IsByRef}";
}