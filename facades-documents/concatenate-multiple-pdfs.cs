using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: outputPath inputFile1 inputFile2 [additional input files...]
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: ConcatenatePdf <output.pdf> <input1.pdf> <input2.pdf> [more...]");
            return;
        }

        string outputPath = args[0];
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, 1, inputFiles, 0, inputFiles.Length);

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();

            // Ensure streams are closed after concatenation (optional but safe)
            editor.CloseConcatenatedStreams = true;

            // Perform concatenation
            bool success = editor.Concatenate(inputFiles, outputPath);

            if (success)
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} files to '{outputPath}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}