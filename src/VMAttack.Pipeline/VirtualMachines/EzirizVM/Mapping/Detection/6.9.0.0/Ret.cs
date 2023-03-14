using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ret)]
    public static bool Is_RetPattern(this EzirizOpcode ins)
    {
        return ins.Handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldc_I4_S, CilCode.Stfld, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt,
            CilCode.Ldc_I4_0, CilCode.Ble_S, CilCode.Ret, CilCode.Ldarg_0, CilCode.Ldarg_0, CilCode.Ldfld,
            CilCode.Callvirt, CilCode.Stfld
        });
    }
}