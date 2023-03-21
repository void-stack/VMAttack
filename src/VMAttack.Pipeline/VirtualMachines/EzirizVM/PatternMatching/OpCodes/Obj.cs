using System.Collections.Generic;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching.OpCodes;

#region Newobj

internal record Newobj : IOpCodePattern
{
    public IList<CilOpCode> Pattern => new List<CilOpCode>
    {
        CilOpCodes.Ldfld,
        CilOpCodes.Unbox_Any,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldtoken,
        CilOpCodes.Call,
        CilOpCodes.Callvirt,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Castclass,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldlen,
        CilOpCodes.Conv_I4,
        CilOpCodes.Newarr,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldlen,
        CilOpCodes.Conv_I4,
        CilOpCodes.Newarr,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldnull,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldnull,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldc_I4_0,
        CilOpCodes.Stloc_S,
        CilOpCodes.Br,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldlen,
        CilOpCodes.Conv_I4,
        CilOpCodes.Blt,
        CilOpCodes.Ldarg_0,
        CilOpCodes.Ldfld,
        CilOpCodes.Callvirt,
        CilOpCodes.Stloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Ldlen,
        CilOpCodes.Conv_I4,
        CilOpCodes.Ldc_I4_1,
        CilOpCodes.Sub,
        CilOpCodes.Ldloc_S,
        CilOpCodes.Sub,
        CilOpCodes.Ldelem_Ref
    };

    public bool MatchEntireBody => false;
    public CilOpCode? CilOpCode => CilOpCodes.Newobj;

    public bool Verify(EzirizOpcode opcode) => true;
}

#endregion