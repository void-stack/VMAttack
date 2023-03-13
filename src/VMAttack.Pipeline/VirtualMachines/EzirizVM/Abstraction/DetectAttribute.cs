using System;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;

public class DetectAttribute : Attribute
{
    public DetectAttribute(CilCode code)
    {
        Code = code;
    }

    public CilCode Code { get; }
}