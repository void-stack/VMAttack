using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;

namespace VMAttack.Core;

/// <summary>
///     This class explores a given .NET module to find methods that match a specified criteria.
/// </summary>
public class ModuleExplorer
{
    // The module being explored.
    private readonly ModuleDefinition _module;

    /// <summary>
    ///     Initializes a new instance of the ModuleExplorer class.
    /// </summary>
    /// <param name="module">The module to explore.</param>
    public ModuleExplorer(ModuleDefinition module)
    {
        _module = module;
    }

    /// <summary>
    ///     Finds a method in the module that matches the given criteria.
    /// </summary>
    /// <param name="methodDefinition">A delegate that defines the criteria for a matching method.</param>
    /// <returns>The first method that matches the given criteria, or null if no such method is found.</returns>
    public MethodDefinition? FindMethod(Func<MethodDefinition, bool> methodDefinition) =>
        // Get all methods in the module and its nested types, and return the first one that matches the given criteria.
        GetMethodsRecursive(_module).FirstOrDefault(methodDefinition);

    /// <summary>
    ///     Recursively gets all methods in the given module.
    /// </summary>
    /// <param name="t">The module to get the methods for.</param>
    /// <returns>An enumerable collection of method definitions for the given module.</returns>
    private static IEnumerable<MethodDefinition> GetMethodsRecursive(ModuleDefinition t) =>
        // Get all types in the module, and get all methods in those types and their nested types.
        t.GetAllTypes().SelectMany(GetMethodsRecursive);

    /// <summary>
    ///     Recursively gets all methods in the given type and its nested types.
    /// </summary>
    /// <param name="type">The type to get the methods for.</param>
    /// <returns>An enumerable collection of method definitions for the given type and its nested types.</returns>
    private static IEnumerable<MethodDefinition> GetMethodsRecursive(TypeDefinition type)
    {
        // Return all methods in this type.
        foreach (var m in type.Methods)
            yield return m;

        // Go through nested types and return all methods in those types as well.
        foreach (var t in type.NestedTypes)
        foreach (var m in GetMethodsRecursive(t))
            yield return m;
    }
}