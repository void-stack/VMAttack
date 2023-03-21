using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Brfalse)]
    public static bool Is_BrfalsePattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldc_I4_0, CilCode.Stloc_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Stloc_S,
            CilCode.Ldloc_S, CilCode.Brtrue_S, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Ldc_I4_0, CilCode.Ceq,
            CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ret, CilCode.Ldarg_0, CilCode.Ldarg_0,
            CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Ldc_I4_1, CilCode.Sub, CilCode.Stfld, CilCode.Ldc_I4_1,
            CilCode.Stloc_S, CilCode.Br_S
        });
    }
}