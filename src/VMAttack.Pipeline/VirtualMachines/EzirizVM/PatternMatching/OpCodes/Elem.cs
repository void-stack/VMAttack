using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Ldelem_Ref

internal record LdelemRef : IOpCodePattern
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
        CilOpCodes.Ldnull,
        CilOpCodes.Callvirt, 
        CilOpCodes.Castclass, 
        CilOpCodes.Dup, 
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldflda,
        CilOpCodes.Ldfld, 
        CilOpCodes.Callvirt, 
        CilOpCodes.Stloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Callvirt, 
        CilOpCodes.Stloc_S, 
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld, 
        CilOpCodes.Ldloc_S, 
        CilOpCodes.Ldloc_S,
        CilOpCodes.Call, 
        CilOpCodes.Callvirt, 
        CilOpCodes.Ret
    };

    public CilOpCode CilOpCode => CilOpCodes.Ldelem_Ref;

    public bool Verify(EzirizHandler handler) => true;
}

#endregion