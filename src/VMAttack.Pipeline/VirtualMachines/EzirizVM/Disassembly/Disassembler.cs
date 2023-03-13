using System.Collections.Generic;
using System.Linq;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

/// <summary>
///     Represents a disassembler for Eziriz .NET Reactor-obfuscated assemblies.
/// </summary>
public class Disassembler : ContextBase
{
    // Stores all the Eziriz methods that have been disassembled so far
    private readonly Dictionary<uint, EzirizMethod> _disassembledMethods = new();
    private readonly HandlerMapper _handlerMapper;

    private readonly MethodReader _methodReader;

    // Stores all the opcodes that have been used in the disassembled methods
    private readonly List<int> _usedEzirizCodes = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="Disassembler" /> class with the specified context and Eziriz stream
    ///     reader.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="ezirizStreamReader">The Eziriz stream reader.</param>
    /// <exception cref="DevirtualizationException">Thrown if a reader cannot be created for the disassembler.</exception>
    public Disassembler(Context context, EzirizStreamReader ezirizStreamReader)
        : base(context, context.Logger)
    {
        if (!ezirizStreamReader.ManifestResource.TryGetReader(out var reader))
            throw new DevirtualizationException("Cannot create reader for disassembler!");

        _handlerMapper = new HandlerMapper(context);
        _methodReader = new MethodReader(context, reader, ezirizStreamReader);
    }

    /// <summary>
    ///     Gets an enumeration of all the opcodes that have been used in the disassembled methods.
    /// </summary>
    public IEnumerable<int> UsedEzirizCodes
    {
        get { return _usedEzirizCodes.OrderBy(opcode => opcode).Distinct().ToList(); }
    }

    /// <summary>
    ///     Gets an enumeration of all the Eziriz methods that have been disassembled so far.
    /// </summary>
    public IEnumerable<EzirizMethod> Methods
    {
        get { return _disassembledMethods.Values; }
    }

    /// <summary>
    ///     Maps the opcodes in the specified Eziriz method to their corresponding handler instructions.
    /// </summary>
    /// <param name="method">The Eziriz method to map opcodes for.</param>
    private void MapOpcodesInMethod(EzirizMethod method)
    {
        var body = method.EzirizBody;
        var instructions = body.Instructions;

        foreach (var instruction in instructions)
            if (!_handlerMapper.TryToSetOpcodeHandler(instruction.Opcode))
                Logger.Warn($"Failed to map code handler {instruction.Opcode.Code} in method {method.Id}!");
    }

    /// <summary>
    ///     Gets an existing Eziriz method with the specified ID, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="id">The ID of the Eziriz method to get or create.</param>
    /// <param name="methodOffset">The offset of the Eziriz method to create, if it doesn't exist.</param>
    /// <returns>The Eziriz method with the specified ID.</returns>
    public EzirizMethod GetOrCreateMethod(uint id, ulong methodOffset)
    {
        if (!_disassembledMethods.TryGetValue(id, out var method))
        {
            Logger.Info($"Created new method_{id:X4}, reading from offset {methodOffset:X8}.");
            var disassembled = _methodReader.CreateMethod(id, methodOffset);

            InsertMethod(id, disassembled);

            return disassembled;
        }

        return method;
    }

    private void InsertMethod(uint id, EzirizMethod disassembled)
    {
        var instructions = disassembled.EzirizBody.Instructions;
        var opcodes = instructions.Select(x => x.Opcode.Code);
        _usedEzirizCodes.AddRange(opcodes);

        MapOpcodesInMethod(disassembled);
        _disassembledMethods.Add(id, disassembled);
    }
}