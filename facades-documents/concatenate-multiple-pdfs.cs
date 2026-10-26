using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: one or more input PDFs and one output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <input1.pdf> [<input2.pdf> ...] <output.pdf>");
            return;
        }

        // The last argument is the output file; all preceding arguments are input files
        string outputPath = args[args.Length - 1];
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, inputFiles, args.Length - 1);

        // Verify that each input file exists
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the input PDFs into the specified output PDF
            editor.Concatenate(inputFiles, outputPath);

            Console.WriteLine($"Successfully concatenated {inputFiles.Length} PDFs into '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}