namespace EV2.CodeAnalysis.Syntax
{
    public sealed partial class CallExpressionSyntax : ExpressionSyntax
    {
        private CallExpressionSyntax(SyntaxTree syntaxTree, ExpressionSyntax identifier, SyntaxToken openParenthesisToken, SeparatedSyntaxList<ExpressionSyntax> arguments, SyntaxToken closeParenthesisToken)
            : base(syntaxTree)
        {
            FullyQualifiedIdentifier = identifier;
            OpenParenthesisToken = openParenthesisToken;
            Arguments = arguments;
            CloseParenthesisToken = closeParenthesisToken;
        }

        internal CallExpressionSyntax(SyntaxTree syntaxTree, NameExpressionSyntax identifier, SyntaxToken openParenthesisToken, SeparatedSyntaxList<ExpressionSyntax> arguments, SyntaxToken closeParenthesisToken)
            : this(syntaxTree, (ExpressionSyntax)identifier, openParenthesisToken, arguments, closeParenthesisToken)
        { }

        internal CallExpressionSyntax(SyntaxTree syntaxTree, MemberAccessExpressionSyntax identifier, SyntaxToken openParenthesisToken, SeparatedSyntaxList<ExpressionSyntax> arguments, SyntaxToken closeParenthesisToken)
            : this(syntaxTree, (ExpressionSyntax)identifier, openParenthesisToken, arguments, closeParenthesisToken)
        { }

        public override SyntaxKind Kind => SyntaxKind.CallExpression;
        public ExpressionSyntax FullyQualifiedIdentifier { get; }
        public SyntaxToken Identifier => FullyQualifiedIdentifier.Kind == SyntaxKind.NameExpression ? ((NameExpressionSyntax)FullyQualifiedIdentifier).IdentifierToken : ((MemberAccessExpressionSyntax)FullyQualifiedIdentifier).IdentifierToken;
        public SyntaxToken OpenParenthesisToken { get; }
        public SeparatedSyntaxList<ExpressionSyntax> Arguments { get; }
        public SyntaxToken CloseParenthesisToken { get; }
    }
}