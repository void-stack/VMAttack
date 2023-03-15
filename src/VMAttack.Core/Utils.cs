using AsmResolver.DotNet;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;

namespace VMAttack.Core;

/// <summary>
///     This class provides helper methods for working with .NET methods.
/// </summary>
public static class Utils
{
    /// <summary>
    ///     Determines whether the given method calls the method with the specified full name.
    /// </summary>
    /// <param name="method">The method to check.</param>
    /// <param name="methodFullName">The full name of the method to search for.</param>
    /// <returns>true if the given method calls the specified method; otherwise, false.</returns>
    public static bool CallsMethod(this MethodDefinition? method, string methodFullName)
    {
        if (method?.CilMethodBody == null)
            return false;

        var instructions = method.CilMethodBody.Instructions;

        foreach (var instr in instructions)
        {
            if (instr.OpCode.Code != CilCode.Call && instr.OpCode.Code != CilCode.Callvirt
                                                  && instr.OpCode.Code != CilCode.Newobj)
                continue;
            if (instr.Operand is not IMethodDescriptor calledMethod)
                continue;

            if (calledMethod.FullName == methodFullName)
                return true;
        }

        return false;
    }


    public static int GetIndexOfFunctionCall(this CilMethodBody? body, string fullMethodName)
    {
        if (body == null)
            return 0;

        var instructions = body.Instructions;

        for (int i = 0; i < instructions.Count; i++)
        {
            var instr = instructions[i];
            if (instr.OpCode.Code != CilCode.Call && instr.OpCode.Code != CilCode.Callvirt
                                                  && instr.OpCode.Code != CilCode.Newobj)
                continue;

            if (instr.Operand is not IMethodDescriptor calledMethod)
                continue;

            if (calledMethod.FullName != fullMethodName)
                continue;

            return i;
        }

        return 0;
    }
}