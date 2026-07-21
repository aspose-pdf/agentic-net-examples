using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        string inputPath = args[0];
        string outputPath = args[1];
        bool verbose = Array.Exists(args, a => a.Equals("--verbose", StringComparison.OrdinalIgnoreCase));

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the annotation editor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            if (verbose) Console.WriteLine($"Binding PDF: {inputPath}");
            editor.BindPdf(inputPath);

            // Export existing annotations to an in‑memory XFDF stream (for logging)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                if (verbose) Console.WriteLine("Exporting annotations to XFDF (in‑memory)...");
                editor.ExportAnnotationsToXfdf(xfdfStream);
                if (verbose) Console.WriteLine($"Exported XFDF size: {xfdfStream.Length} bytes");
            }

            // Flatten all annotations
            if (verbose) Console.WriteLine("Flattening all annotations...");
            editor.FlatteningAnnotations();
            if (verbose) Console.WriteLine("Flattening completed.");

            // Delete all annotations
            if (verbose) Console.WriteLine("Deleting all annotations...");
            editor.DeleteAnnotations();
            if (verbose) Console.WriteLine("Deletion completed.");

            // Save the modified PDF
            if (verbose) Console.WriteLine($"Saving modified PDF to: {outputPath}");
            editor.Save(outputPath);
            if (verbose) Console.WriteLine("Save completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}