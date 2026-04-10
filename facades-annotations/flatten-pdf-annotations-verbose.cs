using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input.pdf> <output.pdf> [--verbose]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool verbose = args.Any(a => a.Equals("--verbose", StringComparison.OrdinalIgnoreCase));

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                if (verbose)
                    Console.WriteLine($"[Verbose] Bound PDF: {inputPath}");

                // Example operation: flatten all annotations
                editor.FlatteningAnnotations();
                if (verbose)
                    Console.WriteLine("[Verbose] Flattened all annotations.");

                // Save the modified PDF
                editor.Save(outputPath);
                if (verbose)
                    Console.WriteLine($"[Verbose] Saved modified PDF to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}