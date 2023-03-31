using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Serialized;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Stloc

internal record Stloc : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0, // 0 - ldarg.0
        CilOpCodes.Ldfld, // 1 - ldfld	class Eziriz.VM/VMObject[] Eziriz.VM/VMMethodExecutor::VMVariables
        CilOpCodes.Unbox_Any, // 2 - unbox.any	[mscorlib]System.Int32
        CilOpCodes.Stloc_S, // 3 - stloc.s	V_25 (25)
        CilOpCodes.Ldarg_0, // 4 - ldarg.0
        CilOpCodes.Ldfld, // 5 - ldfld	class Eziriz.VM/VMObject[] Eziriz.VM/VMMethodExecutor::VMVariables
        CilOpCodes.Ldloc_S, // 6 - ldloc.s	V_25 (25)
        CilOpCodes.Ldarg_0, // 7 - ldarg.0
        CilOpCodes.Ldarg_0, // 8 - ldarg.0
        CilOpCodes.Ldfld, // 9 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Callvirt, // 10 - callvirt	instance class Eziriz.VM/VMObject Eziriz.VM/VMStack::PopVMLocal()
        CilOpCodes.Ldarg_0, // 11 - ldarg.0
        CilOpCodes.Ldfld, // 12 - ldfld	class Eziriz.VM/JWGbihLe59w0Tjpgs7W Eziriz.VM/VMMethodExecutor::eV7XiE3C4H
        CilOpCodes.Ldfld, // 13 - ldfld	class [mscorlib]System.Collections.Generic.List`1<class Eziriz.VM/V6kKaOL2XvatnIN8I0Z> Eziriz.VM/JWGbihLe59w0Tjpgs7W::fBcjhfhonR
        CilOpCodes.Ldloc_S, // 14 - ldloc.s	V_25 (25)
        CilOpCodes.Callvirt, // 15 - callvirt	instance !0 class [mscorlib]System.Collections.Generic.List`1<class Eziriz.VM/V6kKaOL2XvatnIN8I0Z>::get_Item(int32)
        CilOpCodes.Ldfld, // 16 - ldfld	valuetype Eziriz.VM/iQ4HZQLQyLCdABwFXsw Eziriz.VM/V6kKaOL2XvatnIN8I0Z::WhxjRcXFCB
        CilOpCodes.Ldarg_0, // 17 - ldarg.0
        CilOpCodes.Ldfld, // 18 - ldfld	class Eziriz.VM/JWGbihLe59w0Tjpgs7W Eziriz.VM/VMMethodExecutor::eV7XiE3C4H
        CilOpCodes.Ldfld, // 19 - ldfld	class [mscorlib]System.Collections.Generic.List`1<class Eziriz.VM/V6kKaOL2XvatnIN8I0Z> Eziriz.VM/JWGbihLe59w0Tjpgs7W::fBcjhfhonR
        CilOpCodes.Ldloc_S, // 20 - ldloc.s	V_25 (25)
        CilOpCodes.Callvirt, // 21 - callvirt	instance !0 class [mscorlib]System.Collections.Generic.List`1<class Eziriz.VM/V6kKaOL2XvatnIN8I0Z>::get_Item(int32)
        CilOpCodes.Ldfld, // 22 - ldfld	bool Eziriz.VM/V6kKaOL2XvatnIN8I0Z::r4GjQb7Vvg
        CilOpCodes.Call, // 23 - call	instance class Eziriz.VM/VMObject Eziriz.VM/VMMethodExecutor::Tf1j86bleG(class Eziriz.VM/VMObject, valuetype Eziriz.VM/iQ4HZQLQyLCdABwFXsw, bool)
        CilOpCodes.Stelem_Ref, //  24 - stelem.ref
        CilOpCodes.Ret // 25 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Stloc;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion

#region Ldloc

internal record Ldloc : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldarg_0,    // 0 - ldarg.0
        CilOpCodes.Ldfld,      // 1 - ldfld	class Eziriz.VM/VMObject[] Eziriz.VM/VMMethodExecutor::VMVariables
        CilOpCodes.Ldarg_0,    // 2 - ldarg.0
        CilOpCodes.Ldfld,      // 3 - ldfld	object Eziriz.VM/VMMethodExecutor::Operand
        CilOpCodes.Unbox_Any,  // 4 - unbox.any	[mscorlib]System.Int32
        CilOpCodes.Ldelem_Ref, // 5 - ldelem.ref
        CilOpCodes.Stloc_S,    // 6 - stloc.s	V_17 (17)
        CilOpCodes.Ldarg_0,    // 7 - ldarg.0
        CilOpCodes.Ldfld,      // 8 - ldfld	class Eziriz.VM/VMStack Eziriz.VM/VMMethodExecutor::Stack
        CilOpCodes.Ldloc_S,    // 9 - ldloc.s	V_17 (17)
        CilOpCodes.Callvirt,   // 10 - callvirt	instance Void Eziriz.VM/VMStack::AddVMLocal(class Eziriz.VM/VMObject)
        CilOpCodes.Ret         // 11 - ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldloc;

    public bool Verify(EzirizHandler handler) => handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Int32" };
}

#endregion

#region Ldloca

internal record Ldloca : IOpCodePattern
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


    public bool InterchangeLdcI4OpCodes => true;
    public bool InterchangeLdlocOpCodes => true;
    public bool InterchangeStlocOpCodes => true;
    public bool InterchangeBranchesOpCodes => true;

    public CilOpCode CilOpCode => CilOpCodes.Ldloca;

    public bool Verify(EzirizHandler handler)
    {
        if (handler.Instructions[4].Operand is ITypeDefOrRef { FullName: "System.Int32" })
        {
            // this is very breakable code, but it works for now :) against ldarga
            if (handler.Instructions[6].Operand is SerializedMethodDefinition ctor)
            {
                // get the ctor DeclaringType and check if it exist
                var ctorDeclaringType = ctor.DeclaringType;

                if (ctorDeclaringType == null)
                    return false;

                // check if class has a method named "nOQdl4ODOg" overwrite by the VM 
                var method = ctorDeclaringType.Methods.FirstOrDefault(x => x.Name == "nOQdl4ODOg"); // maybe pattern?

                // check instructions count >= 42
                if (method?.CilMethodBody?.Instructions.Count >= 42)
                    return true;
            }
        }

        return false;
    }
}

#endregion