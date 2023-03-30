using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Newobj

internal record Newobj : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldfld,  // 0
        CilOpCodes.Unbox_Any, // 1
        CilOpCodes.Stloc_S, // 2
        CilOpCodes.Ldtoken, // 3
        CilOpCodes.Call, // 4
        CilOpCodes.Callvirt, // 5
        CilOpCodes.Ldloc_S, // 6
        CilOpCodes.Callvirt, //7
        CilOpCodes.Castclass, //8
    };

    public bool MatchEntireBody => false;
    public CilOpCode CilOpCode => CilOpCodes.Newobj;
    public bool Verify(EzirizHandler handler) => handler.Instructions[9].Operand is ITypeDefOrRef { FullName: "System.Reflection.ConstructorInfo" };
}

#endregion