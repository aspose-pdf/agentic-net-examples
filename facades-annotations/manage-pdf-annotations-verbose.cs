using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Added for AnnotationType enum

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        //   args[0] - input PDF file path
        //   args[1] - output PDF file path
        //   optional "--verbose" flag enables detailed logging
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
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document doc = new Document(inputPath))
            {
                // Initialize the annotation editor (facade) and bind the loaded document
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(doc);
                if (verbose) Console.WriteLine("PdfAnnotationEditor bound to document.");

                // Example operation 1: Flatten all annotations
                editor.FlatteningAnnotations();
                if (verbose) Console.WriteLine("FlatteningAnnotations() executed.");

                // Example operation 2: Export all annotations to XFDF (in-memory stream)
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    doc.ExportAnnotationsToXfdf(xfdfStream);
                    if (verbose) Console.WriteLine($"ExportAnnotationsToXfdf() wrote {xfdfStream.Length} bytes to memory stream.");

                    // Reset stream position before importing back
                    xfdfStream.Position = 0;

                    // Example operation 3: Import annotations from XFDF back into the document
                    doc.ImportAnnotationsFromXfdf(xfdfStream);
                    if (verbose) Console.WriteLine("ImportAnnotationsFromXfdf() completed.");
                }

                // Example operation 4: Delete all annotations of a specific type (e.g., FreeText)
                // Note: AnnotationType enum is in Aspose.Pdf.Annotations namespace
                AnnotationType[] typesToDelete = { AnnotationType.FreeText };
                editor.FlatteningAnnotations(1, doc.Pages.Count, typesToDelete);
                if (verbose) Console.WriteLine("FlatteningAnnotations(start, end, types) executed for FreeText annotations.");

                // Save the modified PDF (lifecycle rule: use Document.Save inside the using block)
                doc.Save(outputPath);
                if (verbose) Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
