using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "nonexistent-annotation-id";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Work with annotations via PdfAnnotationEditor (IDisposable)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Attempt to delete the annotation; handle case where it does not exist
                try
                {
                    editor.DeleteAnnotation(annotationName);
                    Console.WriteLine($"Annotation '{annotationName}' deleted.");
                }
                catch (PdfException ex)
                {
                    // Aspose.Pdf throws PdfException when the annotation cannot be found
                    Console.Error.WriteLine($"Failed to delete annotation '{annotationName}': {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors
                    Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}