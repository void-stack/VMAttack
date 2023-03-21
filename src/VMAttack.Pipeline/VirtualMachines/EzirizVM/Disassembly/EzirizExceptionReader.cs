using AsmResolver.DotNet;
using AsmResolver.IO;
using VMAttack.Core;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

/// <summary>
///     Reads an EzirizException from a binary stream.
/// </summary>
public class EzirizExceptionReader : EzirizReaderBase
{
    private readonly Context _context;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EzirizExceptionReader" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="reader">The binary stream reader.</param>
    public EzirizExceptionReader(Context context, ref BinaryStreamReader reader) : base(context, ref reader)
    {
        _context = context;
    }

    /// <summary>
    ///     Reads an EzirizException from the binary stream.
    /// </summary>
    /// <returns>The read EzirizException.</returns>
    public EzirizException ReadEh()
    {
        var module = _context.Module;

        // Read the different parts of the exception and create a new instance of EzirizException.
        var exception = new EzirizException
        {
            TryStart = ReadEncryptedByte(),
            TryEnd = ReadEncryptedByte(),
            HandlerStart = ReadEncryptedByte(),
            HandlerEnd = ReadEncryptedByte(),
            EhType = (EzirizEhType) ReadEncryptedByte()
        };

        // Depending on the exception handling type, read additional information.
        switch (exception.EhType)
        {
            case EzirizEhType.Catch:
                // If the exception handling type is Catch, lookup the member and set the CatchType property.
                exception.CatchType = (ITypeDescriptor) module.LookupMember(ReadEncryptedByte());
                break;
            case EzirizEhType.Filter:
                // If the exception handling type is Filter, set the Filter property.
                exception.Filter = ReadEncryptedByte();
                break;
            case EzirizEhType.Finally:
                // If the exception handling type is Finally, do nothing.
                break;
            case EzirizEhType.Fault:
                // If the exception handling type is Fault, do nothing.
                break;
            default:
                // If the exception handling type is unknown, read an encrypted byte and discard it.
                ReadEncryptedByte();
                break;
        }

        return exception;
    }
}