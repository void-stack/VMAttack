using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Stloc)]
    public static bool Is_StlocPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Stloc_S, CilCode.Ldarg_0, CilCode.Ldfld,
            CilCode.Ldloc_S, CilCode.Ldarg_0, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Ldarg_0,
            CilCode.Ldfld, CilCode.Ldfld, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Ldfld, CilCode.Ldarg_0,
            CilCode.Ldfld, CilCode.Ldfld, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Ldfld, CilCode.Call,
            CilCode.Stelem_Ref, CilCode.Ret
        });
    }
}