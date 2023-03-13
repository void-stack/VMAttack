using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM;

public static class Extensions
{
    public static CilCode Identify(this EzirizOpcode ins)
    {
        return InstructionDetectorV1.GetInstance().Identify(ins);
    }

    public static bool TryIdentify(this EzirizOpcode ins, out CilCode code)
    {
        return InstructionDetectorV1.GetInstance().TryIdentify(ins, out code);
    }
}