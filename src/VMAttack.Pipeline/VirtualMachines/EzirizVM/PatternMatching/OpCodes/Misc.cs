using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

[SuppressMessage("ReSharper", "ArrangeAccessorOwnerBody")]

#region Return

internal record Ret : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,  // 1 - ldarg.0
        CilOpCodes.Ldc_I4_S, // 2 - ldc.i4.s	-3
        CilOpCodes.Stfld,    // 3 - stfld	int32 Eziriz.VM/VMMethodExecutor::Unknown
        CilOpCodes.Ldarg_0,  // 4 - ldarg.0
        CilOpCodes.Ldfld,    // 5 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt, // 6 - callvirt	instance int32 Eziriz.VM/VMStack::Size()
        CilOpCodes.Ldc_I4_0, // 7 - ldc.i4.0
        CilOpCodes.Ble_S,    // 8 - ble	2110 (1D74) ret
        CilOpCodes.Ret,      // 9 - ret
        CilOpCodes.Ldarg_0,  // 10 - ldarg.0
        CilOpCodes.Ldarg_0,  // 11 - ldarg.0
        CilOpCodes.Ldfld,    // 12 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt, // 13 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Stfld,    // 14 - stfld	class Eziriz.VM/VMObject Eziriz.VM/VMMethodExecutor::ReturnValue
    };

    public CilOpCode? CilOpCode => CilOpCodes.Ret;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion Return