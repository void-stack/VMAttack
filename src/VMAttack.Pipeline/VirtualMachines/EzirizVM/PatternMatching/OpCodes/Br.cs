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

#region Beq

internal class BeqVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldloc_0,
        CilOpCodes.Callvirt,
        CilOpCodes.Brtrue,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ret,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldloc_0,
        CilOpCodes.Castclass,
        CilOpCodes.Ldfld,
        CilOpCodes.Ceq,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record Beq : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 8
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

    public CilOpCode CilOpCode => CilOpCodes.Beq;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[8].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BeqVmTypePattern());
    }
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

#region Bge

internal record BgeVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldarg_1,
        CilOpCodes.Castclass,
        CilOpCodes.Ldfld,
        CilOpCodes.Clt_Un,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ceq,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record Bge : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 10
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

    public CilOpCode CilOpCode => CilOpCodes.Bge;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BgeVmTypePattern());
    }
}

#endregion

#region Bge_Un

internal record BgeUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldarg_1,
        CilOpCodes.Castclass,
        CilOpCodes.Ldfld,
        CilOpCodes.Clt_Un,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ceq,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record BgeUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 9
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

    public CilOpCode CilOpCode => CilOpCodes.Bge_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BgeUnVmTypePattern());
    }
}

#endregion

#region Bgt

internal record BgtVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldarg_1,
        CilOpCodes.Castclass,
        CilOpCodes.Ldfld,
        CilOpCodes.Cgt,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record Bgt : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 9
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

    public CilOpCode CilOpCode => CilOpCodes.Bgt;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BgtVmTypePattern());
    }
}

#endregion

#region Bgt_Un

internal record BgtUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Call,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Call,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ret,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Unbox_Any,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Sub,
        CilOpCodes.Stfld,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brtrue_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
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

    public CilOpCode CilOpCode => CilOpCodes.Bgt_Un;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion

#region Ble

internal record BleVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldarg_1,
        CilOpCodes.Castclass,
        CilOpCodes.Ldfld,
        CilOpCodes.Cgt_Un,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ceq,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record Ble : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 9 
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

    public CilOpCode CilOpCode => CilOpCodes.Ble;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BleVmTypePattern());
    }
}

#endregion

#region Ble_Un

internal record BleUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldarg_1,
        CilOpCodes.Castclass,
        CilOpCodes.Ldfld,
        CilOpCodes.Cgt_Un,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Ceq,
        CilOpCodes.Ret
    };

    public bool MatchEntireBody => false;
}

internal record BleUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
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

    public CilOpCode CilOpCode => CilOpCodes.Ble_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BleUnVmTypePattern());
    }
}

#endregion

#region Blt

internal record BltVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 ldarg.0 
        CilOpCodes.Ldfld,     // 1 ldfld float64 jOgQY3RGtH5fd9qQao.OBqe2IUAeSpOmlOQ4O/j5UMoXLRwIcnpEjt1UJ::BUuja1QMZO
        CilOpCodes.Ldarg_1,   // 2 ldarg.1
        CilOpCodes.Castclass, // 3 castclass jOgQY3RGtH5fd9qQao.OBqe2IUAeSpOmlOQ4O/j5UMoXLRwIcnpEjt1UJ
        CilOpCodes.Ldfld,     // 4 ldfld float64 jOgQY3RGtH5fd9qQao.OBqe2IUAeSpOmlOQ4O/j5UMoXLRwIcnpEjt1UJ::BUuja1QMZO
        CilOpCodes.Clt,       // 5 clt
        CilOpCodes.Ret        // 6 ret
    };

    public bool MatchEntireBody => false;
}

internal record Blt : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 9 
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

    public CilOpCode CilOpCode => CilOpCodes.Blt;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BltVmTypePattern());
    }
}

#endregion

#region Blt_Un

internal record BltUnUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 0 ldarg.0 
        CilOpCodes.Ldfld,     // 1 ldfld float64 jOgQY3RGtH5fd9qQao.OBqe2IUAeSpOmlOQ4O/j5UMoXLRwIcnpEjt1UJ::BUuja1QMZO
        CilOpCodes.Ldarg_1,   // 2 ldarg.1
        CilOpCodes.Castclass, // 3 castclass jOgQY3RGtH5fd9qQao.OBqe2IUAeSpOmlOQ4O/j5UMoXLRwIcnpEjt1UJ
        CilOpCodes.Ldfld,     // 4 ldfld float64 jOgQY3RGtH5fd9qQao.OBqe2IUAeSpOmlOQ4O/j5UMoXLRwIcnpEjt1UJ::BUuja1QMZO
        CilOpCodes.Clt,       // 5 clt
        CilOpCodes.Ret        // 6 ret
    };

    public bool MatchEntireBody => false;
}

internal record BltUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt, // 9 
        CilOpCodes.Dup,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Newobj,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ret,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Unbox_Any,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Sub,
        CilOpCodes.Stfld,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Newobj,
        CilOpCodes.Callvirt,
        CilOpCodes.Br_S
    };

    public CilOpCode CilOpCode => CilOpCodes.Blt_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new BltUnUnVmTypePattern());
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