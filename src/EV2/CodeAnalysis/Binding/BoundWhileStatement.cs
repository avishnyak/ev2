using EV2.CodeAnalysis.Syntax;

namespace EV2.CodeAnalysis.Binding
{
    internal sealed class BoundWhileStatement : BoundLoopStatement
    {
        public BoundWhileStatement(SyntaxNode syntax, BoundExpression condition, BoundStatement body, BoundLabel breakLabel, BoundLabel continueLabel)
            : base(syntax, breakLabel, continueLabel)
        {
            Condition = condition;
            Body = body;
        }

        public override BoundNodeKind Kind => BoundNodeKind.WhileStatement;
        public BoundExpression Condition { get; }
        public BoundStatement Body { get; }
    }
}