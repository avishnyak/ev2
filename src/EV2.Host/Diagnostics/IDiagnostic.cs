namespace EV2.Host
{
    public interface IDiagnostic
    {
        bool IsError { get; }
        DiagnosticLocation DiagnosticLocation { get; }
        string Message { get; }
        string? ContextSourceSnippet { get; }
        string? TargetSourceSnippet { get; }
        bool IsWarning { get; }

        string ToString();
    }
}