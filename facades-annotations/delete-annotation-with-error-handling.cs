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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Initialize the annotation editor with the loaded document
                PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

                try
                {
                    // Attempt to delete the annotation; throws if the name does not exist
                    editor.DeleteAnnotation(annotationName);
                    Console.WriteLine($"Annotation '{annotationName}' deleted.");
                }
                catch (PdfException ex)
                {
                    // Handle the case where the annotation is not present
                    Console.WriteLine($"Failed to delete annotation '{annotationName}': {ex.Message}");
                }

                // Save the (potentially unchanged) PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}