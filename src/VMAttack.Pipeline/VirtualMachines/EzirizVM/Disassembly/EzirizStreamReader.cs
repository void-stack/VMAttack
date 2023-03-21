using System.Collections.Generic;
using System.Text;
using AsmResolver.DotNet;
using AsmResolver.IO;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

/// <summary>
///     This class represents a custom data reader for Eziriz virtual machine disassembly.
/// </summary>
public class EzirizStreamReader : EzirizReaderBase
{
    public readonly ManifestResource ManifestResource;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EzirizStreamReader" /> class.
    /// </summary>
    /// <param name="reader">The binary stream reader to read data from.</param>
    /// <param name="context">The context object for the disassembly process.</param>
    public EzirizStreamReader(Context context, BinaryStreamReader reader)
        : base(context, ref reader)
    {
        Logger.Info("Starting to read Eziriz Stream...");

        // Try to find the Eziriz resource data.
        if (!TryGetResource(out ManifestResource))
            throw new DevirtualizationException("Cannot find Eziriz Resource Data!");

        // Try to create a reader for the Eziriz resource data.
        if (!ManifestResource.TryGetReader(out Reader))
            throw new DevirtualizationException("Cannot create reader for Eziriz Resource Data!");

        // Read the number of operands.
        int size = ReadEncryptedByte();
        {
            Logger.Debug($"Reading {size} operands...");

            // Read each operand and add it to the operands dictionary.
            for (uint i = 0; i < size; i++)
            {
                byte index = Reader.ReadByte();
                byte operand = Reader.ReadByte();
                Operands.Add(index, operand);
            }
        }

        // Read the number of strings.
        size = ReadEncryptedByte();
        {
            Logger.Debug($"Reading {size} strings...");

            // Read each string and add it to the strings dictionary.
            for (uint id = 0; id < size; id++)
            {
                int bufferSize = ReadEncryptedByte();
                byte[] buffer = new byte[bufferSize];
                string value = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                Strings.Add(id, value);
            }
        }

        // Read the number of exports.
        size = ReadEncryptedByte();
        {
            Logger.Debug($"Reading {size} MethodKeys...");

            // Read the method IDs and add them to the method keys dictionary.
            for (uint id = 0; id < size; id++)
            {
                ulong methodId = (ulong) ReadEncryptedByte();
                MethodKeys.Add(id, methodId);
            }

            // Calculate the reader offset for each method key.
            ulong readerOffset = Reader.Offset;
            for (uint i = 0; i < size; i++)
            {
                ulong id = MethodKeys[i];
                MethodKeys[i] = readerOffset;
                readerOffset += id;
            }
        }
    }

    /// <summary>
    ///     Gets the dictionary of operands.
    /// </summary>
    public IDictionary<uint, byte> Operands { get; } = new Dictionary<uint, byte>();

    /// <summary>
    ///     Gets the dictionary of strings.
    /// </summary>
    public IDictionary<uint, string> Strings { get; } = new Dictionary<uint, string>();

    /// <summary>
    ///     Gets the dictionary of method keys.
    /// </summary>
    public IDictionary<uint, ulong> MethodKeys { get; } = new Dictionary<uint, ulong>();

    /// <summary>
    ///     Tries to find a resource with a specific name and length in the module's resources and sets the result to the
    ///     manifestResource parameter.
    /// </summary>
    /// <param name="manifestResource">The manifest resource found.</param>
    /// <returns>True if the manifest resource is found, false otherwise.</returns>
    private bool TryGetResource(out ManifestResource manifestResource)
    {
        var resources = Context.Module.Resources;
        manifestResource = null!;

        foreach (var resource in resources)
        {
            if (resource.Name is null)
                continue;

            if (resource.Name.Length != 37)
                continue;

            manifestResource = resource;
            return true;
        }

        return false;
    }
}