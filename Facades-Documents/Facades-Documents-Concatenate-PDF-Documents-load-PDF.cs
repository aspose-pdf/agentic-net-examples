using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: outputPath inputPdf1 inputPdf2 [moreInputPdfs...]
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program.exe <output.pdf> <input1.pdf> <input2.pdf> [more inputs...]");
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
                Console.Error.WriteLine($"Error: Input file not found - {file}");
                return;
            }
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, instantiate directly.
            var editor = new PdfFileEditor();
            editor.Concatenate(inputFiles, outputPath);
            Console.WriteLine($"Successfully concatenated {inputFiles.Length} PDFs into '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Concatenation failed: {ex.Message}");
        }
    }
}
