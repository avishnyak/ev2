using System;
using System.IO;
using EV2.CodeAnalysis.Text;
using EV2.Host;
using Range = EV2.Host.Range;

namespace EV2.CodeAnalysis
{
    public sealed class Diagnostic : IDiagnostic
    {
        private DiagnosticLocation? _diagnosticLocation;

        private Diagnostic(bool isError, TextLocation location, string message)
        {
            IsError = isError;
            Location = location;
            Message = message;
            IsWarning = !IsError;
        }

        public bool IsError { get; }
        public TextLocation Location { get; }
        public string Message { get; }
        public bool IsWarning { get; }

        public DiagnosticLocation DiagnosticLocation {
            get {
                // Lazy initialize external location
                if (_diagnosticLocation == null) {
                    var start = new Position(Location.StartLine, Location.StartCharacter);
                    var end = new Position(Location.EndLine, Location.EndCharacter);
                    var range = new Range(start, end);

                    var path = Uri.IsWellFormedUriString(Location.FileName, UriKind.Absolute)
                        ? new Uri(Location.FileName)
                        : string.IsNullOrWhiteSpace(Location.FileName)
                            ? new Uri("file://repl")
                            : new Uri(Path.GetFullPath(Location.FileName));

                    _diagnosticLocation = new DiagnosticLocation(path, range);
                }

                return _diagnosticLocation;
            }
        }

        public string? ContextSourceSnippet {
            get {
                var start = Location.Text.Lines[Location.StartLine].Start;
                var end = Location.Text.Lines[Location.EndLine].End;

                return Location.Text.ToString(start, end - start);
            }
        }
        public string? TargetSourceSnippet {
            get {
                return Location.Text.ToString(Location.Span.Start, Location.Span.End - Location.Span.Start);
            }
        }

        public override string ToString() => Message;

        public static Diagnostic Error(TextLocation location, string message)
        {
            return new Diagnostic(isError: true, location, message);
        }

        public static Diagnostic Warning(TextLocation location, string message)
        {
            return new Diagnostic(isError: false, location, message);
        }
    }
}