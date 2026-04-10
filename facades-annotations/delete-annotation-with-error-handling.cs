using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string annotationName = "nonexistent-annotation-id";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create and use PdfAnnotationEditor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            try
            {
                // Attempt to delete the annotation by its name
                editor.DeleteAnnotation(annotationName);
                Console.WriteLine($"Annotation '{annotationName}' deleted.");
            }
            catch (PdfException ex)
            {
                // Handles errors such as annotation not existing
                Console.Error.WriteLine($"Failed to delete annotation '{annotationName}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Fallback for any other unexpected errors
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}
