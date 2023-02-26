using System.Collections.Generic;
using AsmResolver.DotNet;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethodBody
{
    public readonly EzirizMethod Parent;

    public EzirizMethodBody(EzirizMethod parent)
    {
        Parent = parent;

        //Instructions = new List<VmInstruction>();
        Locals = new List<ITypeDescriptor>();
        //ExceptionHandlers = new List<VmExceptionHandler>();
    }

    public List<ITypeDescriptor> Locals
    {
        get;
        set;
    }
}