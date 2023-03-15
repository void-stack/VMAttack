using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Newobj)]
    public static bool Is_NewobjPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Stloc_S, CilCode.Ldtoken, CilCode.Call,
            CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Castclass, CilCode.Stloc_S, CilCode.Ldloc_S,
            CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Newarr,
            CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Newarr, CilCode.Stloc_S,
            CilCode.Ldnull, CilCode.Stloc_S, CilCode.Ldnull, CilCode.Stloc_S, CilCode.Ldc_I4_0, CilCode.Stloc_S,
            CilCode.Br, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Blt, CilCode.Ldarg_0,
            CilCode.Ldfld, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Ldlen,
            CilCode.Conv_I4, CilCode.Ldc_I4_1, CilCode.Sub, CilCode.Ldloc_S, CilCode.Sub, CilCode.Ldelem_Ref,
            CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldnull, CilCode.Stloc_3, CilCode.Ldc_I4_0, CilCode.Stloc_S,
            CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Brtrue_S, CilCode.Ldloc_S,
            CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Ldc_I4_1, CilCode.Sub, CilCode.Ldloc_S,
            CilCode.Sub, CilCode.Ldloc_S, CilCode.Stelem_Ref, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Ldlen,
            CilCode.Conv_I4, CilCode.Ldc_I4_1, CilCode.Sub, CilCode.Ldloc_S, CilCode.Sub, CilCode.Ldloc_3,
            CilCode.Stelem_Ref, CilCode.Ldloc_S, CilCode.Ldc_I4_1, CilCode.Add, CilCode.Stloc_S, CilCode.Ldloc_S,
            CilCode.Brfalse_S, CilCode.Ldloc_3, CilCode.Brtrue_S, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Brfalse_S,
            CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Call, CilCode.Stloc_3,
            CilCode.Ldloc_S, CilCode.Isinst, CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Castclass, CilCode.Ldloc_S,
            CilCode.Ldloc_3, CilCode.Call, CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Stloc_S,
            CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Stloc_3, CilCode.Ldloc_S, CilCode.Isinst,
            CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Brtrue_S, CilCode.Ldloc_S,
            CilCode.Ldloc_S, CilCode.Ldfld, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Ldc_I4_1,
            CilCode.Sub, CilCode.Ldloc_S, CilCode.Sub, CilCode.Newobj, CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Ldfld,
            CilCode.Stloc_3, CilCode.Ldloc_3, CilCode.Isinst, CilCode.Brfalse_S, CilCode.Ldc_I4_1, CilCode.Stloc_S,
            CilCode.Ldloc_3, CilCode.Isinst, CilCode.Stloc_S, CilCode.Br_S, CilCode.Newobj, CilCode.Stloc_S,
            CilCode.Ldnull, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ldnull, CilCode.Stloc_S,
            CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Stloc_S,
            CilCode.Ldc_I4_0, CilCode.Stloc_1, CilCode.Br, CilCode.Ldloc_1, CilCode.Ldloc_S, CilCode.Ldlen,
            CilCode.Conv_I4, CilCode.Blt, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Callvirt,
            CilCode.Callvirt, CilCode.Brfalse, CilCode.Ldloc_1, CilCode.Ldc_I4_1, CilCode.Add, CilCode.Stloc_1,
            CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Callvirt,
            CilCode.Brfalse_S, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Isinst, CilCode.Brfalse_S,
            CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref,
            CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Call, CilCode.Callvirt,
            CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref,
            CilCode.Callvirt, CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Call,
            CilCode.Callvirt, CilCode.Br_S, CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Castclass,
            CilCode.Ldloc_S, CilCode.Ldloc_1, CilCode.Ldelem_Ref, CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Ldloc_1,
            CilCode.Ldelem_Ref, CilCode.Call, CilCode.Callvirt, CilCode.Br_S, CilCode.Ldloc_S, CilCode.Ldloc_1,
            CilCode.Callvirt, CilCode.Brtrue_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldloc_S, CilCode.Callvirt,
            CilCode.Ldloc_S, CilCode.Call, CilCode.Callvirt, CilCode.Ret, CilCode.Ldloc_S, CilCode.Ldnull,
            CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Br_S, CilCode.Ldloc_S, CilCode.Ldloc_S,
            CilCode.Newobj, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldc_I4_1, CilCode.Ldloc_S, CilCode.Call,
            CilCode.Stloc_S
        });
    }
}