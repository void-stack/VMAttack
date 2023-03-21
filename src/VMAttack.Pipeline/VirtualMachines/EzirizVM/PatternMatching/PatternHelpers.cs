using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Serialized;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching;

public static class PatternHelpers
{
    public static bool FindPatternInOverrides(this SerializedMethodDefinition? virtualMethod, IPattern pattern)
    {
        if (virtualMethod is not { IsVirtual: true, IsAbstract: true })
            return false;

        if (virtualMethod.Module is null)
            return false;

        var overwrites = new List<MethodDefinition>();

        foreach (var t in virtualMethod.Module.GetAllTypes())
        foreach (var vMethod in t.Methods.Where(x => x.IsVirtual && x.HasMethodBody && x.Name == virtualMethod.Name))
        {
            if (vMethod.CilMethodBody is null)
                continue;

            if (PatternMatcher.GetAllMatchingInstructions(pattern, vMethod.CilMethodBody.Instructions).Count == 1)
                overwrites.Add(vMethod);
        }

        return overwrites.Count > 0;
    }
}