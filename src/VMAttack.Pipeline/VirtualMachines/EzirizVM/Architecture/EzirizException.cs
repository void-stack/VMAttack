using AsmResolver.DotNet;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public struct EzirizException
{
    public ITypeDescriptor CatchType { get; set; }
    public EzirizEhType EhType { get; set; }

    public int Filter { get; set; }
    public int HandlerEnd { get; set; }
    public int HandlerStart { get; set; }
    public int TryEnd { get; set; }
    public int TryStart { get; set; }

    public override string ToString() =>
        $"TryStart: {TryStart}, TryEnd: {TryEnd}, HandlerStart: {HandlerStart}, HandlerEnd: {HandlerEnd}, EhType: {EhType}, CatchType: {CatchType}, Filter: {Filter}";
}