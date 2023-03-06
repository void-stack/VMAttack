using System.Collections.Generic;
using System.Linq;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

public class Disassembler : ContextBase
{
    private readonly MethodDecoder _methodDecoder;

    private readonly Dictionary<uint, EzirizMethod> _methods = new();
    private readonly List<int> _usedOpcodesMap = new();

    public Disassembler(Context context, EzirizStreamReader ezirizStreamReader)
        : base(context, context.Logger)
    {
        if (!ezirizStreamReader.ManifestResource.TryGetReader(out var reader))
            throw new DevirtualizationException("Cannot create reader for disassembler!");

        _methodDecoder = new MethodDecoder(context, reader, ezirizStreamReader);
    }

    public IEnumerable<int> UsedOpcodesMap
    {
        get { return _usedOpcodesMap.OrderBy(opcode => opcode).Distinct(); }
    }

    public EzirizMethod GetOrCreateMethod(uint id, ulong methodOffset)
    {
        if (!_methods.TryGetValue(id, out var method))
        {
            Logger.Debug($"Created new method_{id:X4}, reading from offset {methodOffset:X8}.");

            var disassembled = _methodDecoder.CreateMethod(id, methodOffset);
            var distinctOpcodes = disassembled.EzirizBody.Instructions.Select(x => x.Opcode.Code);
            _usedOpcodesMap.AddRange(distinctOpcodes);

            _methods.Add(id, disassembled);
        }

        return method!;
    }
}