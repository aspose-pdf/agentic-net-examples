using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path after deletion
        const string outputPath = "output.pdf";
        // Name (ID) of the annotation to delete
        const string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to manipulate annotations
        // The class implements IDisposable, so wrap it in a using block
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Delete the annotation with the specified name (ID)
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation '{annotationName}' deleted. Output saved to '{outputPath}'.");
    }
}