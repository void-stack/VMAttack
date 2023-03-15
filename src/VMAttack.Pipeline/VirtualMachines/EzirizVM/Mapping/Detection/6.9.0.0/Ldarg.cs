using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ldarg)]
    public static bool Is_LdargPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldarg_0, CilCode.Ldfld,
            CilCode.Unbox_Any, CilCode.Ldelem_Ref, CilCode.Callvirt, CilCode.Ret
        });
    }
}