using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.pdf> <output.pdf> [--verbose]");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        bool verbose = false;

        // Detect the optional verbose flag
        foreach (string arg in args)
        {
            if (arg.Equals("--verbose", StringComparison.OrdinalIgnoreCase))
            {
                verbose = true;
                break;
            }
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the annotation editor (creation rule)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF (load rule)
                if (verbose) Console.WriteLine($"Binding PDF: {inputPath}");
                editor.BindPdf(inputPath);

                // Example operation: flatten all annotations
                if (verbose) Console.WriteLine("Flattening all annotations...");
                editor.FlatteningAnnotations();

                // Save the modified PDF (save rule)
                if (verbose) Console.WriteLine($"Saving output PDF: {outputPath}");
                editor.Save(outputPath);

                if (verbose) Console.WriteLine("Annotation processing completed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}