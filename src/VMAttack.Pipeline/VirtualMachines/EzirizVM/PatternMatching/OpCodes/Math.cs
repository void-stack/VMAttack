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
        CilOpCodes.Newobj,    // 9 - newobj instance void Eziriz.VM/VMIntegerType::.ctor(int32)
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
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance void Eziriz.VM/VMStack::MulVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret

    };

    public CilOpCode? CilOpCode => CilOpCodes.Mul;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;

        return virtualMethod.FindPatternInOverrides(new MulVmTypePattern());
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
        CilOpCodes.Newobj,    // 9 - newobj instance void Eziriz.VM/VMIntegerType::.ctor(int32)
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
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance void Eziriz.VM/VMStack::AddVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode? CilOpCode => CilOpCodes.Add;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new AddVmTypePattern());
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
        CilOpCodes.Newobj,    // 9 - newobj instance void Eziriz.VM/VMIntegerType::.ctor(int32)
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
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance void Eziriz.VM/VMStack::SubVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode? CilOpCode => CilOpCodes.Sub;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new SubVmTypePattern());
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
        CilOpCodes.Sub,       // 8 - Sub
        CilOpCodes.Newobj,    // 9 - newobj instance void Eziriz.VM/VMIntegerType::.ctor(int32)
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
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance void Eziriz.VM/VMStack::DivVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode? CilOpCode => CilOpCodes.Div;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new DivVmTypePattern());
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
        CilOpCodes.Newobj,    // 9 - newobj instance void Eziriz.VM/VMIntegerType::.ctor(int32)
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
        CilOpCodes.Brfalse_S, // 12 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Newobj,    // 22 - newobj	instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Throw,     // 23 - throw
        CilOpCodes.Ldloc_S,   // 13 - ldloc.s	V_27 (27)
        CilOpCodes.Brfalse_S, // 14 - brfalse.s	2002 (1B61) newobj instance void Eziriz.VM/VMException::.ctor()
        CilOpCodes.Ldarg_0,   // 15 - ldarg.0
        CilOpCodes.Ldfld,     // 16 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,   // 17 - ldloc.s	V_21 (21)
        CilOpCodes.Ldloc_S,   // 18 - ldloc.s	V_27 (27)
        CilOpCodes.Callvirt,  // 19 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMType::PZYuDsqDg5(class Eziriz.VM/VMObject)
        CilOpCodes.Callvirt,  // 20 - callvirt	instance void Eziriz.VM/VMStack::XorVMLocals(class Eziriz.VM/VMObject)
        CilOpCodes.Ret        // 21 - ret
    };

    public CilOpCode? CilOpCode => CilOpCodes.Xor;

    public bool Verify(EzirizHandler handler)
    {
        var virtualMethod = handler.Instructions[20].Operand as SerializedMethodDefinition;
        return virtualMethod.FindPatternInOverrides(new XorVmTypePattern());
    }
}

#endregion