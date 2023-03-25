using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Ldarg

internal record Ldarg : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,    // 0 - ldarg.0
        CilOpCodes.Ldfld,      // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldarg_0,    // 2 - ldarg.0
        CilOpCodes.Ldfld,      // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Ldarg_0,    // 4 - ldarg.0
        CilOpCodes.Ldfld,      // 5 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Unbox_Any,  // 6 - unbox.any	[mscorlib]System.Int32
        CilOpCodes.Ldelem_Ref, // 7 - ldelem.ref     
        CilOpCodes.Callvirt,   // 8 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocal(class Eziriz.VM/VMObject)
        CilOpCodes.Ret         // 9 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldarg;

    public bool Verify(EzirizHandler handler) => handler.Instructions[6].Operand is ITypeDefOrRef { FullName: "System.Int32" };
}

#endregion

#region Ldarga

internal record Ldarga : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 - ldarg.0
        CilOpCodes.Ldfld,     // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldarg_0,   // 2 - ldarg.0
        CilOpCodes.Ldfld,     // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Unbox_Any, // 4 - unbox.any	[mscorlib]System.Int32
        CilOpCodes.Ldarg_0,   // 5 - ldarg.0
        CilOpCodes.Newobj,    // 6 - newobj	instance void Eziriz.VM/Class27::.ctor(int32, class Eziriz.VM/VMMethodExecutor)
        CilOpCodes.Callvirt,  // 7 - callvirt	instance void Eziriz.VM/VMStack::PushValue(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 8 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldarga;

    public bool Verify(EzirizHandler handler) => handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Int32" };
}

#endregion