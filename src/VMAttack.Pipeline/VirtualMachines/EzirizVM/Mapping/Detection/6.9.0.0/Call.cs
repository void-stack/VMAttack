using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [Detect(CilCode.Call)]
    public static bool Is_CallPattern(this EzirizOpcode ins)
    {
        return ins.HandlerMatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldc_I4_0, CilCode.Call, CilCode.Ret
        });
    }
}