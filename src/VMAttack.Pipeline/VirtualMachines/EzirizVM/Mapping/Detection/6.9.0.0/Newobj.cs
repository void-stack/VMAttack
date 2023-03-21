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

        return Helpers.FindOpCodePatterns(handler.Instructions, new[] {CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Stloc_S, CilCode.Ldtoken, CilCode.Call,
            CilCode.Callvirt, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Castclass, CilCode.Stloc_S, CilCode.Ldloc_S,
            CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Newarr,
            CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Newarr, CilCode.Stloc_S,
            CilCode.Ldnull, CilCode.Stloc_S, CilCode.Ldnull, CilCode.Stloc_S, CilCode.Ldc_I4_0, CilCode.Stloc_S,
            CilCode.Br, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Ldlen, CilCode.Conv_I4, CilCode.Blt, CilCode.Ldarg_0,
            CilCode.Ldfld, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Ldlen,
            CilCode.Conv_I4, CilCode.Ldc_I4_1, CilCode.Sub, CilCode.Ldloc_S, CilCode.Sub, CilCode.Ldelem_Ref}).Count == 1;
    }
}