using System.Collections.Generic;
using System.Linq;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public readonly struct EzirizHandler
{
    public IList<CilInstruction> Instructions { get; }

    public EzirizHandler(IList<CilInstruction> instructions)
    {
        Instructions = instructions;
    }

    public bool HasInstructions => Instructions.Count > 0;

    public override string ToString()
    {
        return Instructions.Count > 0
            ? $"{string.Join(", ", Instructions.Select(x => $"CilCode.{x.OpCode.Code}"))}"
            : "None";
    }
}