using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.PatternMatching;

public class OpCodeMapper : ContextBase
{
    private static OpCodeMapper? _instance;
    private readonly HandlerMapper _handlerMapper;

    private readonly PatternMatcher _patternMatching;

    private OpCodeMapper(Context context) : base(context, context.Logger)
    {
        _patternMatching = PatternMatcher.GetInstance();
        _handlerMapper = HandlerMapper.GetInstance(context);
    }

    public static OpCodeMapper GetInstance(Context context)
    {
        if (_instance == null)
            _instance = new OpCodeMapper(context);

        return _instance;
    }

    public EzirizOpcode ResolveOpcode(byte vmCode)
    {
        var vmOpCode = _patternMatching.GetCreateOpCodeValue(vmCode);

        if (vmOpCode.IsIdentified)
            return vmOpCode;

        if (_handlerMapper.TryGetOpcodeHandler(vmCode, out var handler))
        {
            vmOpCode = new EzirizOpcode(handler)
            {
                VirtualCode = vmCode,
                HasVirtualCode = true,
                HasHandler = true
            };

            var opCodePat = _patternMatching.FindOpCode(vmOpCode);

            if (opCodePat is not null)
            {
                vmOpCode.IsIdentified = true;
                vmOpCode.CilOpCode = opCodePat.CilOpCode;
            }
        }
        else
        {
            Logger.Debug($"No handler found for opcode {vmCode}");
        }

        _patternMatching.SetOpCodeValue(vmCode, vmOpCode);
        return vmOpCode;
    }
}