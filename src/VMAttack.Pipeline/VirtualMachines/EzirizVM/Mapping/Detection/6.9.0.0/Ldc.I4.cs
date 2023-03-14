/*
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ldc_I4)]
    public static bool Is_LdcI4Pattern(this EzirizOpcode ins)
    {
        return ins.Handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Newobj,
            CilCode.Callvirt, CilCode.Ret
        });
    }
}
*/

