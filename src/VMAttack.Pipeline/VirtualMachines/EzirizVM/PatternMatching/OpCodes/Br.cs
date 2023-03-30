using System.Collections.Generic;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Br

internal record Br : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 - ldarg.0
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	object EzirizVM/EzirizInterpreter::Operand
        CilOpCodes.Unbox_Any, // 3 - unbox.any	[mscorlib]System.Int32
        CilOpCodes.Ldc_I4_1,  // 4 - ldc.i4.1
        CilOpCodes.Sub,       // 5 - sub
        CilOpCodes.Stfld,     // 6 - stfld	int32 EzirizVM/EzirizInterpreter::InstructionPointer
        CilOpCodes.Ret        // 7 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Br;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion

#region Brfalse

internal record Brfalse : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brtrue_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ceq,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ret,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Unbox_Any,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Sub,
        CilOpCodes.Stfld,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Stloc_S,
        CilOpCodes.Br_S
    };

    public CilOpCode CilOpCode => CilOpCodes.Brfalse;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion

#region Brtrue

internal record Brtrue : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0, 
        CilOpCodes.Ldfld, 
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S, 
        CilOpCodes.Ldloc_S, 
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ret, 
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ldarg_0, 
        CilOpCodes.Ldarg_0, 
        CilOpCodes.Ldfld, 
        CilOpCodes.Unbox_Any, 
        CilOpCodes.Ldc_I4_1, 
        CilOpCodes.Sub,
        CilOpCodes.Stfld
    };

    public CilOpCode CilOpCode => CilOpCodes.Brtrue;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion