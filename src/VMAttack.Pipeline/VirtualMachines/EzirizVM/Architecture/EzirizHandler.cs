using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public readonly partial struct EzirizHandler
{
    public MethodDefinition? Method { get; }
    public IList<CilInstruction> Instructions { get; }

    public EzirizHandler(IList<CilInstruction> instructions, MethodDefinition? method)
    {
        Instructions = instructions;
        Method = method;
    }

    public override string ToString()
    {
        return Instructions.Count > 0
            ? $"{string.Join(", ", Instructions.Select(x => $"CilCode.{x.OpCode.Code}"))}"
            : "None";
    }
}

public readonly partial struct EzirizHandler
{
    public bool MatchesEntire(CilCode[] codePattern)
    {
        if (codePattern == null)
            throw new ArgumentNullException();

        if (Instructions is null)
            return false;

        var instructions = Helpers.FindOpCodePatterns(Instructions, codePattern);
        return instructions.Count == 1 && instructions[0].Length == Instructions.Count;
    }

    public IList<CilCode> ModifyPattern(IList<CilCode> pattern, CilCode oldCode, CilCode newCode)
    {
        var result = new CilCode[pattern.Count];
        for (int i = 0; i < result.Length; i++)
            if (pattern[i] == oldCode)
                result[i] = newCode;

            else result[i] = pattern[i];
        return result;
    }
}