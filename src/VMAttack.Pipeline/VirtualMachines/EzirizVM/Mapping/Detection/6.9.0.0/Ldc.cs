using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    private static readonly CilCode[] PatternPush =
    {
        CilCode.Ldarg_0,
        CilCode.Ldfld,
        CilCode.Ldarg_0,
        CilCode.Ldfld,
        CilCode.Unbox_Any, // cast to (int) that's how we know it's a Int32
        CilCode.Newobj,
        CilCode.Callvirt,
        CilCode.Ret
    };

    [DetectV1(CilCode.Ldc_I4)]
    public static bool Is_LdcI4Pattern(this EzirizOpcode code)
    {
        var handler = code.Handler;
        var instructions = handler.Instructions;

        if (handler.MatchesEntire(PatternPush))
        {
            if (instructions[4].Operand is not TypeReference type)
                return false;

            var module = type.Module;
            var corLibTypeFactory = module?.CorLibTypeFactory;

            if (type.ToTypeSignature() == corLibTypeFactory?.Int32)
                return true;
        }

        return false;
    }
}