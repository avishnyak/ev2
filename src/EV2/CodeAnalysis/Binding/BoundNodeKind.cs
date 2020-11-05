namespace EV2.CodeAnalysis.Binding
{
    internal enum BoundNodeKind
    {
        // Statements
        BlockStatement,
        ConditionalGotoStatement,
        DoWhileStatement,
        ExpressionStatement,
        ForStatement,
        GotoStatement,
        IfStatement,
        LabelStatement,
        MemberBlockStatement,
        NopStatement,
        ReturnStatement,
        SequencePointStatement,
        VariableDeclaration,
        WhileStatement,

        // Expressions
        AssignmentExpression,
        BinaryExpression,
        CallExpression,
        CompoundAssignmentExpression,
        ConversionExpression,
        ErrorExpression,
        FieldAccessExpression,
        LiteralExpression,
        UnaryExpression,
        VariableExpression,
    }
}
