using System;
using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching;

/// <summary>
///     Thanks to https://github.com/puff and Krypton project for the patterns.
/// </summary>
internal class PatternMatcher
{
    private static PatternMatcher? _instance;
    private readonly List<IOpCodePattern?> _opCodePatterns;

    private readonly Dictionary<int, EzirizOpcode> _opCodes;

    private PatternMatcher()
    {
        _opCodes = new Dictionary<int, EzirizOpcode>();
        _opCodePatterns = new List<IOpCodePattern?>();
        foreach (var type in typeof(PatternMatcher).Assembly.GetTypes())
            if (type.GetInterface(nameof(IOpCodePattern)) != null)
                if (Activator.CreateInstance(type) is IOpCodePattern instance)
                    _opCodePatterns.Add(instance);
    }

    public static PatternMatcher GetInstance()
    {
        if (_instance == null)
            _instance = new PatternMatcher();

        return _instance;
    }

    public void SetOpCodeValue(int value, EzirizOpcode opCode) => _opCodes[value] = opCode;
    public EzirizOpcode GetCreateOpCodeValue(int value) => _opCodes.TryGetValue(value, out var opc) ? opc : EzirizOpcode.DefaultNopOpCode;

    public IOpCodePattern? FindOpCode(EzirizOpcode vmOpCode, int index = 0)
    {
        if (!vmOpCode.Handler.HasInstructions)
            throw new Exception("Handler has no instructions!");

        foreach (var pat in _opCodePatterns)
        {
            if (pat!.MatchEntireBody
                    ? !MatchesEntire(pat, vmOpCode.Handler.Instructions, index) || !pat.Verify(vmOpCode.Handler.Instructions, index)
                    : GetAllMatchingInstructions(pat, vmOpCode, index).Count <= 0)
                continue;

            if (!pat.AllowMultiple)
                _opCodePatterns.Remove(pat);
            return pat;
        }

        return null;
    }

    /// <summary>
    ///     Checks if pattern matches a handler's instructions body
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against</param>
    /// <param name="opcode"></param>
    /// <param name="index">Index of handler's instruction body to start matching at</param>
    /// <returns>Whether the pattern matches handler's instruction body</returns>
    public static bool MatchesPattern(IOpCodePattern pattern, EzirizOpcode opcode, int index = 0)
    {
        if (!opcode.Handler.HasInstructions) return false;

        return pattern.MatchEntireBody
            ? MatchesEntire(pattern, opcode.Handler.Instructions, index) &&
              pattern.Verify(opcode.Handler.Instructions, index)
            : GetAllMatchingInstructions(pattern, opcode, index).Count > 0;
    }


    /// <summary>
    ///     Checks if pattern matches a handler's instructions body.
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against.</param>
    /// <param name="instructions">Instructions to match body against</param>
    /// <param name="index">Index of the instructions collection to start matching at</param>
    /// <returns>Whether the pattern matches the given instructions</returns>
    public static bool MatchesPattern(IPattern pattern, IList<CilInstruction> instructions, int index = 0) =>
        pattern.MatchEntireBody
            ? MatchesEntire(pattern, instructions, index) &&
              pattern.Verify(instructions, index)
            : GetAllMatchingInstructions(pattern, instructions, index).Count > 0;

    /// <summary>
    ///     Checks if pattern matches a method's instructions body
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against</param>
    /// <param name="method">Method to match body against</param>
    /// <param name="index">Index of method's instruction body to start matching at</param>
    /// <returns>Whether the pattern matches method's instruction body</returns>
    public static bool MatchesPattern(IPattern pattern, MethodDefinition? method, int index = 0)
    {
        if (!(method?.HasMethodBody).GetValueOrDefault()) return false;

        return pattern.MatchEntireBody
            ? MatchesEntire(pattern, method!.CilMethodBody!.Instructions, index) &&
              pattern.Verify(method, index)
            : GetAllMatchingInstructions(pattern, method!, index).Count > 0;
    }

    private static bool CanInterchange(IPattern pat, CilInstruction ins, CilOpCode patOpCode)
    {
        var patIns = new CilInstruction(patOpCode);
        if (ins.IsLdcI4())
            return pat.InterchangeLdcI4OpCodes && patIns.IsLdcI4();

        if (ins.IsLdloc())
            return pat.InterchangeLdlocOpCodes && patIns.IsLdloc();

        if (ins.IsStloc())
            return pat.InterchangeStlocOpCodes && patIns.IsStloc();

        return false;
    }

    private static bool MatchesEntire(IPattern pattern, IList<CilInstruction> instructions, int index)
    {
        var pat = pattern.Pattern;
        if (index + pat.Count > instructions.Count || pattern.MatchEntireBody && pat.Count != instructions.Count) return false;

        for (int i = 0; i < pat.Count; i++)
        {
            if (pat[i] == CilOpCodes.Nop)
                continue;

            var instruction = instructions[i + index];
            if (instructions[i + index].OpCode != pat[i] && !CanInterchange(pattern, instruction, pat[i]))
                return false;
        }

        return true;
    }

    /// <summary>
    ///     Gets all matching instruction sets in a method's instructions body.
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against.</param>
    /// <param name="method">Method to match pattern against</param>
    /// <param name="index">Index of method's instruction body to start matching at.</param>
    /// <returns>List of matching instruction sets</returns>
    public static List<CilInstruction[]> GetAllMatchingInstructions(IPattern pattern, MethodDefinition method, int index = 0)
    {
        if (!method.HasMethodBody) return new List<CilInstruction[]>();
        var instructions = method.CilMethodBody!.Instructions;

        var pat = pattern.Pattern;
        if (index + pat.Count > instructions.Count) return new List<CilInstruction[]>();

        var matchingInstructions = new List<CilInstruction[]>();
        for (int i = index; i < instructions.Count; i++)
        {
            var current = new List<CilInstruction>();

            for (int j = i, k = 0; j < instructions.Count && k < pat.Count; j++, k++)
            {
                var instruction = instructions[j];
                if (instruction.OpCode != pat[k] && !CanInterchange(pattern, instruction, pat[k]))
                    break;
                current.Add(instructions[j]);
            }

            if (current.Count == pat.Count && pattern.Verify(method, index + i))
                matchingInstructions.Add(current.ToArray());
        }

        return matchingInstructions;
    }

    /// <summary>
    ///     Gets all matching instruction sets in a handler's instructions body.
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against.</param>
    /// <param name="instructions">CIL instruction body to match pattern against</param>
    /// <param name="index">Index of handler's instruction body to start matching at.</param>
    /// <returns>List of matching instruction sets</returns>
    public static List<CilInstruction[]> GetAllMatchingInstructions(IPattern pattern, IList<CilInstruction> instructions, int index = 0)
    {
        var pat = pattern.Pattern;
        if (index + pat.Count > instructions.Count) return new List<CilInstruction[]>();

        var matchingInstructions = new List<CilInstruction[]>();
        for (int i = index; i < instructions.Count; i++)
        {
            var current = new List<CilInstruction>();

            for (int j = i, k = 0; j < instructions.Count && k < pat.Count; j++, k++)
            {
                var instruction = instructions[j];
                if (instruction.OpCode != pat[k] && !CanInterchange(pattern, instruction, pat[k]))
                    break;
                current.Add(instructions[j]);
            }

            if (current.Count == pat.Count && pattern.Verify(instructions, index + i))
                matchingInstructions.Add(current.ToArray());
        }

        return matchingInstructions;
    }

    /// <summary>
    ///     Gets all matching instruction sets in a handler's instructions body.
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against.</param>
    /// <param name="handler">Method to match pattern against</param>
    /// <param name="index">Index of handler's instruction body to start matching at.</param>
    /// <returns>List of matching instruction sets</returns>
    public static List<CilInstruction[]> GetAllMatchingInstructions(IPattern pattern, EzirizHandler handler, int index = 0)
    {
        if (!handler.HasInstructions) return new List<CilInstruction[]>();
        var instructions = handler.Instructions;

        var pat = pattern.Pattern;
        if (index + pat.Count > instructions.Count) return new List<CilInstruction[]>();

        var matchingInstructions = new List<CilInstruction[]>();
        for (int i = index; i < instructions.Count; i++)
        {
            var current = new List<CilInstruction>();

            for (int j = i, k = 0; j < instructions.Count && k < pat.Count; j++, k++)
            {
                var instruction = instructions[j];
                if (instruction.OpCode != pat[k] && !CanInterchange(pattern, instruction, pat[k]))
                    break;
                current.Add(instructions[j]);
            }

            if (current.Count == pat.Count && pattern.Verify(handler, index + i))
                matchingInstructions.Add(current.ToArray());
        }

        return matchingInstructions;
    }

    /// <summary>
    ///     Gets all matching instruction sets in a vm opcode delegate handler instructions body.
    /// </summary>
    /// <param name="pattern">Pattern to match instructions against.</param>
    /// <param name="vmOpCode">VM Opcode to match pattern against</param>
    /// <param name="index">Index of handler's instruction body to start matching at.</param>
    /// <returns>List of matching instruction sets</returns>
    public static List<CilInstruction[]> GetAllMatchingInstructions(IOpCodePattern pattern, EzirizOpcode vmOpCode, int index = 0)
    {
        if (!vmOpCode.Handler.HasInstructions) return new List<CilInstruction[]>();
        var instructions = vmOpCode.Handler.Instructions;

        var pat = pattern.Pattern;
        if (index + pat.Count > instructions.Count) return new List<CilInstruction[]>();

        var matchingInstructions = new List<CilInstruction[]>();
        for (int i = index; i < instructions.Count; i++)
        {
            var current = new List<CilInstruction>();

            for (int j = i, k = 0; j < instructions.Count && k < pat.Count; j++, k++)
            {
                var instruction = instructions[j];
                if (instruction.OpCode != pat[k] && !CanInterchange(pattern, instruction, pat[k]))
                    break;
                current.Add(instructions[j]);
            }

            if (current.Count == pat.Count && pattern.Verify(vmOpCode.Handler))
                matchingInstructions.Add(current.ToArray());
        }

        return matchingInstructions;
    }
}