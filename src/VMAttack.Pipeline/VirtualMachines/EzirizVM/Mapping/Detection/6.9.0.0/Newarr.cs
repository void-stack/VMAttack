using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Newarr)]
    public static bool Is_NewarrPattern(this EzirizOpcode code)
    {
        var ldargPattern = new[]
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
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Callvirt,
            CilCode.Call,
            CilCode.Stloc_S,
            CilCode.Ldloc_S,
            CilCode.Callvirt,
            CilCode.Ldflda,
            CilCode.Ldfld,
            CilCode.Call,
            CilCode.Stloc_S,
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Ldloc_S,
            CilCode.Newobj,
            CilCode.Callvirt,
            CilCode.Ret,
        };
        
        var handler = code.Handler;
        return handler.MatchesEntire(ldargPattern);
    }
}