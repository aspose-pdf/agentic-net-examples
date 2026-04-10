using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one input PDF and one output path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: concat.exe <input1.pdf> [<input2.pdf> ...] <output.pdf>");
            return;
        }

        // Last argument is the output file, the rest are input files
        string outputPath = args[args.Length - 1];
        string[] inputFiles = args.Take(args.Length - 1).ToArray();

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create the PdfFileEditor (no using – it does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Optional: close streams automatically after operation
        editor.CloseConcatenatedStreams = true;

        // Concatenate the input PDFs into the output PDF
        bool success = editor.Concatenate(inputFiles, outputPath);

        if (success)
        {
            Console.WriteLine($"Successfully concatenated PDFs to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed. Check the editor's LastException for details.");
        }

        // No explicit disposal required for PdfFileEditor
    }
}