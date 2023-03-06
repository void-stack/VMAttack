using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Echo.ControlFlow;
using Echo.Core.Graphing.Analysis.Traversal;
using Echo.Platforms.AsmResolver;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Mapping;

using FlowNode = ControlFlowNode<CilInstruction>;

public class OpcodeMapper : ContextBase
{
    private readonly Disassembler _disassembler;
    private readonly Dictionary<int, List<FlowNode>> _handlers;

    public OpcodeMapper(Context context, Disassembler disassembler) : base(context, context.Logger)
    {
        _disassembler = disassembler;
        _handlers = new Dictionary<int, List<FlowNode>>();
    }

    private IEnumerable<int> UsedOpcodesMap => _disassembler.UsedOpcodesMap;

    public void MapOpcodes()
    {
        // Finds the method that handles opcodes in the module.
        var opCodeMethod = FindOpCodeMethod(Context.Module);

        if (opCodeMethod is null)
            throw new DevirtualizationException("Could not find opcode handler method!");

        var cilBody = opCodeMethod.CilMethodBody;
        var flowGraph = cilBody.ConstructSymbolicFlowGraph(out var dataFlowGraph);

        // TODO: Fix this, echo has bugs
        // Parses the Abstract Syntax Tree (AST) from the flow graph.
        //var parser = new AstParser<CilInstruction>(flowGraph, dataFlowGraph);
        //var ast = parser.Parse();

        // Iterates through each node in the flow graph.
        foreach (var node in flowGraph.Nodes)
        {
            var contents = node.Contents;

            // Skips nodes that don't contain a switch statement.
            if (contents.Footer.OpCode.Code != CilCode.Switch)
                continue;

            // Gets the edges of the current node.
            var edges = node.ConditionalEdges.ToArray();

            // Iterates through each possible opcode.
            for (int opcode = 0; opcode < edges.Length; opcode++)
            {
                // Check if the opcode is used in the disassembled code.
                if (!UsedOpcodesMap.Contains(opcode))
                    continue;

                // Gets the target node of the current opcode.
                var handler = edges[opcode].Target;

                // Traverses the control flow graph and records the traversal order.
                var traversal = new DepthFirstTraversal();
                var recorder = new TraversalOrderRecorder(traversal);
                traversal.Run(handler);

                // Gets the full traversal order of the control flow graph.
                var fullTraversal = recorder.GetTraversal();
                var astFullTraversal = new List<FlowNode>();

                // Iterates through each node in the traversal order.
                foreach (var recordedNode in fullTraversal)
                {
                    if (recordedNode is not FlowNode handlerNode)
                        continue;

                    astFullTraversal.Add(handlerNode);

                    // Gets the AST node associated with the current handler node.
                    //var astNode = ast.GetNodeByOffset(handlerNode.Offset);
                    //astFullTraversal.Add(astNode);

                    Console.WriteLine(handlerNode.Contents);
                }

                _handlers.Add(opcode, astFullTraversal);
                Logger.Debug($"Dumped handle with opcode {opcode}");
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