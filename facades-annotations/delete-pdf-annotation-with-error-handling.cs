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
        const string annotationName = "nonexistent-annotation-id";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor with the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // Bind the editor to the document (optional when using ctor with Document)
                editor.BindPdf(doc);

                try
                {
                    // Attempt to delete the annotation by name
                    editor.DeleteAnnotation(annotationName);
                    Console.WriteLine($"Annotation '{annotationName}' deleted successfully.");
                }
                catch (PdfException ex)
                {
                    // Handle the case where the annotation does not exist or other PDF errors
                    Console.Error.WriteLine($"Failed to delete annotation '{annotationName}': {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected exceptions
                    Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                }

                // Save the modified PDF
                editor.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
    }
}