using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDFs and one output path
        if (args == null || args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input1.pdf> <input2.pdf> [<inputN.pdf> ...] <output.pdf>");
            return;
        }

        // All arguments except the last one are input files
        string[] inputFiles = args.Take(args.Length - 1).ToArray();
        string outputFile = args[args.Length - 1];

        // Verify that each input file exists
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
            // PdfFileEditor does not implement IDisposable, so no using block
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the input PDFs into the output PDF
            bool success = editor.Concatenate(inputFiles, outputFile);

            if (success)
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} files into '{outputFile}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}