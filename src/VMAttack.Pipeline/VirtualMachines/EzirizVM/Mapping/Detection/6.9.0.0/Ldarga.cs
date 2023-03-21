using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

public static partial class Handler
{
    [DetectV1(CilCode.Ldarga)]
    public static bool Is_LdargaPattern(this EzirizOpcode code)
    {
        
        var ldargPattern = new[]
        {
            CilCode.Ldarg_0, 
            CilCode.Ldfld, 
            CilCode.Ldarg_0, 
            CilCode.Ldfld,  
            CilCode.Unbox_Any, 
            CilCode.Ldarg_0, 
            CilCode.Newobj, 
            CilCode.Callvirt, 
            CilCode.Ret
        };
        
        var handler = code.Handler;
        var instructions = handler.Instructions;

        if (handler.MatchesEntire(ldargPattern))
        {
            if (instructions[4].Operand is not TypeReference type)
                return false;

            var module = type.Module;
            var corLibTypeFactory = module?.CorLibTypeFactory;

            if (type.ToTypeSignature() == corLibTypeFactory?.Int32)
                return true;
        }

        return false;
    }
}