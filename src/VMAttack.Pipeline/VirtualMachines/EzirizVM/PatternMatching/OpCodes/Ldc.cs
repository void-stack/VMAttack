using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Ldc_I4

internal record LdcI4 : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 - ldarg.0
        CilOpCodes.Ldfld,     // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldarg_0,   // 2 - ldarg.0
        CilOpCodes.Ldfld,     // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Unbox_Any, // 4 - unbox.any	[mscorlib]System.Int32
        CilOpCodes.Newobj,    // 5 - newobj	instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Callvirt,  // 6 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocal(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 7 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldc_I4;

    public bool Verify(EzirizHandler handler) => handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Int32" };
}

#endregion

#region Ldc_I8

internal record LdcI8 : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 - ldarg.0
        CilOpCodes.Ldfld,     // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldarg_0,   // 2 - ldarg.0
        CilOpCodes.Ldfld,     // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Unbox_Any, // 4 - unbox.any	[mscorlib]System.Int64
        CilOpCodes.Newobj,    // 5 - newobj	instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Callvirt,  // 6 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocal(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 7 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldc_I8;

    public bool Verify(EzirizHandler handler) => handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Int64" };
}

#endregion

#region Ldc_R4

internal record LdcR4 : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 - ldarg.0
        CilOpCodes.Ldfld,     // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldarg_0,   // 2 - ldarg.0
        CilOpCodes.Ldfld,     // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Unbox_Any, // 4 - unbox.any	[mscorlib]System.Single
        CilOpCodes.Newobj,    // 5 - newobj	instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Callvirt,  // 6 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocal(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 7 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldc_R4;

    public bool Verify(EzirizHandler handler) => handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Single" };
}

#endregion

#region Ldc_R8

internal record LdcR8 : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 - ldarg.0
        CilOpCodes.Ldfld,     // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldarg_0,   // 2 - ldarg.0
        CilOpCodes.Ldfld,     // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Unbox_Any, // 4 - unbox.any	[mscorlib]System.Double
        CilOpCodes.Newobj,    // 5 - newobj	instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Callvirt,  // 6 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocal(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 7 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldc_R8;
    public bool Verify(EzirizHandler handler) => handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Double" };
}

#endregion