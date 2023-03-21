using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Br)]
    public static bool Is_BrPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0,
            CilCode.Ldarg_0,
            CilCode.Ldfld,
            CilCode.Unbox_Any,
            CilCode.Ldc_I4_1,
            CilCode.Sub,
            CilCode.Stfld,
            CilCode.Ret
        });
    }
}