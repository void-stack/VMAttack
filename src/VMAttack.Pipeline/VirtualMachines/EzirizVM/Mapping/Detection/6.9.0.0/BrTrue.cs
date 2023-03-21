using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Brtrue)]
    public static bool Is_BrtruePattern(this EzirizOpcode code)
    {
        var handler = code.Handler;
        return handler.MatchesEntire(new CilCode[]  { CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldloc_S, CilCode.Brfalse_S, CilCode.Ret, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Brfalse_S, CilCode.Ldarg_0, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Ldc_I4_1, CilCode.Sub, CilCode.Stfld});
    }
}