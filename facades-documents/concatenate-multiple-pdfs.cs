using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two input PDFs and one output path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input1.pdf> <input2.pdf> [<inputN.pdf> ...] <output.pdf>");
            return;
        }

        // The last argument is the output file, all preceding are inputs
        string outputPath = args[args.Length - 1];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, inputPaths, args.Length - 1);

        // Verify that each input file exists
        foreach (string input in inputPaths)
        {
            if (!File.Exists(input))
            {
                Console.Error.WriteLine($"Input file not found: {input}");
                return;
            }
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate normally
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the input PDFs into the output PDF
            editor.Concatenate(inputPaths, outputPath);

            Console.WriteLine($"Successfully concatenated {inputPaths.Length} PDFs into '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}