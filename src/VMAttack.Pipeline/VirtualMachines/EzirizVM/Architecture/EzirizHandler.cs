using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public readonly struct EzirizHandler
{
    public IList<CilInstruction> Pattern { get; }


    public EzirizHandler(IList<CilInstruction> pattern)
    {
        Pattern = pattern;
    }

    public override string ToString()
    {
        return Pattern.Count > 0 ? $"{string.Join(", ", Pattern.Select(x => $"CilCode.{x.OpCode.Code}"))}" : "None";
    }

    public bool MatchesEntire(CilCode[] codePattern)
    {
        if (codePattern == null)
            throw new ArgumentNullException();

        if (Pattern is null)
            return false;

        var instructions = Helpers.FindOpCodePatterns(Pattern, codePattern);
        return instructions.Count == 1 && instructions[0].Length == Pattern.Count;
    }
}