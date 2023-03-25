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

    public bool HasMethodBody => Instructions.Count > 0;

    public override string ToString()
    {
        return Instructions.Count > 0
            ? "public IList<CilOpCode> Pattern => new List<CilOpCode> {" + $" {string.Join(", ", Instructions.Select(x => $"CilOpCodes.{x.OpCode.Code}"))}" + "};"
            : "None";
    }
}