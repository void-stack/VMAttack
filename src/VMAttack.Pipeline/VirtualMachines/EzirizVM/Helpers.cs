using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

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

    public static bool GetMethodOverwrites(this MethodDefinition method, out List<MethodDefinition> overwrites)
    {
        var module = method.Module;

        if (module is null)
            throw new ArgumentNullException(nameof(module));

        var types = module.GetAllTypes();

        overwrites = new List<MethodDefinition>();

        foreach (var type in types)
        foreach (var vMethod in type.Methods.Where(m => m.IsVirtual))
        {
            if (vMethod.CilMethodBody is null)
                continue;

            if (vMethod.Name == method.Name)
                overwrites.Add(vMethod);
        }

        return overwrites.Count > 0;
    }

    public static bool FindMatchInTargets(IList<MethodDefinition>? targetMethods, IList<CilCode> codePattern)
    {
        if (targetMethods == null)
            return false;

        foreach (var method in targetMethods)
        {
            if (method.CilMethodBody is null)
                continue;

            if (FindOpCodePatterns(method.CilMethodBody.Instructions, codePattern).Count > 0)
                return true;
        }

        return false;
    }

    public static bool HasIndirectOverride(this EzirizHandler handler, MethodDefinition method, IList<CilCode> pattern)
    {
        return method.GetMethodOverwrites(out var overrides) && FindMatchInTargets(overrides, pattern);
    }
}