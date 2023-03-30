using System.Collections.Generic;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethodBody
{
    public EzirizMethodBody(EzirizMethod virtualParent)
    {
        VirtualParent = virtualParent;

        Variables = new List<EzirizVariable>();
        Instructions = new List<EzirizInstruction>();
        ExceptionHandlers = new List<EzirizException>();
    }

    public EzirizMethod VirtualParent { get; }

    public List<EzirizException> ExceptionHandlers { get; }
    public List<EzirizInstruction> Instructions { get; }
    public List<EzirizVariable> Variables { get; }
    public bool FullyIdentified => Instructions.TrueForAll(x => x.Opcode.IsIdentified);
    public bool Decompiled { get; set; } = false;
}