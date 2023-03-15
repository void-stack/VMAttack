using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Echo.ControlFlow;
using Echo.Core.Graphing.Analysis.Traversal;
using Echo.Platforms.AsmResolver;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping;

using FlowNode = ControlFlowNode<CilInstruction>;

/// <summary>
///     The OpcodeMapper class is responsible for mapping opcodes to their corresponding handler patterns.
/// </summary>
public class HandlerMapper : ContextBase
{
    // make singleton
    private static HandlerMapper? _instance;

    /// <summary>
    ///     Gets the dictionary of mapped opcodes to their corresponding instructions.
    /// </summary>
    private readonly Dictionary<int, List<CilInstruction>> _handlers = new();

    private MethodDefinition? _opCodeMethod;

    /// <summary>
    ///     Initializes a new instance of the OpcodeMapper class.
    /// </summary>
    /// <param name="context">The context in which the opcode mapper is operating.</param>
    private HandlerMapper(Context context) : base(context, context.Logger)
    {
        MapOpCodeHandlers();
    }

    public static HandlerMapper GetInstance(Context context)
    {
        return _instance ??= new HandlerMapper(context);
    }

    public bool TryGetOpcodeHandler(byte code, out EzirizHandler handler)
    {
        if (_handlers.TryGetValue(code, out var handlerInstructions))
        {
            handler = new EzirizHandler(handlerInstructions, _opCodeMethod);
            return true;
        }

        handler = new EzirizHandler();
        return false;
    }

    private void AddHandler(int opcode, List<CilInstruction> instructions)
    {
        Logger.Debug(_handlers.TryAdd(opcode, instructions)
            ? $"Added handler for opcode {opcode}"
            : $"Failed to add handler for opcode {opcode}");
    }

    /// <summary>
    ///     Maps the opcodes to their corresponding handler patterns.
    /// </summary>
    private void MapOpCodeHandlers()
    {
        // Finds the method that handles opcodes in the module.
        _opCodeMethod = FindOpCodeMethod(Context.Module);

        if (_opCodeMethod is null)
            throw new DevirtualizationException("Could not find opcode handler method!");

        var cilBody = _opCodeMethod.CilMethodBody;
        cilBody?.Instructions.OptimizeMacros(); // de4dot

        var cfg = cilBody.ConstructSymbolicFlowGraph(out var dfg);

        // Iterates through each node in the flow graph.
        foreach (var node in cfg.Nodes)
        {
            var contents = node.Contents;

            // Skips nodes that don't contain a switch statement.
            if (contents.Footer.OpCode.Code != CilCode.Switch)
                continue;

            // Gets the cases of switch. First label is assigned to first opcode and so on.
            var cases = contents.Footer.Operand as IList<ICilLabel>;

            // Iterates through each opcode.
            for (int opcode = 0; opcode < cases!.Count; opcode++)
            {
                // Gets the target node of the current opcode.
                var handler = cfg.GetNodeByOffset(cases[opcode].Offset);

                // Traverses the control flow graph and records the traversal order.
                var traversal = new DepthFirstTraversal();
                var recorder = new TraversalOrderRecorder(traversal);
                traversal.Run(handler);

                // Gets the full traversal order of the control flow graph.
                var fullTraversal = recorder.GetTraversal();
                var nodes = new List<FlowNode>();

                // Iterates through each node in the traversal order.
                foreach (var recordedNode in fullTraversal)
                {
                    if (recordedNode is not FlowNode handlerNode)
                        continue;

                    nodes.Add(handlerNode);
                }

                var basicBlocks = nodes.Select(q => q.Contents).ToList();
                var instructions = basicBlocks.SelectMany(q => q.Instructions).ToList();

                AddHandler(opcode, instructions);
            }
        }

        Logger.Info($"Dumped {_handlers.Count} used handles.");
    }


    /// <summary>
    ///     Experimental method to find the opcode handler method
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    private MethodDefinition? FindOpCodeMethod(ModuleDefinition module)
    {
        foreach (var type in module.GetAllTypes())
        {
            var method = type.Methods.FirstOrDefault(q =>
                q.IsIL && q.CilMethodBody?.Instructions != null &&
                q.CilMethodBody.Instructions.Count >= 3200 &&
                q.CilMethodBody.Instructions.Count(d => d.OpCode == CilOpCodes.Switch) == 1);

            if (method == null)
                continue;

            Logger.Debug(
                $"Treating method with MetadataToken 0x{method.MetadataToken.ToInt32():X4} as opcode handler.");
            return method;
        }

        return null;
    }
}