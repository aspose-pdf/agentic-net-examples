using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: one or more input PDFs and one output PDF.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ConcatenatePdf <input1.pdf> [<input2.pdf> ...] <output.pdf>");
            return;
        }

        // Separate input files and output file.
        string[] inputFiles = args.Take(args.Length - 1).ToArray();
        string outputFile = args[args.Length - 1];

        // Verify that all input files exist.
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
            // PdfFileEditor does not implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the input PDFs into the specified output file.
            bool success = editor.Concatenate(inputFiles, outputFile);

            if (success)
            {
                Console.WriteLine($"Successfully concatenated {inputFiles.Length} files into '{outputFile}'.");
            }
            else
            {
                Console.Error.WriteLine("Concatenation failed. Check the input files and permissions.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}