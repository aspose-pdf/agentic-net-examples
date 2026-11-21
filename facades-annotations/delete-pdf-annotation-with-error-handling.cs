using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath       = "input.pdf";
        const string outputPath      = "output.pdf";
        const string annotationName  = "myAnnotation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does NOT implement IDisposable; instantiate directly.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor.
        try
        {
            editor.BindPdf(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to bind PDF: {ex.Message}");
            return;
        }

        // Attempt to delete the annotation. Catch PDF‑specific and generic exceptions.
        try
        {
            // DeleteAnnotation overload accepts a single annotation name.
            editor.DeleteAnnotation(annotationName);
            Console.WriteLine($"Annotation '{annotationName}' deleted.");
        }
        catch (PdfException ex)
        {
            // Thrown when the annotation does not exist or other PDF errors occur.
            Console.Error.WriteLine($"PDF error while deleting annotation: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }

        // Save the modified document.
        try
        {
            editor.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
        }
    }
}