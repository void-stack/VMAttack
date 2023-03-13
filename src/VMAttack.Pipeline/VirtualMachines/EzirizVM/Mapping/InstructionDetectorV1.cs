using System;
using System.Collections.Generic;
using System.Reflection;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping.Detection._6._9._0._0;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping;

public class InstructionDetectorV1 : InstructionDetectorBase
{
    private static InstructionDetectorV1? _instance;
    private readonly Dictionary<CilCode, Detector> _detectors = new();

    private InstructionDetectorV1()
    {
        Initialize();
    }

    public static InstructionDetectorV1 GetInstance()
    {
        if (_instance == null)
            _instance = new InstructionDetectorV1();

        return _instance;
    }

    private void AddDetector(DetectAttribute attr, Detector callback)
    {
        if (!_detectors.ContainsKey(attr.Code))
            _detectors.Add(attr.Code, callback);
        else
            _detectors[attr.Code] = callback;
    }

    private void Initialize()
    {
        var extensions = typeof(Handler);
        var methods = extensions.GetMethods();

        foreach (var method in methods)
        foreach (var attr in method.GetCustomAttributes<DetectAttribute>())
        {
            var detector = (Detector)Delegate.CreateDelegate(typeof(Detector), method);

            if (attr != null)
                AddDetector(attr, detector);
        }
    }

    public override CilCode Identify(EzirizOpcode instruction)
    {
        foreach (var kvp in _detectors)
            if (kvp.Value(instruction))
                return kvp.Key;

        throw new OriginalOpcodeUnknownException(instruction);
    }

    private delegate bool Detector(EzirizOpcode instruction);
}