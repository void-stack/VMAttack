using System.Collections.Generic;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM;

public static class Helpers
{
    /// <summary>
    ///     Look through some sequence of instructions for a pattern of opcodes, and return all instruction
    ///     subsequences which match the given pattern.
    /// </summary>
    /// <param name="instructions">Instruction sequence to look through</param>
    /// <param name="pattern">OpCode pattern</param>
    /// <returns>All matching instruction subsequences</returns>
    public static IList<CilInstruction[]> FindOpCodePatterns(IList<CilInstruction> instructions, IList<CilCode> pattern)
    {
        var list = new List<CilInstruction[]>();

        for (int i = 0; i < instructions.Count; i++)
        {
            var current = new List<CilInstruction>();

            for (int j = i, k = 0; j < instructions.Count && k < pattern.Count; j++, k++)
            {
                if (instructions[j].OpCode.Code != pattern[k])
                    break;

                current.Add(instructions[j]);
            }

            if (current.Count == pattern.Count)
                list.Add(current.ToArray());
        }

        return list;
    }
}