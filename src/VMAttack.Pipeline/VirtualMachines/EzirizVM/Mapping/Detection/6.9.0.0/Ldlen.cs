using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ldlen)]
    public static bool Is_LdlenPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]
        {
            CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Ldnull, CilCode.Callvirt, CilCode.Castclass,
            CilCode.Stloc_3, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldloc_3, CilCode.Callvirt, CilCode.Ldc_I4_5,
            CilCode.Newobj, CilCode.Callvirt, CilCode.Ret
        });
    }
}