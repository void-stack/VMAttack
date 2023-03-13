using System.Collections.Generic;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethodBody
{
    public EzirizMethodBody(EzirizMethod parent)
    {
        Parent = parent;

        Locals = new List<EzirizVariable>();
        Instructions = new List<EzirizInstruction>();
        ExceptionHandlers = new List<EzirizException>();
    }

    public EzirizMethod Parent { get; }

    public List<EzirizException> ExceptionHandlers { get; }

    public List<EzirizInstruction> Instructions { get; }

    public List<EzirizVariable> Locals { get; }
}