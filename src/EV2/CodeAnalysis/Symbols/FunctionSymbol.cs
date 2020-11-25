using System.Collections.Immutable;
using EV2.CodeAnalysis.Syntax;

namespace EV2.CodeAnalysis.Symbols
{
    public sealed class FunctionSymbol : Symbol
    {
        internal FunctionSymbol(
                string name,
                ImmutableArray<ParameterSymbol> parameters,
                TypeSymbol returnType,
                FunctionDeclarationSyntax? declaration = null,
                FunctionSymbol? overloadFor = null,
                StructSymbol? receiver = null
            ) : base(name)
        {
            Parameters = parameters;
            ReturnType = returnType;
            Declaration = declaration;
            OverloadFor = overloadFor;
            Receiver = receiver;
        }

        public override SymbolKind Kind => SymbolKind.Function;
        public FunctionDeclarationSyntax? Declaration { get; }
        public ImmutableArray<ParameterSymbol> Parameters { get; }
        public TypeSymbol ReturnType { get; }
        public FunctionSymbol? OverloadFor { get; }
        public StructSymbol? Receiver { get; }
    }
}