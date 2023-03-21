using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ldsfld)]
    public static bool Is_LdsfldPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Unbox_Any,
            CilCode.Stloc_S,
            CilCode.Ldtoken,
            CilCode.Call,
            CilCode.Callvirt,
            CilCode.Ldloc_S,
            CilCode.Callvirt,
            CilCode.Stloc_S,
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Ldloc_S,
            CilCode.Callvirt,
            CilCode.Ldloc_S,
            CilCode.Ldnull,
            CilCode.Callvirt,
            CilCode.Call,
            CilCode.Callvirt,
            CilCode.Ret,
        });
    }
}