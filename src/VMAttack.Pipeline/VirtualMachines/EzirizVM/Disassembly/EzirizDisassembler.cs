using System.Collections.Generic;
using System.Linq;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

/// <summary>
///     Represents a disassembler for Eziriz .NET Reactor-obfuscated assemblies.
/// </summary>
public class EzirizDisassembler : ContextBase
{
    // Stores all the Eziriz methods that have been disassembled so far
    private readonly Dictionary<uint, EzirizMethod> _disassembledMethods = new Dictionary<uint, EzirizMethod>();
    private readonly EzirizMethodReader _ezirizMethodReader;

    // Stores all the opcodes that have been used in the disassembled methods
    private readonly List<int?> _usedEzirizCodes = new List<int?>();

    /// <summary>
    ///     Initializes a new instance of the <see cref="EzirizDisassembler" /> class with the specified context and Eziriz
    ///     stream
    ///     reader.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="ezirizStreamReader">The Eziriz stream reader.</param>
    /// <exception cref="DevirtualizationException">Thrown if a reader cannot be created for the disassembler.</exception>
    public EzirizDisassembler(Context context, EzirizStreamReader ezirizStreamReader)
        : base(context, context.Logger)
    {
        if (!ezirizStreamReader.ManifestResource.TryGetReader(out var reader))
            throw new DevirtualizationException("Cannot create reader for disassembler!");

        _ezirizMethodReader = new EzirizMethodReader(context, reader, ezirizStreamReader);
    }

    /// <summary>
    ///     Gets an enumeration of all the opcodes that have been used in the disassembled methods.
    /// </summary>
    public List<int?> UsedEzirizCodes
    {
        get { return _usedEzirizCodes.OrderBy(opcode => opcode).Distinct().ToList(); }
    }

    /// <summary>
    ///     Gets an enumeration of all the Eziriz methods that have been disassembled so far.
    /// </summary>
    public IEnumerable<EzirizMethod> Methods => _disassembledMethods.Values;

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
            var disassembled = _ezirizMethodReader.CreateMethod(id, methodOffset);
            InsertMethod(id, disassembled);
            return disassembled;
        }

        return method;
    }

    private void InsertMethod(uint id, EzirizMethod disassembled)
    {
        var body = disassembled.EzirizBody;
        var instructions = body.Instructions;
        var opcodes = instructions.Select(x => x.Opcode.VirtualCode);

        _usedEzirizCodes.AddRange(opcodes);
        _disassembledMethods.Add(id, disassembled);
    }
}