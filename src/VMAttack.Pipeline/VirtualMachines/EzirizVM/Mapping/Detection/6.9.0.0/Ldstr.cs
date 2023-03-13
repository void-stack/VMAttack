using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [Detect(CilCode.Ldstr)]
    public static bool Is_Ldstr(this EzirizOpcode ins)
    {
        return ins.HandlerMatchesEntire(new[]
        {
            CilCode.Ldsfld, CilCode.Callvirt, CilCode.Brtrue_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Ldsfld,
            CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Callvirt, CilCode.Newobj, CilCode.Callvirt,
            CilCode.Ret, CilCode.Ldtoken, CilCode.Call, CilCode.Callvirt, CilCode.Stloc_S, CilCode.Ldarg_0,
            CilCode.Ldfld, CilCode.Ldloc_S, CilCode.Ldarg_0, CilCode.Ldfld, CilCode.Unbox_Any, CilCode.Ldc_I4,
            CilCode.Or, CilCode.Callvirt, CilCode.Newobj, CilCode.Callvirt, CilCode.Ret
        });
    }
}