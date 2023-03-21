using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ldelem_Ref)]
    public static bool Is_Ldelem_RefPattern(this EzirizOpcode code)
    {
        var handler = code.Handler;

        return handler.MatchesEntire(new CilCode[]  { CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Call, CilCode.Stloc_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Callvirt, CilCode.Ldnull, CilCode.Callvirt, CilCode.Castclass, CilCode.Dup, CilCode.Ldloc_S, CilCode.Callvirt, CilCode.Ldflda, CilCode.Ldfld, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Callvirt, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldloc_S, CilCode.Ldloc_S, CilCode.Call, CilCode.Callvirt, CilCode.Ret}
        );
    }
}