using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    private static readonly CilCode[] ArithmeticPattern =
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
        CilCode.Callvirt, // add or mul etc... [20]
        CilCode.Callvirt,
        CilCode.Ret
    };

    [DetectV1(CilCode.Mul)]
    public static bool Is_MulPattern(this EzirizOpcode ins)
    {
        bool isArithmetic = ins.Handler.MatchesEntire(ArithmeticPattern);

        if (isArithmetic)
        {
            var operation = ins.Handler.Pattern[20].Operand as MethodDefinition;
            // get echo to determine if it is a mul Arithmetic operation
        }

        return false;
    }
}