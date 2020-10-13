using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EV2.CodeAnalysis.Syntax;
using EV2.CompilerService;
using EV2.IO;
using Mono.Options;

namespace EV2
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var outputPath = (string?) null;
            var moduleName = (string?) null;
            var referencePaths = new List<string>();
            var sourcePaths = new List<string>();
            var helpRequested = false;

            var options = new OptionSet
            {
                "usage: ev2c <source-paths> [options]",
                { "r=", "The {path} of an assembly to reference", v => referencePaths.Add(v) },
                { "o=", "The output {path} of the assembly to create", v => outputPath = v },
                { "m=", "The {name} of the module", v => moduleName = v },
                { "?|h|help", "Prints help", v => helpRequested = true },
                { "<>", v => sourcePaths.Add(v) }
            };

            options.Parse(args);

            if (helpRequested)
            {
                options.WriteOptionDescriptions(Console.Out);
                return 0;
            }

            if (sourcePaths.Count == 0)
            {
                Console.Error.WriteLine("error: need at least one source file");
                return 1;
            }

            if (outputPath == null)
                outputPath = Path.ChangeExtension(sourcePaths[0], ".exe");

            if (moduleName == null)
                moduleName = Path.GetFileNameWithoutExtension(outputPath);

            var syntaxTrees = new List<SyntaxTree>();
            var hasErrors = false;

            foreach (var path in sourcePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"error: file '{path}' doesn't exist");
                    hasErrors = true;
                    continue;
                }
            }

            foreach (var path in referencePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"error: file '{path}' doesn't exist");
                    hasErrors = true;
                    continue;
                }
            }

            if (hasErrors)
                return 1;

            var compilerHost = new ConsoleCompilerHost();
            var compilerService = new Server(compilerHost);

            await compilerService.Initialize();

            syntaxTrees.AddRange(await compilerService.Parse(sourcePaths));

            if (compilerHost.Errors > 0) {
                Console.Out.WriteBuildSummary(false, compilerHost.Errors, compilerHost.Warnings);

                return 1;
            }

            if (!compilerService.EmitBinary(syntaxTrees, moduleName, referencePaths.ToArray(), outputPath))
            {
                Console.Out.WriteBuildSummary(false, compilerHost.Errors, compilerHost.Warnings);

                return 1;
            }

            await compilerService.Shutdown();
            compilerService.Exit();

            Console.Out.WriteBuildSummary(true, compilerHost.Errors, compilerHost.Warnings);

            return 0;
        }
    }
}
