using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    private static readonly CilCode[] PatternArithmetic =
    {
        CilCode.Ldarg_0,
        CilCode.Ldfld,
        CilCode.Callvirt,
        CilCode.Call,
        CilCode.Stloc_S,
        CilCode.Ldarg_0,
        CilCode.Ldfld,
        CilCode.Callvirt,
        CilCode.Call,
        CilCode.Stloc_S,
        CilCode.Ldloc_S,
        CilCode.Brfalse_S,
        CilCode.Newobj,
        CilCode.Throw,
        CilCode.Ldloc_S,
        CilCode.Brfalse_S,
        CilCode.Ldarg_0,
        CilCode.Ldfld,
        CilCode.Ldloc_S,
        CilCode.Ldloc_S,
        CilCode.Callvirt, //  CilCode.Callvirt has a override with a pattern [AddPattern, MulPattern, etc] to determine the instruction
        CilCode.Callvirt,
        CilCode.Ret
    };

    private static readonly CilCode[] AddPattern =
    {
        CilCode.Ldarg_1, CilCode.Castclass,
        CilCode.Ldflda, CilCode.Ldfld,
        CilCode.Add, CilCode.Newobj,
        CilCode.Ret
    };

    private static readonly CilCode[] MulPattern =
    {
        CilCode.Ldarg_1, CilCode.Castclass,
        CilCode.Ldflda, CilCode.Ldfld,
        CilCode.Mul, CilCode.Newobj,
        CilCode.Ret
    };

    [DetectV1(CilCode.Mul)]
    public static bool Is_MulPattern(this EzirizOpcode opcode)
    {
        var handler = opcode.Handler;
        var instructions = handler.Instructions;

        if (handler.MatchesEntire(PatternArithmetic))
        {
            if (instructions[20].Operand is not MethodDefinition method)
                return false;

            return handler.HasIndirectOverride(method, MulPattern);
        }

        return false;
    }

    [DetectV1(CilCode.Add)]
    public static bool Is_AddPattern(this EzirizOpcode opcode)
    {
        var handler = opcode.Handler;
        var instructions = handler.Instructions;

        if (handler.MatchesEntire(PatternArithmetic))
        {
            if (instructions[20].Operand is not MethodDefinition method)
                return false;

            return handler.HasIndirectOverride(method, AddPattern);
        }

        return false;
    }
}