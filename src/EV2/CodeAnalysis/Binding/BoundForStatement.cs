using EV2.CodeAnalysis.Symbols;
using EV2.CodeAnalysis.Syntax;

namespace EV2.CodeAnalysis.Binding
{
    internal sealed class BoundForStatement : BoundLoopStatement
    {
        public BoundForStatement(SyntaxNode syntax, VariableSymbol variable, BoundExpression lowerBound, BoundExpression upperBound, BoundStatement body, BoundLabel breakLabel, BoundLabel continueLabel)
            : base(syntax, breakLabel, continueLabel)
        {
            Variable = variable;
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Body = body;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ForStatement;
        public VariableSymbol Variable { get; }
        public BoundExpression LowerBound { get; }
        public BoundExpression UpperBound { get; }
        public BoundStatement Body { get; }
    }
}
