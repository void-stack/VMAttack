using System;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;

public class DetectV1Attribute : Attribute
{
    public DetectV1Attribute(CilCode code)
    {
        Code = code;
    }

    public CilCode Code { get; }
}