using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationId = "nonexistent-id"; // ID of the annotation to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable, so no using block is needed.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        try
        {
            // Attempt to delete the annotation. If the ID does not exist,
            // Aspose.Pdf throws a PdfException.
            editor.DeleteAnnotation(annotationId);
            Console.WriteLine($"Annotation '{annotationId}' deleted successfully.");
        }
        catch (PdfException ex)
        {
            // Handle the case where the annotation ID is not present.
            Console.Error.WriteLine($"Annotation deletion failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }

        // Save the modified PDF (or the original if deletion failed).
        editor.Save(outputPath);
        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}