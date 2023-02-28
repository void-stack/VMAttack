using System.Collections.Generic;
using AsmResolver.DotNet;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethodBody
{
    public EzirizMethodBody(EzirizMethod parent)
    {
        Parent = parent;

        Locals = new List<ITypeDescriptor>();
        Instructions = new List<EzirizInstruction>();
        ExceptionHandlers = new List<EzirizException>();
    }

    public EzirizMethod Parent { get; }

    public List<EzirizException> ExceptionHandlers { get; }

    public List<EzirizInstruction> Instructions { get; }

    public List<ITypeDescriptor> Locals { get; }

    public override string ToString()
    {
        return
            $"body_{Parent.Id:X4} with {Instructions.Count} instructions and {Locals.Count} locals and {ExceptionHandlers.Count} exception handlers";
    }
}