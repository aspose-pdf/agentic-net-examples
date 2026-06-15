using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.pdf> <output.pdf> [--verbose]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool verbose = false;

        // Detect the optional verbose flag.
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
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the annotation editor facade.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the source PDF.
            editor.BindPdf(inputPath);
            if (verbose) Console.WriteLine($"[Verbose] Bound PDF: {inputPath}");

            // Example operation 1: Export all existing annotations to a memory stream.
            using (MemoryStream exportStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(exportStream);
                if (verbose) Console.WriteLine("[Verbose] Exported annotations to XFDF (in-memory).");
                // Reset stream position for potential import later.
                exportStream.Position = 0;
            }

            // Example operation 2: Delete all annotations.
            editor.DeleteAnnotations();
            if (verbose) Console.WriteLine("[Verbose] Deleted all annotations.");

            // Example operation 3: Flatten any remaining annotations (no effect after delete, but shown for completeness).
            editor.FlatteningAnnotations();
            if (verbose) Console.WriteLine("[Verbose] Flattened annotations.");

            // Example operation 4: Import annotations from an XFDF file if it exists (demonstrates import).
            string xfdfPath = Path.ChangeExtension(inputPath, ".xfdf");
            if (File.Exists(xfdfPath))
            {
                editor.ImportAnnotationsFromXfdf(xfdfPath);
                if (verbose) Console.WriteLine($"[Verbose] Imported annotations from XFDF file: {xfdfPath}");
            }

            // Save the modified PDF.
            editor.Save(outputPath);
            if (verbose) Console.WriteLine($"[Verbose] Saved modified PDF to: {outputPath}");
            else Console.WriteLine($"Processed PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}