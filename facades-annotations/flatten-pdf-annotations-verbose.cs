using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdf outputPdf [--verbose]
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputPdf> <outputPdf> [--verbose]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool verbose = false;

        // Detect verbose flag
        for (int i = 2; i < args.Length; i++)
        {
            if (args[i].Equals("--verbose", StringComparison.OrdinalIgnoreCase))
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
            // Initialize the annotation editor
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            if (verbose)
                Console.WriteLine($"Binding PDF: {inputPath}");

            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Example operation: flatten all annotations
            if (verbose)
                Console.WriteLine("Flattening all annotations...");

            editor.FlatteningAnnotations();

            // Example operation: delete all annotations of type Line (as a demo)
            // Uncomment the following lines if you want to delete specific types
            // AnnotationType[] lineTypes = new AnnotationType[] { AnnotationType.Line };
            // if (verbose)
            //     Console.WriteLine("Deleting line annotations on pages 1 to end...");
            // editor.FlatteningAnnotations(1, editor.Document.Pages.Count, lineTypes);

            if (verbose)
                Console.WriteLine($"Saving processed PDF to: {outputPath}");

            // Save the modified PDF
            editor.Save(outputPath);

            if (verbose)
                Console.WriteLine("Operation completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}