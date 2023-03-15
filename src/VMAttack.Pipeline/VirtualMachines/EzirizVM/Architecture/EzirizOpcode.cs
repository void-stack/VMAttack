using System.Collections.Generic;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizOpcode
{
    public EzirizOpcode(int code) : this(code, new EzirizHandler(new List<CilInstruction>(), null!))
    {
    }

    public EzirizOpcode(int code, EzirizHandler handler)
    {
        Code = code;
        Handler = handler;
    }

    public int Code { get; }

    public static EzirizCode EzirizCode
    {
        get { return EzirizCode.Unknown; }
    }

    public EzirizHandler Handler { get; set; }

    public bool HasHandler
    {
        get { return Handler.Instructions.Count > 0; }
    }

    public override string ToString()
    {
        return $"opcode_{EzirizCode} ({Code})";
    }
}