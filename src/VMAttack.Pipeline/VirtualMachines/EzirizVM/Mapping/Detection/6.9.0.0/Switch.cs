using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Switch)]
    public static bool Is_SwitchPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Castclass,
            CilCode.Stloc_S,
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Callvirt,
            CilCode.Call,
            CilCode.Stloc_S,
            CilCode.Ldloc_S,
            CilCode.Callvirt,
            CilCode.Ldflda,
            CilCode.Ldfld,
            CilCode.Stloc_S,
            CilCode.Ldloc_S,
            CilCode.Ldc_I8,
            CilCode.Blt_S,
            CilCode.Ldloc_S,
            CilCode.Callvirt,
            CilCode.Brfalse_S,
            CilCode.Call,
            CilCode.Ldc_I4_4,
            CilCode.Bne_Un_S,
            CilCode.Ldloc_S,
            CilCode.Conv_I4,
            CilCode.Conv_I8,
            CilCode.Stloc_S,
            CilCode.Ldloc_S,
            CilCode.Callvirt,
            CilCode.Brfalse_S,
            CilCode.Ldloc_S,
            CilCode.Castclass,
            CilCode.Stloc_S,
            CilCode.Ldloc_S,
            CilCode.Ldfld,
            CilCode.Ldc_I4_6,
            CilCode.Bne_Un_S,
            CilCode.Ldloc_S,
            CilCode.Ldflda,
            CilCode.Ldfld,
            CilCode.Conv_U8,
            CilCode.Stloc_S,
            CilCode.Ldloc_S,
            CilCode.Ldloc_S,
            CilCode.Ldlen,
            CilCode.Conv_I4,
            CilCode.Conv_I8,
            CilCode.Bge_S,
            CilCode.Ldloc_S,
            CilCode.Ldc_I8,
            CilCode.Blt_S,
            CilCode.Ldarg_0,
            CilCode.Ldloc_S,
            CilCode.Ldloc_S,
            CilCode.Conv_Ovf_I,
            CilCode.Ldelem_I4,
            CilCode.Ldc_I4_1,
            CilCode.Sub,
            CilCode.Stfld,
            CilCode.Ret,
        });
    }
}