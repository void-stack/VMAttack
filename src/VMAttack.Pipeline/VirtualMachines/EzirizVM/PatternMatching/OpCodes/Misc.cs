using System.Collections.Generic;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region CallVirt

internal record CallVirt : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,  // 1 - ldarg.0
        CilOpCodes.Ldc_I4_0, // 2 - ldc.i4.0
        CilOpCodes.Call,     // 3 - call	instance Void Eziriz.VM/VMMethodExecutor::CallMethodFromOperand(bool) 
        CilOpCodes.Ret       // 4 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Callvirt;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion

#region Call

internal record Call : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,  // 1 - ldarg.0
        CilOpCodes.Ldc_I4_1, // 2 - ldc.i4.1
        CilOpCodes.Call,     // 3 - call	instance Void Eziriz.VM/VMMethodExecutor::CallMethodFromOperand(bool) 
        CilOpCodes.Ret       // 4 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Call;

    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion

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
        CilOpCodes.Stfld     // 14 - stfld	class Eziriz.VM/VMObject Eziriz.VM/VMMethodExecutor::ReturnValue
    };

    public CilOpCode CilOpCode => CilOpCodes.Ret;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion Return

#region Pop

internal record Pop : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,  // 0 - ldarg.0
        CilOpCodes.Ldfld,    // 1 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt, // 2 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Pop,      // 3 - pop
        CilOpCodes.Ret       // 4 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Pop;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion

#region Ldstr

internal record Ldstr : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldsfld, // 0 - ldsfld	class [mscorlib]System.Collections.Generic.List`1<string> Eziriz.VM/VMMethodExecutor::Strings
        CilOpCodes.Callvirt, // 1 - callvirt	instance !0 class [mscorlib]System.Collections.Generic.List`1<string>::get_Item(int32)      
        CilOpCodes.Brtrue_S, // 2 - brtrue.s	379 (07C6) ldarg.0
        CilOpCodes.Ldarg_0, // 3 - ldarg.0
        CilOpCodes.Ldfld, // 4 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldsfld, // 5 - ldsfld	class [mscorlib]System.Collections.Generic.List`1<string> Eziriz.VM/VMMethodExecutor::Strings       
        CilOpCodes.Ldarg_0, // 6 - ldarg.0
        CilOpCodes.Ldfld, // 7 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand           
        CilOpCodes.Unbox_Any, // 8 - unbox.any	[mscorlib]System.Int32           
        CilOpCodes.Callvirt, // 9 - callvirt	instance !0 class [mscorlib]System.Collections.Generic.List`1<string>::get_Item(int32)            
        CilOpCodes.Newobj, // 10 - newobj	instance void Eziriz.VM/VMObject::.ctor(object)           
        CilOpCodes.Callvirt, // 11 - callvirt	instance void Eziriz.VM/VMStack::Push(class Eziriz.VM/VMObject)            
        CilOpCodes.Ret, // 12 - ret          
        CilOpCodes.Ldtoken, // 13 - ldtoken	EzirizVM          
        CilOpCodes.Call, // 14 - call	class [mscorlib]System.Element [mscorlib]System.Element::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)             
        CilOpCodes.Callvirt, // 15 - callvirt	instance class [mscorlib]System.Reflection.Module [mscorlib]System.Element::get_Module()             
        CilOpCodes.Stloc_S, // 16 - stloc.s	V_15 (15)          
        CilOpCodes.Ldarg_0, // 17 - ldarg.0         
        CilOpCodes.Ldfld, // 18 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack            
        CilOpCodes.Ldloc_S, // 19 - ldloc.s	V_15 (15)         
        CilOpCodes.Ldarg_0, // 20 - ldarg.0         
        CilOpCodes.Ldfld, // 21 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand            
        CilOpCodes.Unbox_Any, // 22 - unbox.any	[mscorlib]System.Int32           
        CilOpCodes.Ldc_I4, // 23 - ldc.i4	0x70000000          
        CilOpCodes.Or, // 24 - or           
        CilOpCodes.Callvirt, // 25 - callvirt	instance string [mscorlib]System.Reflection.Module::ResolveString(int32)             
        CilOpCodes.Newobj, // 26 - newobj	instance void Eziriz.VM/VMObject::.ctor(object)           
        CilOpCodes.Callvirt, // 27 - callvirt	instance void Eziriz.VM/VMStack::Push(class Eziriz.VM/VMObject)             
        CilOpCodes.Ret // 28 - ret

    };

    public CilOpCode CilOpCode => CilOpCodes.Ldstr;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion

#region Nop

internal record Nop : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Nop;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion

#region Ldlen

internal record Ldlen : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldnull,
        CilOpCodes.Callvirt,
        CilOpCodes.Castclass,
        CilOpCodes.Stloc_3,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldloc_3,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldc_I4_5,
        CilOpCodes.Newobj,
        CilOpCodes.Callvirt,
        CilOpCodes.Ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldlen;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion

#region Ceq

internal record Ceq : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Newobj,
        CilOpCodes.Callvirt,
        CilOpCodes.Ret,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Newobj,
        CilOpCodes.Callvirt,
        CilOpCodes.Ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ceq;
    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion