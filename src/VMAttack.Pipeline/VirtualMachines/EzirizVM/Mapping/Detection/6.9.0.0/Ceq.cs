using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ceq)]
    public static bool Is_CeqPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new[]  { CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, 
            CilCode.Call, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Call, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Brfalse_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldc_I4_0, CilCode.Newobj, CilCode.Callvirt, CilCode.Ret, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldc_I4_1, CilCode.Newobj, CilCode.Callvirt, CilCode.Ret});
    }
}