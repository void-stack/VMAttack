using System.Collections.Generic;
using AsmResolver.DotNet.Serialized;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Mul

internal record MulVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Mul,       // 8 - mul
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Mul : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::MulVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret

    };

    public CilOpCode CilOpCode => CilOpCodes.Mul;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;

        return virtualMethod.FindPatternInOverrides(new MulVmTypePattern());
    }
}

#endregion

#region Mul_Ovf

internal record MulOvfVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Mul_Ovf,   // 8 - Mul_Ovf
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record MulOvf : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::MulVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret

    };

    public CilOpCode CilOpCode => CilOpCodes.Mul_Ovf;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;

        return virtualMethod.FindPatternInOverrides(new MulOvfVmTypePattern());
    }
}

#endregion

#region Mul_Ovf_Un

internal record MulOvfUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,    // 1 - ldarg.0
        CilOpCodes.Ldflda,     // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,      // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,    // 4 - ldarg.1
        CilOpCodes.Castclass,  // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,     // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,      // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Mul_Ovf_Un, // 8 - Mul_Ovf_Un
        CilOpCodes.Newobj,     // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret         // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record MulOvfUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::MulVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret

    };

    public CilOpCode CilOpCode => CilOpCodes.Mul_Ovf_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new MulOvfUnVmTypePattern());
    }
}

#endregion

#region Add

internal record AddVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Add,       // 8 - Add
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Add : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Add;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new AddVmTypePattern());
    }
}

#endregion

#region Add_Ovf

internal record AddOvfVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Add_Ovf,   // 8 - Add_Ovf
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record AddOvf : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Add_Ovf;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new AddOvfVmTypePattern());
    }
}

#endregion

#region Add_Ovf_Un

internal record AddOvfUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,    // 1 - ldarg.0
        CilOpCodes.Ldflda,     // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,      // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,    // 4 - ldarg.1
        CilOpCodes.Castclass,  // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,     // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,      // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Add_Ovf_Un, // 8 - Add_Ovf_Un
        CilOpCodes.Newobj,     // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret         // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record AddOvfUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Add_Ovf_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new AddOvfUnVmTypePattern());
    }
}

#endregion

#region Sub

internal record SubVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Sub,       // 8 - Sub
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
    public bool Verify(EzirizHandler handler) => true;
}

internal record Sub : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::SubVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Sub;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new SubVmTypePattern());
    }
}

#endregion

#region Sub_Ovf

internal record SubOvfVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Sub_Ovf,   // 8 - Sub_Ovf
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
    public bool Verify(EzirizHandler handler) => true;
}

internal record SubOvf : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Call,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Castclass,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Newobj,
        CilOpCodes.Throw,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brfalse_S,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Callvirt,
        CilOpCodes.Ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Sub_Ovf;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new SubOvfVmTypePattern());
    }
}

#endregion

#region Sub_Ovf_Un

internal record SubOvfUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,    // 1 - ldarg.0
        CilOpCodes.Ldflda,     // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,      // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,    // 4 - ldarg.1
        CilOpCodes.Castclass,  // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,     // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,      // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Sub_Ovf_Un, // 8 - Sub_Ovf_Un
        CilOpCodes.Newobj,     // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret         // 10 - ret
    };

    public bool MatchEntireBody => false;
    public bool Verify(EzirizHandler handler) => true;
}

internal record SubOvfUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::SubVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Sub_Ovf_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new SubOvfUnVmTypePattern());
    }
}

#endregion

#region Div

internal record DivVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Div,       // 8 - Sub
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Div : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::DivVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Div;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new DivVmTypePattern());
    }
}

#endregion

#region Div_Un

internal record DivUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Div_Un,    // 8 - Sub
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record DivUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::DivVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Div_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new DivUnVmTypePattern());
    }
}

#endregion

#region Xor

internal record XorVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Xor,       // 8 - Xor
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Xor : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::XorVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Xor;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new XorVmTypePattern());
    }
}

#endregion

#region Rem

internal record RemVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Rem,       // 8 - Xor
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Rem : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::RemVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Rem;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new RemVmTypePattern());
    }
}

#endregion

#region Rem_Un

internal record RemUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Rem_Un,    // 8 - Rem_Un
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record RemUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::RemVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Rem_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new RemUnVmTypePattern());
    }
}

#endregion

#region Shr

internal record ShrVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldc_I4_S,  // 7 - ldc.i4.s 0x1F
        CilOpCodes.And,       // 8 - And
        CilOpCodes.Shr,       // 8 - Shr
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Shr : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::ShrVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Shr;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new ShrVmTypePattern());
    }
}

#endregion

#region Shr_Un

internal record ShrUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldc_I4_S,  // 7 - ldc.i4.s 0x1F
        CilOpCodes.And,       // 8 - And
        CilOpCodes.Shr_Un,    // 8 - Shr_Un
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record ShrUn : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::ShrVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Shr_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new ShrUnVmTypePattern());
    }
}

#endregion

#region Shl

internal record ShlVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldc_I4_S,  // 7 - ldc.i4.s 0x1F
        CilOpCodes.And,       // 8 - And
        CilOpCodes.Shl,       // 8 - Shr_Un
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Shl : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::RemVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Shl;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new ShlVmTypePattern());
    }
}

#endregion

#region And

internal record AndVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.And,       // 8 - And
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record And : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::DivVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.And;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new AndVmTypePattern());
    }
}

#endregion

#region Or

internal record OrVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Or,        // 8 - Or
        CilOpCodes.Newobj,    // 9 - newobj instance Void Eziriz.VM/VMIntegerType::.ctor(int32)
        CilOpCodes.Ret        // 10 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Or : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldfld,     // 2 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 3 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 4 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 5 - stloc.s	V_27 (27)
        CilOpCodes.Ldarg_0,   // 6 - ldarg.0
        CilOpCodes.Ldfld,     // 7 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt,  // 8 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Call,      // 9 - call	class Eziriz.VM/VMType Eziriz.VM/VMMethodExecutor::GetVMType(class Eziriz.VM/VMObject)
        CilOpCodes.Stloc_S,   // 10 - stloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 11 - ldloc.s	V_21 (21)
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance Void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance Void Eziriz.VM/VMStack::DivVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Or;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new OrVmTypePattern());
    }
}

#endregion

#region Cgt

internal record CgtVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Cgt,       // 8 - Cgt
        CilOpCodes.Ret        // 9 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Cgt : IOpCodePattern
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

    public CilOpCode CilOpCode => CilOpCodes.Cgt;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new CgtVmTypePattern());
    }
}

#endregion

#region Cgt_Un

internal record CgtUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Cgt_Un,    // 8 - Cgt_Un
        CilOpCodes.Ret        // 9 - ret
    };

    public bool MatchEntireBody => false;
}

internal record CgtUn : IOpCodePattern
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
        CilOpCodes.Ret,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Brtrue_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Brfalse_S, // 36 
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

    public CilOpCode CilOpCode => CilOpCodes.Cgt_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[36].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new CgtUnVmTypePattern());
    }
}

#endregion

#region Clt

internal record CltVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Clt,       // 8 - Clt
        CilOpCodes.Ret        // 9 - ret
    };

    public bool MatchEntireBody => false;
}

internal record Clt : IOpCodePattern
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

    public CilOpCode CilOpCode => CilOpCodes.Clt;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new CltVmTypePattern());
    }
}

#endregion

#region Clt_Un

internal record CltUnVmTypePattern : IPattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,   // 1 - ldarg.0
        CilOpCodes.Ldflda,    // 2 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 3 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Ldarg_1,   // 4 - ldarg.1
        CilOpCodes.Castclass, // 5 - castclass Eziriz.VM/VMIntegerType
        CilOpCodes.Ldflda,    // 6 - ldflda valuetype Eziriz.VM/hSn0ucLlLMaeFvn5InR Eziriz.VM/VMIntegerType::d37Bh8uTDv
        CilOpCodes.Ldfld,     // 7 - ldfld int32 Eziriz.VM/hSn0ucLlLMaeFvn5InR::GLgBtRhyOg
        CilOpCodes.Clt_Un,    // 8 - Clt_Un
        CilOpCodes.Ret        // 9 - ret
    };

    public bool MatchEntireBody => false;
}

internal record CltUn : IOpCodePattern
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

    public CilOpCode CilOpCode => CilOpCodes.Clt_Un;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[9].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new CltUnVmTypePattern());
    }
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