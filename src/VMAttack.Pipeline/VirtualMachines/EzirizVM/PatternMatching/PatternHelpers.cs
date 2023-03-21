using System.Linq;
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

        foreach (var t in virtualMethod.Module.GetAllTypes())
        foreach (var vMethod in t.Methods.Where(x => x.IsVirtual && x.HasMethodBody && x.Name == virtualMethod.Name))
        {
            if (vMethod.CilMethodBody is not null)
                return PatternMatcher.MatchesPattern(pattern, vMethod);
        }

        return false;
    }
}