using System;
using System.Collections.Generic;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizOpcode
{
    public EzirizOpcode(int code) : this(code, new List<CilInstruction>())
    {
    }

    public EzirizOpcode(int code, IList<CilInstruction> handlerPattern)
    {
        Code = code;
        HandlerPattern = handlerPattern;
    }

    public int Code { get; }

    public static EzirizCode EzirizCode
    {
        get { return EzirizCode.Unknown; }
    }

    public IList<CilInstruction>? HandlerPattern { get; set; }

    public bool HasHandlerPattern
    {
        get { return HandlerPattern is { Count: > 0 }; }
    }

    public override string ToString()
    {
        return $"opcode_{EzirizCode} ({Code})";
    }

    public bool HandlerMatchesEntire(CilCode[] codePattern)
    {
        if (codePattern == null)
            throw new ArgumentNullException();

        if (!HasHandlerPattern)
            return false;

        var instructions = Helpers.FindOpCodePatterns(HandlerPattern, codePattern);
        return instructions.Count == 1 && instructions[0].Length == HandlerPattern!.Count;
    }
}