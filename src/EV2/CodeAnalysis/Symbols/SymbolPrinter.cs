using System;
using System.IO;
using EV2.CodeAnalysis.Syntax;
using EV2.IO;

namespace EV2.CodeAnalysis.Symbols
{
    internal static class SymbolPrinter
    {
        public static void WriteTo(Symbol symbol, TextWriter writer)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Function:
                    WriteFunctionTo((FunctionSymbol)symbol, writer);
                    break;
                case SymbolKind.GlobalVariable:
                    WriteGlobalVariableTo((GlobalVariableSymbol)symbol, writer);
                    break;
                case SymbolKind.LocalVariable:
                    WriteLocalVariableTo((LocalVariableSymbol)symbol, writer);
                    break;
                case SymbolKind.Parameter:
                    WriteParameterTo((ParameterSymbol)symbol, writer);
                    break;
                case SymbolKind.Type:
                    WriteTypeTo((TypeSymbol)symbol, writer);
                    break;
                case SymbolKind.Struct:
                    WriteStructTo((StructSymbol)symbol, writer);
                    break;
                default:
                    throw new Exception($"Unexpected symbol: {symbol.Kind}");
            }
        }

        private static void WriteFunctionTo(FunctionSymbol symbol, TextWriter writer)
        {
            writer.WriteKeyword(SyntaxKind.FunctionKeyword);
            writer.WriteSpace();
            writer.WriteIdentifier(symbol.Name);
            writer.WritePunctuation(SyntaxKind.OpenParenthesisToken);

            for (int i = 0; i < symbol.Parameters.Length; i++)
            {
                if (i > 0)
                {
                    writer.WritePunctuation(SyntaxKind.CommaToken);
                    writer.WriteSpace();
                }

                symbol.Parameters[i].WriteTo(writer);
            }

            writer.WritePunctuation(SyntaxKind.CloseParenthesisToken);

            if (symbol.ReturnType != TypeSymbol.Void)
            {
                writer.WritePunctuation(SyntaxKind.ColonToken);
                writer.WriteSpace();
                symbol.ReturnType.WriteTo(writer);
            }
        }

        private static void WriteGlobalVariableTo(GlobalVariableSymbol symbol, TextWriter writer)
        {
            writer.WriteKeyword(symbol.IsReadOnly ? SyntaxKind.LetKeyword : SyntaxKind.VarKeyword);
            writer.WriteSpace();
            writer.WriteIdentifier(symbol.Name);
            writer.WritePunctuation(SyntaxKind.ColonToken);
            writer.WriteSpace();
            symbol.Type.WriteTo(writer);
        }

        private static void WriteLocalVariableTo(LocalVariableSymbol symbol, TextWriter writer)
        {
            writer.WriteKeyword(symbol.IsReadOnly ? SyntaxKind.LetKeyword : SyntaxKind.VarKeyword);
            writer.WriteSpace();
            writer.WriteIdentifier(symbol.Name);
            writer.WritePunctuation(SyntaxKind.ColonToken);
            writer.WriteSpace();
            symbol.Type.WriteTo(writer);
        }

        private static void WriteParameterTo(ParameterSymbol symbol, TextWriter writer)
        {
            writer.WriteIdentifier(symbol.Name);
            writer.WritePunctuation(SyntaxKind.ColonToken);
            writer.WriteSpace();
            symbol.Type.WriteTo(writer);
        }

        private static void WriteStructTo(StructSymbol @struct, TextWriter writer)
        {
            writer.WriteKeyword(SyntaxKind.StructKeyword);
            writer.WriteSpace();
            writer.WriteIdentifier(@struct.Name);
        }

        private static void WriteTypeTo(TypeSymbol symbol, TextWriter writer)
        {
            writer.WriteIdentifier(symbol.Name);
        }
    }
}