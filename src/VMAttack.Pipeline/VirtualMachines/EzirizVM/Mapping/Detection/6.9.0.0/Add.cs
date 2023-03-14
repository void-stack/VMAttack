/*using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Add)]
    public static bool Is_AddPattern(this EzirizOpcode ins)
    {
        return ins.Handler.MatchesEntire(new[]
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
                CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ldarg_0, CilCode.Ldfld,
                CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Callvirt, CilCode.Ret
            }
        );
    }
}*/

