using System;
using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Recompiler;

public class CilMethodBodyGenerator : ContextBase
{
    public CilMethodBodyGenerator(Context context, EzirizStreamReader ezirizStream) : base(context, context.Logger)
    {
        EzirizStream = ezirizStream;
    }

    private EzirizStreamReader EzirizStream { get; }

    /// <summary>
    ///     Translates the specified <paramref name="ezirizElement" /> to a corresponding <see cref="ITypeDescriptor" />
    ///     object.
    /// </summary>
    /// <param name="ezirizElement">The <see cref="EzirizElement" /> to translate to a <see cref="ITypeDescriptor" />.</param>
    /// <returns>An <see cref="ITypeDescriptor" /> object corresponding to the specified <paramref name="ezirizElement" />.</returns>
    private ITypeDescriptor TranslateToType(EzirizElement ezirizElement)
    {
        var typeFactory = Context.Module.CorLibTypeFactory;

        return ezirizElement switch
        {
            EzirizElement.Object => typeFactory.Object,
            EzirizElement.SByte => typeFactory.SByte,
            EzirizElement.Byte => typeFactory.Byte,
            EzirizElement.Int16 => typeFactory.Int16,
            EzirizElement.UInt16 => typeFactory.UInt16,
            EzirizElement.Int32 => typeFactory.Int32,
            EzirizElement.UInt32 => typeFactory.UInt32,
            EzirizElement.Int64 => typeFactory.Int64,
            EzirizElement.UInt64 => typeFactory.UInt64,
            EzirizElement.Single => typeFactory.Single,
            EzirizElement.Double => typeFactory.Double,
            EzirizElement.Boolean => typeFactory.Boolean,
            EzirizElement.IntPtr => typeFactory.IntPtr,
            EzirizElement.UIntPtr => typeFactory.UIntPtr,
            EzirizElement.String => typeFactory.String,
            EzirizElement.Char => typeFactory.Char,
            EzirizElement.Enum => typeFactory.Object,
            EzirizElement.Unknown => typeFactory.Object,
            EzirizElement.Void => typeFactory.Void,
            _ => throw new ArgumentOutOfRangeException(nameof(ezirizElement), ezirizElement, null)
        };
    }

    /// <summary>
    ///     Generates a list of <see cref="CilInstruction" /> objects from the specified <paramref name="vmMethodBody" />.
    /// </summary>
    /// <param name="vmMethodBody">The <see cref="EzirizMethodBody" /> to generate instructions from.</param>
    /// <returns>
    ///     A list of <see cref="CilInstruction" /> objects representing the instructions in the specified
    ///     <paramref name="vmMethodBody" />.
    /// </returns>
    private static IEnumerable<CilInstruction> GenerateInstructions(EzirizMethodBody vmMethodBody)
    {
        var instructions = new List<CilInstruction>();

        // Generate a CilInstruction for each VmInstruction in the VmMethodBody
        foreach (var vmInstruction in vmMethodBody.Instructions)
            instructions.Add(new CilInstruction(vmInstruction.Opcode.CilOpCode, vmInstruction.Operand));

        return instructions;
    }

    /// <summary>
    ///     Compiles the specified virtualized method body into a Common Intermediate Language (CIL) method body.
    /// </summary>
    /// <param name="vmMethodBody">The virtualized method body to compile.</param>
    /// <returns>A CIL method body that corresponds to the specified virtualized method body.</returns>
    public CilMethodBody Compile(EzirizMethodBody vmMethodBody)
    {
        var virtualizedMethod = vmMethodBody.VirtualParent;
        var physicalMethod = virtualizedMethod.PhysicalParent;

        var cilMethodBody = new CilMethodBody(physicalMethod);

        if (vmMethodBody.Variables.Count > 0)
        {
            foreach (var variable in vmMethodBody.Variables)
                cilMethodBody.LocalVariables.Add(new CilLocalVariable(TranslateToType(variable.Element).ToTypeSignature()));

            cilMethodBody.InitializeLocals = true;
        }

        cilMethodBody.Instructions.AddRange(GenerateInstructions(vmMethodBody));
        cilMethodBody.Instructions.CalculateOffsets();

        foreach (var instruction in cilMethodBody.Instructions)
            ValidateInstruction(instruction, cilMethodBody);

        cilMethodBody.Instructions.CalculateOffsets();
        cilMethodBody.Instructions.OptimizeMacros();

        return cilMethodBody;
    }

    /// <summary>
    ///     Validates the specified CIL instruction.
    /// </summary>
    /// <param name="instruction">The CIL instruction to validate.</param>
    /// <param name="cilMethodBody">The CIL method body that contains the instruction.</param>
    /// <exception cref="Exception">
    ///     When the instruction operand is invalid or not within the instruction body.
    /// </exception>
    private void ValidateInstruction(CilInstruction instruction, CilMethodBody cilMethodBody)
    {
        var module = cilMethodBody.Owner.Module;

        if (module is null)
            throw new Exception("Physical method has no module assigned.");

        switch (instruction.OpCode.OperandType)
        {
            case CilOperandType.ShortInlineBrTarget:
            case CilOperandType.InlineBrTarget:
                if (instruction.Operand is not ICilLabel)
                {
                    int offset = instruction.Operand is int operand ? operand : throw new Exception("Invalid ICilLabel operand");

                    if (offset < 0 || offset >= cilMethodBody.Instructions.Count)
                        throw new Exception("Not within instruction body: " + offset);

                    var targetInstruction = cilMethodBody.Instructions[offset].CreateLabel();
                    instruction.Operand = targetInstruction;
                }
                break;

            case CilOperandType.InlineSwitch:
                if (instruction.Operand is not IList<ICilLabel>)
                {
                    int[] offset = instruction.Operand is int[] operand ? operand : throw new Exception("Invalid IList<ICilLabel> operand");

                    instruction.Operand = new List<ICilLabel>(offset.Length);

                    foreach (int t in offset)
                    {
                        if (t < 0 || t >= cilMethodBody.Instructions.Count)
                            throw new Exception("Not within instruction body: " + t);

                        ((List<ICilLabel>) instruction.Operand).Add(cilMethodBody.Instructions[t].CreateLabel());
                    }
                }
                break;

            case CilOperandType.InlineMethod:
            case CilOperandType.InlineField:
            case CilOperandType.InlineType:
            case CilOperandType.InlineTok:
                if (instruction.Operand is not IMemberDescriptor)
                {
                    int token = instruction.Operand is int operand ? operand : 0;

                    if (module.TryLookupMember(token, out var member))
                        instruction.Operand = member;
                    else
                        throw new Exception("Could not find member with token " + token);
                }
                break;

            case CilOperandType.InlineSig:
                if (instruction.Operand is not StandAloneSignature)
                {
                    int token = instruction.Operand is int operand ? operand : 0;

                    if (module.TryLookupMember(token, out var member))
                        instruction.Operand = member;
                    else
                        throw new Exception("Expected a valid signature operand with token: " + token);
                }
                break;

            case CilOperandType.InlineString:
                if (instruction.Operand is not string)
                {
                    int token = instruction.Operand is int operand ? operand : throw new Exception("Expected a valid string operand");

                    if (EzirizStream.Strings.Count == 0)
                    {
                        int fixedToken = token | 0x70000000;

                        if (module.TryLookupString(fixedToken, out string? member))
                            instruction.Operand = member;
                        else
                            throw new Exception("Expected a valid string operand with token: " + fixedToken);
                    }
                    else
                    {
                        if (EzirizStream.Strings.TryGetValue((uint) token, out string? resolvedString))
                            instruction.Operand = resolvedString;
                        else
                            throw new Exception("Could not find resolved string in EzirizStream with token: " + token);
                    }
                }
                break;

            case CilOperandType.ShortInlineVar:
            case CilOperandType.InlineVar:
                if (instruction.Operand is not CilLocalVariable)
                {
                    int index = instruction.Operand is int operand ? operand : throw new Exception("Expected a valid local variable index");

                    if (index >= cilMethodBody.LocalVariables.Count)
                        throw new Exception("Expected a valid local variable index");

                    instruction.Operand = cilMethodBody.LocalVariables[index];
                }
                break;

            case CilOperandType.InlineArgument:
            case CilOperandType.ShortInlineArgument:
                if (instruction.Operand is not CilLocalVariable)
                {
                    int index = instruction.Operand is int operand ? operand : throw new Exception("Expected a valid local variable index");
                    var signature = cilMethodBody.Owner.Parameters.GetBySignatureIndex(index);

                    if (signature is null)
                        throw new Exception("Expected a valid local variable index");

                    instruction.Operand = signature;
                }
                break;

            case CilOperandType.InlineI:
                if (instruction.Operand is not int)
                    throw new Exception("Expected a valid int operand");
                break;

            case CilOperandType.InlineI8:
                if (instruction.Operand is not long)
                    throw new Exception("Expected a valid long operand");
                break;

            case CilOperandType.InlineNone:
                if (Context.Options.WriteNotCompletedVirtualizedBodies == false && instruction.Operand != null)
                    throw new Exception("Unexpected operand");
                break;

            case CilOperandType.InlineR:
                if (instruction.Operand is not double)
                    throw new Exception("Expected a valid double operand");
                break;

            case CilOperandType.ShortInlineI:
                if (instruction.Operand is not sbyte)
                    throw new Exception("Expected a valid sbyte operand");
                break;

            case CilOperandType.ShortInlineR:
                if (instruction.Operand is not float)
                    throw new Exception("Expected a valid float operand");
                break;
        }
    }
}