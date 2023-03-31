using System.Collections.Generic;
using AsmResolver.DotNet.Serialized;
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

#region Bne_Un

internal record BneUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldflda,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldloc_0,
        CilOpCodes.Castclass,
        CilOpCodes.Ldflda,
        CilOpCodes.Ldfld,
        CilOpCodes.Ceq,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ceq,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record BneUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Callvirt, // 6
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ret,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Unbox_Any,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Sub,
        CilOpCodes.Stfld
    };

    public CilOpCode CilOpCode => CilOpCodes.Bne_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[6].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BneUnVmTypePattern());
    }
}

#endregion

#region Switch

internal record Switch : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Castclass,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldflda,
        CilOpCodes.Ldfld,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Conv_I8,
        CilOpCodes.Blt_S,
        CilOpCodes.Call,
        CilOpCodes.Ldc_I4_4,
        CilOpCodes.Bne_Un_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldlen,
        CilOpCodes.Conv_I4,
        CilOpCodes.Conv_I8,
        CilOpCodes.Bge_S,
        CilOpCodes.Ret,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Conv_I8,
        CilOpCodes.Blt_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Conv_Ovf_I,
        CilOpCodes.Ldelem_I4,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Sub,
        CilOpCodes.Stfld,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Castclass,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldc_I4_6,
        CilOpCodes.Bne_Un_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldflda,
        CilOpCodes.Ldfld,
        CilOpCodes.Conv_U8,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Conv_I4,
        CilOpCodes.Conv_I8,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S
    };

    public CilOpCode CilOpCode => CilOpCodes.Switch;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion