using System.Collections.Immutable;
using EV2.CodeAnalysis.Symbols;

namespace EV2.CodeAnalysis.Binding
{
    internal sealed class BoundGlobalScope
    {
        public BoundGlobalScope(BoundGlobalScope? previous,
                                ImmutableArray<Diagnostic> diagnostics,
                                FunctionSymbol? mainFunction,
                                FunctionSymbol? scriptFunction,
                                ImmutableArray<StructSymbol> structs,
                                ImmutableArray<FunctionSymbol> functions,
                                ImmutableArray<VariableSymbol> variables,
                                ImmutableArray<BoundStatement> statements)
        {
            Previous = previous;
            Diagnostics = diagnostics;
            MainFunction = mainFunction;
            ScriptFunction = scriptFunction;
            Structs = structs;
            Functions = functions;
            Variables = variables;
            Statements = statements;
        }

        public BoundGlobalScope? Previous { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public FunctionSymbol? MainFunction { get; }
        public FunctionSymbol? ScriptFunction { get; }
        public ImmutableArray<StructSymbol> Structs { get; }
        public ImmutableArray<FunctionSymbol> Functions { get; }
        public ImmutableArray<VariableSymbol> Variables { get; }
        public ImmutableArray<BoundStatement> Statements { get; }
    }
}
