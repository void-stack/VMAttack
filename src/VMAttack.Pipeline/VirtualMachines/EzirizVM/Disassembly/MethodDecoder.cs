﻿using AsmResolver.DotNet;
using AsmResolver.IO;
using AsmResolver.PE.DotNet.Metadata.Tables;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

/// <summary>
///     The class is used to decode a method from the EzirizStream and create an EzirizMethod from it.
///     The decoded method is created based on the given id and methodOffset.
/// </summary>
public class MethodDecoder : EzirizReaderBase
{
    /// <summary>
    ///     The context object used in the decoding process.
    /// </summary>
    private readonly Context _context;

    /// <summary>
    ///     The EzirizStream object containing the binary data to be read.
    /// </summary>
    private readonly EzirizStreamReader _ezirizStream;

    /// <summary>
    ///     Constructor that initializes a new instance of the MethodDecoder class.
    /// </summary>
    /// <param name="context">The context object used in the decoding process.</param>
    /// <param name="reader">The BinaryStreamReader used to read binary data from a stream.</param>
    /// <param name="ezirizStream">The EzirizStream object containing the binary data to be read.</param>
    public MethodDecoder(Context context, BinaryStreamReader reader, EzirizStreamReader ezirizStream) : base(context,
        reader)
    {
        _context = context;
        _ezirizStream = ezirizStream;
    }

    /// <summary>
    ///     Creates an EzirizMethod based on the given id and methodOffset.
    /// </summary>
    /// <param name="id">The id of the method to be decoded.</param>
    /// <param name="methodOffset">The offset of the method in the EzirizStream.</param>
    /// <returns>The created EzirizMethod.</returns>
    public EzirizMethod CreateMethod(uint id, ulong methodOffset)
    {
        Logger.Debug($"Reading method at offset {methodOffset:X4}");
        Reader.Offset = methodOffset;

        // Reads the metadata token of the method and resolves its parent method.
        var metadataToken = new MetadataToken((uint)ReadEncryptedByte());
        var parentMethod = ((IMethodDescriptor)_context.Module.LookupMember(metadataToken)).Resolve();
        Logger.Debug($"\tMethod parent is {parentMethod?.Name}");

        // Reads the count of locals, exception handlers, and instructions for the method.
        int localsCount = ReadEncryptedByte();
        int exceptionHandlersCount = ReadEncryptedByte();
        int instructionsCount = ReadEncryptedByte();

        // Creates a new EzirizMethod with the parent method, id, and methodOffset.
        var method = new EzirizMethod(parentMethod, id, methodOffset);

        // Reads the locals, exception handlers, and instructions for the method.
        ReadLocals(method, localsCount);
        ReadExceptionHandlers(method, exceptionHandlersCount);
        ReadInstructions(method, instructionsCount);

        Logger.Debug($"\t\t{method.EzirizBody}");

        // Returns the created EzirizMethod.
        return method;
    }

    /// <summary>
    ///     Reads the instructions for the given EzirizMethod.
    /// </summary>
    /// <param name="method">The EzirizMethod object to read the instructions for.</param>
    /// <param name="instructionsCount">The count of instructions to be read.</param>
    private void ReadInstructions(EzirizMethod method, int instructionsCount)
    {
        for (int i = 0; i < instructionsCount; i++)
        {
            byte b = Reader.ReadByte();
            var instr = new EzirizInstruction(EzirizOpCode.Nop);

            if (b > 173) throw new DevirtualizationException("Disassembling Exception!");

            if (_ezirizStream.Operands.TryGetValue(b, out byte operand))
                instr.Operand = operand switch
                {
                    // TODO: read operands
                    _ => null
                };
            method.EzirizBody.Instructions.Add(instr);
        }
    }

    private void ReadLocals(EzirizMethod method, int count)
    {
        for (int i = 0; i < count; i++) method.EzirizBody.Locals.Add(_context.Module.CorLibTypeFactory.Object);
    }

    private void ReadExceptionHandlers(EzirizMethod method, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var eh = new EzirizExceptionReader(_context, Reader).Read();
            method.EzirizBody.ExceptionHandlers.Add(eh);
        }
    }
}