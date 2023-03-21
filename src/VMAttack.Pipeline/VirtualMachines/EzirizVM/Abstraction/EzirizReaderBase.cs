using AsmResolver.IO;
using VMAttack.Core;
using VMAttack.Core.Abstraction;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;

/// <summary>
///     This abstract class serves as a base for custom stream reader classes.
/// </summary>
public abstract class EzirizReaderBase : ContextBase
{
    /// <summary>
    ///     The binary stream reader used to read data.
    /// </summary>
    protected BinaryStreamReader Reader;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EzirizReaderBase" /> class.
    /// </summary>
    /// <param name="context">The context used for reading.</param>
    /// <param name="reader">The binary stream reader used to read data.</param>
    protected EzirizReaderBase(Context context, ref BinaryStreamReader reader)
        : base(context, context.Logger)
    {
        Reader = reader;
    }

    /// <summary>
    ///     Reads an encrypted byte from the binary stream reader.
    /// </summary>
    /// <returns>The decrypted byte.</returns>
    protected int ReadEncryptedByte()
    {
        bool flag = false;
        uint num = Reader.ReadByte();
        uint num2 = 0U | num & 63U;

        if ((num & 64U) != 0U)
            flag = true;

        if (num < 128U)
        {
            if (flag)
                return ~(int) num2;

            return (int) num2;
        }

        int num3 = 0;
        for (;;)
        {
            uint num4 = Reader.ReadByte();
            num2 |= (num4 & 127U) << 7 * num3 + 6;
            if (num4 < 128U)
                break;

            num3++;
        }

        if (flag)
            return ~(int) num2;

        return (int) num2;
    }
}