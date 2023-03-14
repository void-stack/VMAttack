using System;
using AsmResolver.DotNet;
using AsmResolver.IO;
using AsmResolver.PE.DotNet.Metadata.Tables;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

/// <summary>
///     The class is used to decode a method from the EzirizStream and create an EzirizMethod from it.
///     The decoded method is created based on the given id and methodOffset.
/// </summary>
public class EzirizMethodReader : EzirizReaderBase
{
    /// <summary>
    ///     The context object used in the decoding process.
    /// </summary>
    private readonly Context _context;

    /// <summary>
    ///     The EzirizStream object containing the binary data to be read.
    /// </summary>
    private readonly EzirizStreamReader _ezirizStream;

    private readonly HandlerMapper _handlerMapper;


    /// <summary>
    ///     Constructor that initializes a new instance of the MethodDecoder class.
    /// </summary>
    /// <param name="context">The context object used in the decoding process.</param>
    /// <param name="reader">The BinaryStreamReader used to read binary data from a stream.</param>
    /// <param name="ezirizStream">The EzirizStream object containing the binary data to be read.</param>
    public EzirizMethodReader(Context context, BinaryStreamReader reader, EzirizStreamReader ezirizStream) : base(
        context,
        ref reader)
    {
        _handlerMapper = new HandlerMapper(context);
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

        // Reads the variables, exception handlers, and instructions for the method.
        ReadVariables(method, localsCount);
        ReadExceptionHandlers(method, exceptionHandlersCount);
        ReadInstructions(method, instructionsCount);

        // Returns the created EzirizMethod.
        return method;
    }

    /// <summary>
    ///     Reads the instructions for the given EzirizMethod.
    /// </summary>
    /// <param name="method">The EzirizMethod object to read the instructions for.</param>
    /// <param name="count">The count of instructions to be read.</param>
    private void ReadInstructions(EzirizMethod method, int count)
    {
        // TODO: MAKE THIS INTO SEPARATE CLASS
        Logger.Info($"Reading {count} instructions...");

        for (int i = 0; i < count; i++)
        {
            byte b = Reader.ReadByte();
            var opcode = new EzirizOpcode(b);
            var instr = new EzirizInstruction(opcode)
            {
                Offset = Reader.Offset
            };

            if (_handlerMapper.TryGetOpcodeHandler(b, out var handler))
                opcode.Handler = handler;
            else
                Logger.Warn($"No handler found for opcode {b}!");

            if (b >= 176) throw new DevirtualizationException("Disassembling Exception!");

            if (_ezirizStream.Operands.TryGetValue(b, out byte operand))
                instr.Operand = operand switch
                {
                    1 => ReadEncryptedByte(),
                    2 => Reader.ReadInt64(),
                    3 => Reader.ReadSingle(),
                    4 => Reader.ReadDouble(),

                    // read array
                    5 => ((Func<object>)(() =>
                    {
                        int size = ReadEncryptedByte();
                        int[] array = new int[size];
                        for (int index = 0; index < size; index++)
                            array[index] = ReadEncryptedByte();

                        return array;
                    }))(),
                    _ => null
                };

            method.EzirizBody.Instructions.Add(instr);

            Logger.Debug($"\t{instr}");
            Logger.Debug(string.Format("\t\tHandler: new CilCode[]  {{ " + opcode.Handler + "}};\n"));
        }
    }

    private void ReadVariables(EzirizMethod method, int count)
    {
        Logger.Info($"Reading {count} locals...");

        for (uint index = 0; index < count; index++)
        {
            int encryptedType = ReadEncryptedByte();

            if (encryptedType is >= 0 and < 50)
            {
                var type = (EzirizType)encryptedType & (EzirizType)31;
                bool isByRef = (encryptedType & 32) > 0;

                method.EzirizBody.Locals.Add(new EzirizVariable(index, type, isByRef));
            }
            else
            {
                Logger.Warn("Adding unknown local type!");
                method.EzirizBody.Locals.Add(new EzirizVariable(index, EzirizType.Object, false));
            }

            Logger.Debug($"\t{method.EzirizBody.Locals[(int)index]}");
        }
    }

    private void ReadExceptionHandlers(EzirizMethod method, int count)
    {
        Logger.Info($"Reading {count} exception handlers...");
        var exceptions = method.EzirizBody.ExceptionHandlers;

        var reader = new EzirizExceptionReader(_context, ref Reader);

        for (int i = 0; i < count; i++)
        {
            var eh = reader.ReadEh();
            exceptions.Add(eh);
        }

        if (exceptions.Count <= 0)
            return;

        Logger.Debug("Sorting exception handlers...");
        exceptions.Sort((x, y) => x.TryStart.CompareTo(y.TryStart));

        foreach (var exception in exceptions)
            Logger.Debug(exception.ToString());
    }
}