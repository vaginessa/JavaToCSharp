using JavaToCSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JavaToCSharpCli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Usage: JavaToCSharpCli.exe [pathToJavaDir] [pathToCsOutputDir]");
                return;
            }
            
            //if (!File.Exists(args[0]))
            //{
            //    Console.WriteLine("Java input file doesn't exist!");
            //    return;
            //}

            var inputDir = args[0];
            var outputDir = args[1];

            var inputInfo = new DirectoryInfo(inputDir);
            var outputInfo = new DirectoryInfo(outputDir);

            var files = inputInfo.GetFiles("*.java", SearchOption.AllDirectories);
            var filePaths = files.Select(x => x.FullName).ToList();

            if (filePaths.Any())
            {
                var options = new JavaConversionOptions();
                var inOutPairs = filePaths.ToDictionary(fileIn => fileIn, fileIn => $"{fileIn.Replace(inputInfo.FullName, outputInfo.FullName)}.cs");
                WriteAllFiles(inOutPairs, options);
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void WriteAllFiles(Dictionary<string, string> files, JavaConversionOptions options)
        {
            foreach (var file in files)
            {
                var directoryName = new FileInfo(file.Value).DirectoryName;
                if (directoryName != null) Directory.CreateDirectory(directoryName);
                if (!File.Exists(file.Value))
                {
                    var javaText = File.ReadAllText(file.Key);

                    try
                    {
                        var parsed = JavaToCSharpConverter.ConvertText(javaText, options);
                        File.WriteAllText(file.Value, parsed);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine($"Skipping file {file.Value} that would write to {file.Key}\n\n");
                    }
                }
            }
        }
    }
}
